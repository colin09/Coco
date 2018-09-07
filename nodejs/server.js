var express = require('express');

var bodyParser = require('body-parser');

var fs = require('fs');
var multer = require('multer');

var cookieParser = require('cookie-parser');




var app = express();
// 创建 application/x-www-form-urlencoded 编码解析
var urlencodedParser = bodyParser.urlencoded({extended:false});





app.use(express.static('content'));
app.use(bodyParser.urlencoded({extended:false}));
app.use(multer({dest:'/tmp/'}).array('image'));
app.use(cookieParser());



app.get('/',function(req,res){
	console.log('/ by get');
	console.log("cookie:");
	console.log(req.cookies);
	res.send(" 葛饰北斋的《神奈川冲浪里》非常具有辨识度，在这幅二维图像中大海尽情展现着它的力量。但这幅19世纪杰作的秘辛可能会令你大吃一惊。");
});

app.post('/',function(req,res){
	console.log('/ by post');
	res.send(" 1、虽然它以浪命名，但其实这里面还藏着一座山。");
});


app.delete('/del_user',function(req,res){
	console.log('/del_user by delete');
	res.send(" 2、它是版画集而不是一幅画");
});

app.get('/list_user',function(req,res){
	console.log('/list_user by get');
	res.send(" 3、版画集的制作是一种精明的商业举措");
});

app.get('/show*user',function(req,res){
	console.log('/show*user by get');
	res.send(" 正则匹配");
});




app.get('/index',function(req,res){
	console.log('/index by get');
	res.sendFile(__dirname+'/index.htm');
});

app.get('/process_get',function(req,res){
	console.log('/process_get by get');
	var response = {
		first_name: req.query.first_name,
		last_name: req.query.last_name
	};
	console.log(response);
	res.send(JSON.stringify(response));
});

app.post('/process_get', urlencodedParser, function(req,res){
	console.log('/process_get by post');
	var response = {
		first_name: req.body.first_name,
		last_name: req.body.last_name
	};
	console.log(response);
	res.send(JSON.stringify(response));
});


app.get('/upload',function(req,res){
	console.log('/upload by get');
	res.sendFile(__dirname+'/upload.htm');
});

app.post('/file_upload', function(req,res){
	console.log('/file_upload by post');
	console.log(req.files[0]);

	var des_file = __dirname+'/'+req.files[0].originalname;

	fs.readFile(req.files[0].path, function(error,data){
		fs.writeFile(des_file,data,function(error){
			if(error){
				console.log(error);
			}else{
				var response = {
					message: 'File uploaded successfully',
					filename: req.files[0].originalname
				};
			}
			console.log(response);
			res.send(JSON.stringify(response));

		});
	});

});

















var server = app.listen(9090,function(){
	var host = server.address().address;
	var port = server.address().port;

	console.log('server is listen on http://%s:%s',host,port);
});




/*
request 和 response 对象的具体介绍：
Request 对象 - request 对象表示 HTTP 请求，包含了请求查询字符串，参数，内容，HTTP 头部等属性。常见属性有：
req.app：当callback为外部文件时，用req.app访问express的实例
req.baseUrl：获取路由当前安装的URL路径
req.body / req.cookies：获得「请求主体」/ Cookies
req.fresh / req.stale：判断请求是否还「新鲜」
req.hostname / req.ip：获取主机名和IP地址
req.originalUrl：获取原始请求URL
req.params：获取路由的parameters
req.path：获取请求路径
req.protocol：获取协议类型
req.query：获取URL的查询参数串
req.route：获取当前匹配的路由
req.subdomains：获取子域名
req.accpets（）：检查请求的Accept头的请求类型
req.acceptsCharsets / req.acceptsEncodings / req.acceptsLanguages
req.get（）：获取指定的HTTP请求头
req.is（）：判断请求头Content-Type的MIME类型

Response 对象 - response 对象表示 HTTP 响应，即在接收到请求时向客户端发送的 HTTP 响应数据。常见属性有：
res.app：同req.app一样
res.append（）：追加指定HTTP头
res.set（）在res.append（）后将重置之前设置的头
res.cookie（name，value [，option]）：设置Cookie
opition: domain / expires / httpOnly / maxAge / path / secure / signed
res.clearCookie（）：清除Cookie
res.download（）：传送指定路径的文件
res.get（）：返回指定的HTTP头
res.json（）：传送JSON响应
res.jsonp（）：传送JSONP响应
res.location（）：只设置响应的Location HTTP头，不设置状态码或者close response
res.redirect（）：设置响应的Location HTTP头，并且设置状态码302
res.send（）：传送HTTP响应
res.sendFile（path [，options] [，fn]）：传送指定路径的文件 -会自动根据文件extension设定Content-Type
res.set（）：设置HTTP头，传入object可以一次设置多个头
res.status（）：设置HTTP状态码
res.type（）：设置Content-Type的MIME类型
*/