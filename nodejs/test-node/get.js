var http = require('http');
var url = require('url');
var util = require('util');

//get
http.createServer(function(req,res){
	res.writeHead(200,{'Content-Type':'text/plain'});
	res.end(util.inspect(url.prise(req.url,true)));
}).listen(9090);


//post
var querystring = require('querystring');

http.createServer(function(req,res){
	var post = '';

	req.on('data',function(chunk){
		post += chunk;
	});

	req.on('end',function(){
		post = querystring.parse(post);
		res.end(util.inspect(post));
	});

}).listen(9090);
