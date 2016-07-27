var events = require('events');

var eventEmitter = new events.EventEmitter();

eventEmitter.on('some_event',function(){
	console.log('event => some-event');
});

eventEmitter.on('some_event',function(arg1){
	console.log('event by arg => some-event ; arg:'+ arg1);
});


setTimeout(function(){
	eventEmitter.emit('some_event','pppppp');
},1000);


//EventEmitter 方法 类 及 事件 实例

var listen1 = function(){
	console.log('listener [listen1] action !');
}

var listen2 = function(){
	console.log('listener [listen2] action !');
}

eventEmitter.addListener('connect',listen1);

eventEmitter.on('connect',listen2);

var connectListeners = events.EventEmitter.listenerCount(eventEmitter,'connect');
//var connectListeners = require('events').EventEmitter.listenerCount(eventEmitter,'connect');
console.log("监听器个数：" + connectListeners);

eventEmitter.emit('connect');

eventEmitter.removeListener('connect',listen1);
console.log('remove listener listen1');

connectListeners = events.EventEmitter.listenerCount(eventEmitter,'connect');
console.log('监听器个数：' + connectListeners);

eventEmitter.emit('connect');

console.log("程序执行完毕。");




// Buffer(缓冲区)

var buf1 = new Buffer(10);

var buf2 = new Buffer([10,20,30,40,50]);

//utf-8 是默认的编码方式，此外它同样支持以下编码："ascii", "utf8", "utf16le", "ucs2", "base64" 和 "hex"。
var buf3 = new Buffer("www.node.js.org","utf-8");


//写入缓冲区 // 语法：buf.write(string[, offset[, length]][, encoding])

var buf4 = new Buffer(256);

var len = buf4.write("www.abc.com");
console.log("write byte length :"+len);


//读取缓冲区数据 // 语法：buf.toString([encoding[, start[, end]]])

var buf5 = new Buffer(26);
for (var i = 0 ; i < 26 ; i++) {
  buf5[i] = i + 97;
}

console.log(buf5.toString());
console.log(buf5.toString('ascii'));
console.log(buf5.toString('ascii',0,5));

console.log(buf5.toString('utf8',0,5));
console.log(buf5.toString(undefined,0,5));


// convert Buffer to json

var buf6 = new Buffer("www.node.js.org","utf-8");
var json = buf6.toJSON(buf6);

console.log('to json :' + json);
console.log(json);


// 缓冲区合并 // 语法：Buffer.concat(list[, totalLength])

var buf7 = new Buffer("link address:","utf-8");

var buf8 = new Buffer("www.node.js.org","utf-8");

var buf9 = Buffer.concat([buf7,buf8]);
console.log('concat Buffer ==> '+ buf9.toString());

//缓冲区比较 // 语法：buf.compare(otherBuffer);
var buf10 = new Buffer('ABC');
var buf11 = new Buffer('ABCD');

var result = buf10.compare(buf11);
if(result<0)
	console.log('在之前');
if(result==0)
	console.log('相同');
if(result>0)
	console.log('在之后');

//拷贝缓冲区  //语法：buf.copy(targetBuffer[, targetStart[, sourceStart[, sourceEnd]]])
var r = buf7.copy(buf8);
console.log('copy return :'+ r);
console.log('copy to :'+ buf8);

// 缓冲区裁剪  语法：buf.slice([start[, end]])
var buf13 = buf11.slice(0,2);
console.log('slice buf :'+ buf13.toString());

console.log('buf12.length='+ buf13.length);


/*******************************************************************************************************/
console.log('/*************************************************************************************/');

var fs = require("fs");

// 从流中读取数据
var data ="";
var readerStream = fs.createReadStream('readme.txt');
// 设置编码为 utf8
readerStream.setEncoding('utf8');
// 处理流事件 --> data, end, and error
readerStream.on('data',function(chunk){
	data+= chunk;
});

readerStream.on('end',function(){
	console.log(data);
});

readerStream.on('error',function(error){
	console.log(error.stack);
});


/**************/
var writerStream = fs.createWriteStream("output.txt");
// 使用 utf8 编码写入数据
writerStream.write(data,'utf8');
// 标记文件末尾
writerStream.end();

writerStream.on('finish',function(){
	console.log('write finished');
});

writerStream.on('error',function(error){
	console.log('write error:'+ error.stack);
});



var writerStream2 = fs.createWriteStream("output.txt");

// 管道读写操作
// 读取 input.txt 文件内容，并将内容写入到 output.txt 文件中
readerStream.pipe(writerStream2);
console.log("stream pipe finished");


console.log('/*************************************************************************************/');

var zlib = require('zlib');

// 压缩 input.txt 文件为 input.txt.gz
//readerStream.pipe(zlib.createGzip()).pipe(fs.createWriteStream('input.txt.gz'));
console.log("stream Gzip finished");

// 解压 input.txt.gz 文件为 input.txt
fs.createReadStream('input.txt.gz').pipe(zlib.createGunzip()).pipe(fs.createWriteStream('input.txt'));
console.log("stream Gunzip finished");




function printHello(){
   console.log( "Hello, World!");
}
// 两秒后执行以上函数
var t = setTimeout(printHello, 2000);

// 清除定时器
clearTimeout(t);

// 两秒后执行以上函数
var setInterval(printHello, 2000);
//使用 clearInterval(t) 函数来清除定时器

/*
console.log([data][, ...]);
console.info([data][, ...]);
console.error([data][, ...]);
console.warn([data][, ...]);
console.dir(obj[, options]);
console.time(label);
console.timeEnd(label);
console.trace(message[, ...]);
console.assert(value[, message][, ...]);
*/
///util.inherits
//util.inherits(constructor, superConstructor)是一个实现对象间原型继承 的函数。