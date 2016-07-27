const express = require('express');

const app = require('./app/app');
app.use(express.static('./public'));

var http = require('http');
var server = http.createServer(app);

server.listen(9090,function(){
	console.log('app is listening on port 9090...');
});






