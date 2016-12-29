console.log("vs code start....");
console.log(typeof(window));
console.log(global.process.cwd());



const Koa = require('koa'); // 导入koa，和koa 1.x不同，在koa2中，我们导入的是一个class，因此用大写的Koa表示:
//const router = require('koa-router')(); // 注意require('koa-router')返回的是函数:
const bodyParser = require('koa-bodyparser');
const controller = require('./controller'); //导入controller middleware

// 创建一个Koa对象表示web app本身:
const app = new Koa();

/*
// 对于任何请求，app将调用该异步函数处理请求：
app.use(async (ctx, next) => {
    //console.log(ctx);
    const start = new Date().getTime(); // 当前时间
    //console.log(`${ctx.request.method} ${ctx.request.url}`); // 打印URL
    console.log(`${ctx.method} ${ctx.url}`); // 打印URL 同上

    await next();

    const ms = new Date().getTime() - start; // 耗费时间
    console.log(`Time: ${ms}ms`); // 打印耗费时间

    ctx.response.type = 'text/html';
    ctx.response.body = '<h1>Hello, nodejs / Koa2 / ES7!</h1>';
});

// add url-route:
router.get('/list', async (ctx, next) => {
    console.log(`params: ${ctx.params}`);
    ctx.response.body = `<h1>Hello, list !</h1>`;
});

router.get('/', async (ctx, next) => {
    ctx.response.body = '<h1>Index</h1>';
});
*/


//由于middleware的顺序很重要，这个koa-bodyparser必须在router之前被注册到app对象上
app.use(bodyParser());

// 使用middleware:
app.use(controller());

// add router middleware:
//app.use(router.routes());




// 在端口3000监听:
app.listen(3000);
console.log('app started at port 3000...');