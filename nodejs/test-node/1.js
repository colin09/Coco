var http = require('http');
var fs = require('fs');

http.createServer(function(req,res){

	//发送 HTTP 头
	//HTTP 状态值：200
	/// 内容类型：text/plain
	res.writeHead(200,{'Content-Type':'text/plain'});


	//阻塞代码实例
	var data = fs.readFileSync('Readme.txt');

	//非阻塞代码实例
	fs.readFile('Readme.txt',function(error,data){
		if(error) 
			return console.error(error);
		console.log('function =>'+data);
	});

	//发送响应数据
	//res.end("hi, first node-js...");
	res.end("Sync ==> "+data);
}).listen(9090,'127.0.0.1');

console.log('Server running at http://127.0.0.1:9090/');