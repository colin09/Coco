var http = require("http");
var url = require("url");
var fs = require('fs');

function start(route) {
  function onRequest(request, response) {

    var pathname = url.parse(request.url).pathname;
    console.log("Request " + pathname + " received.");

    route(pathname);

    
    fs.readFile(pathname.substr(1), function (err, data) {
      if (err) {
         console.log(err);
         // HTTP 状态码: 404 : NOT FOUND
         // Content Type: text/plain
         response.writeHead(404, {'Content-Type': 'text/html'});
      }else{           
         // HTTP 状态码: 200 : OK
         // Content Type: text/plain
         response.writeHead(200, {'Content-Type': 'text/html'});  
         
         // 响应文件内容
         response.write(data.toString());   
      }
      //  发送响应数据
      response.end();
   }); 


  }

  http.createServer(onRequest).listen(9090);
  console.log("Server has started.");
}

exports.start = start;