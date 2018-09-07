console.log("vs code start....");
console.log(typeof(window)); //nodejs or browser
console.log(global.process.cwd());
console.log(global.process.env.NODE_ENV);

const isProduction = process.env.NODE_ENV === 'production';
const Koa = require('koa'); // 导入koa，和koa 1.x不同，在koa2中，我们导入的是一个class，因此用大写的Koa表示:
//const router = require('koa-router')(); // 注意require('koa-router')返回的是函数:
const bodyParser = require('koa-bodyparser');
const controller = require('./controller'); //导入controller middleware
const templating = require('./templating');

// 导入WebSocket模块:
const WebSocket = require('ws');
const Cookies = require('cookies');
const url = require('url');

// 引用Server类:
const WebSocketServer = WebSocket.Server;
// 创建一个Koa对象表示web app本身:
const app = new Koa();


/**
 * 识别用户身份(cookie)
 */
function parseUser(obj) {
    if (!obj) {
        return;
    }
    console.log('try parse: ' + obj);
    let s = '';
    if (typeof obj === 'string') {
        s = obj;
    } else if (obj.headers) {
        let cookies = new Cookies(obj, null);
        s = cookies.get('fwCookie');
    }
    if (s) {
        try {
            let user = JSON.parse(Buffer.from(s, 'base64').toString());
            console.log(`User: ${user.name}, ID: ${user.id}`);
            return user;
        } catch (e) {
            // ignore
        }
    }
}


/**
 * 第一个middleware是记录URL以及页面执行时间：
 * 对于任何请求，app将调用该异步函数处理请求
 */
app.use(async (ctx, next) => {
    //console.log(ctx);
    var start = new Date().getTime(); // 当前时间
    //console.log(`${ctx.request.method} ${ctx.request.url}`); // 打印URL
    console.log(`${ctx.method} ${ctx.url}`); // 打印URL 同上

    ctx.state.user = parseUser(ctx.cookies.get('fwCookie') || '');
    await next();

    var execTime = new Date().getTime() - start; // 耗费时间
    console.log(`Time: ${execTime}ms`); // 打印耗费时间
    ctx.response.set('X-Response-Time', `${execTime}ms`);
});

/**
 * 第二个middleware处理静态文件：
 */
if (! isProduction) {
    let staticFiles = require('./static-files');
    app.use(staticFiles('/static/', __dirname + '/static'));
}


/**
 * 第三个middleware解析POST请求(json)：
 * 由于middleware的顺序很重要，这个koa-bodyparser必须在router之前被注册到app对象上
 */
app.use(bodyParser());

/**
 * 第四个middleware负责给ctx加上render()来使用Nunjucks：
 */
app.use(templating('views', {
    noCache: !isProduction,
    watch: !isProduction
}));




/**
 * 最后一个middleware处理URL路由：
 */
app.use(controller());

/**
 *  在端口3000监听:
 */ 
let server = app.listen(3000);
//console.log('app started at port 3000...');





function createWebSocketServer(server,wssBroadcast, onConnection, onMessage, onClose, onError) {
    let wss = new WebSocketServer({
        server: server
    });
    //wss.broadcast = wssBroadcast;
    wss.broadcast = function(data) {
        wss.clients.forEach(function (client) {
            client.send(data);
    });
};

    wss.on('connection',function(ws){        
        let location = url.parse(ws.upgradeReq.url, true);
        console.log('[WebSocketServer] connection: ' + location.href);

        ws.on('message',onMessage);
        ws.on('close', onClose);
        ws.on('error', onError);

        if (location.pathname !== '/ws/chat') {
            ws.close(4000, 'Invalid URL');
        }

        let user = parseUser(ws.upgradeReq);
        if (!user) {
            // Cookie不存在或无效，直接关闭WebSocket:
            ws.close(4001, 'Invalid user');
            console.log('Invalid user');
        }
        // 识别成功，把user绑定到该WebSocket对象:
        ws.user = user;
        // 绑定WebSocketServer对象:
        ws.wss = wss;
        onConnection.apply(ws);
    });
    console.log('WebSocketServer was attached.');
    return wss;
}


// 消息ID:
var messageIndex = 0;

function createMessage(type, user, data) {
    messageIndex ++;
    return JSON.stringify({
        id: messageIndex,
        type: type,
        user: user,
        data: data
    });
}

// 广播
function wssBroadcast(data) {
    wss.clients.forEach(function (client) {
        client.send(data);
    });
};

function onConnect() {
    let user = this.user;
    let msg = createMessage('join', user, `${user.name} joined.`);
    this.wss.broadcast(msg);
    // build user list:
    let users = this.wss.clients.map(function (client) {
        return client.user;
    });
    this.send(createMessage('list', user, users));
}

function onMessage(message) {
    console.log(message);
    if (message && message.trim()) {
        let msg = createMessage('chat', this.user, message.trim());
        this.wss.broadcast(msg);
    }
}

function onClose() {
    let user = this.user;
    let msg = createMessage('left', user, `${user.name} is left.`);
    this.wss.broadcast(msg);
}

function onError(err) {
    console.log('[WebSocket] error: ' + err);
};

app.wss = createWebSocketServer(server, wssBroadcast, onConnect, onMessage, onClose, onError);
console.log('app started at port 3000...');