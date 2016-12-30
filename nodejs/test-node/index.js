var server = require("./server");
var router = require("./router");

server.start(router.route);


//当前正在执行的脚本的文件名
console.log(__filename);

//当前执行脚本所在的目录
console.log(__dirname);