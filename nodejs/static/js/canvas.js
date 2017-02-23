function draw1(){
  var canvas = document.getElementById('canvas1');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');
    ctx.fillStyle = "rgb(200,0,0)";
    ctx.fillRect (10, 10, 55, 50);

    ctx.fillStyle = "rgba(0, 0, 200, 0.5)";
    ctx.fillRect (30, 30, 55, 50);
  }else {
    // canvas-unsupported code here
    console.log('canvas-unsupported');
  }
}
draw1();

//矩形（Rectangular）例子
function draw21() {
  var canvas = document.getElementById('canvas21');
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');

    ctx.fillRect(25,25,100,100);
    ctx.clearRect(45,45,60,60);
    ctx.strokeRect(50,50,50,50);
  }
}
draw21();

//绘制一个三角形
function draw22() {
  var canvas = document.getElementById('canvas22');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    ctx.beginPath();
    ctx.moveTo(75,50);
    ctx.lineTo(100,75);
    ctx.lineTo(100,25);
    ctx.fill();
  }
}
draw22();

//移动笔触
function draw23() {
  var canvas = document.getElementById('canvas23');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    ctx.beginPath();
    ctx.arc(75,75,50,0,Math.PI*2,true); // 绘制
    ctx.moveTo(110,75);
    ctx.arc(75,75,35,0,Math.PI,false);   // 口(顺时针)
    ctx.moveTo(65,65);
    ctx.arc(60,65,5,0,Math.PI*2,true);  // 左眼
    ctx.moveTo(95,65);
    ctx.arc(90,65,5,0,Math.PI*2,true);  // 右眼
    ctx.stroke();
  }
}
draw23();

//线
function draw24() {
  var canvas = document.getElementById('canvas24');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    // 填充三角形
    ctx.beginPath();
    ctx.moveTo(25,25);
    ctx.lineTo(105,25);
    ctx.lineTo(25,105);
    ctx.fill();

    // 描边三角形
    ctx.beginPath();
    ctx.moveTo(125,125);
    ctx.lineTo(125,45);
    ctx.lineTo(45,125);
    ctx.closePath();
    ctx.stroke();
  }
}
draw24();

//圆弧
function draw25() {
  var canvas = document.getElementById('canvas25');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    for(var i=0;i<4;i++){
      for(var j=0;j<3;j++){
        ctx.beginPath();
        var x              = 25+j*50;               // x 坐标值
        var y              = 25+i*50;               // y 坐标值
        var radius         = 20;                    // 圆弧半径
        var startAngle     = 0;                     // 开始点
        var endAngle       = Math.PI+(Math.PI*j)/2; // 结束点
        var anticlockwise  = i%2==0 ? false : true; // 顺时针或逆时针

        ctx.arc(x, y, radius, startAngle, endAngle, anticlockwise);

        if (i>1){
          ctx.fill();
        } else {
          ctx.stroke();
        }
      }
    }
  }
}
draw25();


//贝塞尔（bezier）以及二次贝塞尔
//贝塞尔曲线
function draw26() {
  var canvas = document.getElementById('canvas26');
  if (canvas.getContext) {
    var ctx = canvas.getContext('2d');

    // 贝尔赛曲线
    ctx.beginPath();
    ctx.moveTo(75,25);
    ctx.quadraticCurveTo(25,25,25,62.5);
    ctx.quadraticCurveTo(25,100,50,100);
    ctx.quadraticCurveTo(50,120,30,125);
    ctx.quadraticCurveTo(60,120,65,100);
    ctx.quadraticCurveTo(125,100,125,62.5);
    ctx.quadraticCurveTo(125,25,75,25);
    ctx.stroke();
  }
}
draw26();

//二次贝塞尔曲线
function draw27() {
  var canvas = document.getElementById('canvas27');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    //二次曲线
    ctx.beginPath();
    ctx.moveTo(75,40);
    ctx.bezierCurveTo(75,37,70,25,50,25);
    ctx.bezierCurveTo(20,25,20,62.5,20,62.5);
    ctx.bezierCurveTo(20,80,40,102,75,120);
    ctx.bezierCurveTo(110,102,130,80,130,62.5);
    ctx.bezierCurveTo(130,62.5,130,25,100,25);
    ctx.bezierCurveTo(85,25,75,37,75,40);
    ctx.fill();
  }
}
draw27();


//组合使用
function draw28() {
  var canvas = document.getElementById('canvas28');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    roundedRect(ctx,5,5,190,190,15);
    roundedRect(ctx,10,10,175,175,9);
    roundedRect(ctx,45,53,49,33,10);
    roundedRect(ctx,45,119,49,16,6);
    roundedRect(ctx,125,53,49,33,10);
    roundedRect(ctx,125,119,25,49,10);

    ctx.beginPath();
    ctx.arc(37,37,13,Math.PI/7,-Math.PI/7,false);
    ctx.lineTo(31,37);
    ctx.fill();

    for(var i=0;i<8;i++){
      ctx.fillRect(40+i*16,35,4,4);
    }

    for(i=0;i<10;i++){
      ctx.fillRect(104,35+i*16,4,4);
    }

    for(i=0;i<8;i++){
      ctx.fillRect(40+i*16,99,4,4);
    }

    ctx.beginPath();
    ctx.moveTo(83,116);
    ctx.lineTo(83,102);
    ctx.bezierCurveTo(83,94,89,88,97,88);
    ctx.bezierCurveTo(105,88,111,94,111,102);
    ctx.lineTo(111,116);
    ctx.lineTo(106.333,111.333);
    ctx.lineTo(101.666,116);
    ctx.lineTo(97,111.333);
    ctx.lineTo(92.333,116);
    ctx.lineTo(87.666,111.333);
    ctx.lineTo(83,116);
    ctx.fill();

    ctx.fillStyle = "white";
    ctx.beginPath();
    ctx.moveTo(91,96);
    ctx.bezierCurveTo(88,96,87,99,87,101);
    ctx.bezierCurveTo(87,103,88,106,91,106);
    ctx.bezierCurveTo(94,106,95,103,95,101);
    ctx.bezierCurveTo(95,99,94,96,91,96);
    ctx.moveTo(103,96);
    ctx.bezierCurveTo(100,96,99,99,99,101);
    ctx.bezierCurveTo(99,103,100,106,103,106);
    ctx.bezierCurveTo(106,106,107,103,107,101);
    ctx.bezierCurveTo(107,99,106,96,103,96);
    ctx.fill();

    ctx.fillStyle = "black";
    ctx.beginPath();
    ctx.arc(101,102,2,0,Math.PI*2,true);
    ctx.fill();

    ctx.beginPath();
    ctx.arc(89,102,2,0,Math.PI*2,true);
    ctx.fill();
  }
}

// 封装的一个用于绘制圆角矩形的函数.

function roundedRect(ctx,x,y,width,height,radius){
  ctx.beginPath();
  ctx.moveTo(x,y+radius);
  ctx.lineTo(x,y+height-radius);
  ctx.quadraticCurveTo(x,y+height,x+radius,y+height);
  ctx.lineTo(x+width-radius,y+height);
  ctx.quadraticCurveTo(x+width,y+height,x+width,y+height-radius);
  ctx.lineTo(x+width,y+radius);
  ctx.quadraticCurveTo(x+width,y,x+width-radius,y);
  ctx.lineTo(x+radius,y);
  ctx.quadraticCurveTo(x,y,x,y+radius);
  ctx.stroke();
}
draw28();

//Path2D 示例
function draw29() {
  var canvas = document.getElementById('canvas29');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    var rectangle = new Path2D();
    rectangle.rect(10, 10, 50, 50);

    var circle = new Path2D();
    circle.moveTo(125, 35);
    circle.arc(100, 35, 25, 0, 2 * Math.PI);

    ctx.stroke(rectangle);
    ctx.fill(circle);
  }
}

draw29();


//SVG paths
function draw2A() {
  var canvas = document.getElementById('canvas2A');
  if (canvas.getContext){
    var ctx = canvas.getContext('2d');

    var p = new Path2D('M10 10 h 80 v 70 h -60 Z');
    ctx.fill(p);
  }
}
draw2A();

/********************************************************************************************************************************************* 2 */

//fillStyle 示例
function draw31() {
  var ctx = document.getElementById('canvas31').getContext('2d');
  for (var i=0;i<6;i++){
    for (var j=0;j<6;j++){
      ctx.fillStyle = 'rgb(' + Math.floor(255-42.5*i) + ',' + Math.floor(255-42.5*j) + ',0)';
      ctx.fillRect(j*25,i*25,25,25);
    }
  }
}
draw31();

//strokeStyle 示例
function draw32() {
    var ctx = document.getElementById('canvas32').getContext('2d');
    for (var i=0;i<6;i++){
      for (var j=0;j<6;j++){
        ctx.strokeStyle = 'rgb(0,' + Math.floor(255-42.5*i) + ',' + 
                         Math.floor(255-42.5*j) + ')';
        ctx.beginPath();
        ctx.arc(12.5+j*25,12.5+i*25,10,0,Math.PI*2,true);
        ctx.stroke();
      }
    }
  }
  draw32();

//globalAlpha 示例
function draw33() {
  var ctx = document.getElementById('canvas33').getContext('2d');
  // 画背景
  ctx.fillStyle = '#FD0';
  ctx.fillRect(0,0,75,75);
  ctx.fillStyle = '#6C0';
  ctx.fillRect(75,0,75,75);
  ctx.fillStyle = '#09F';
  ctx.fillRect(0,75,75,75);
  ctx.fillStyle = '#F30';
  ctx.fillRect(75,75,75,75);
  ctx.fillStyle = '#FFF';

  // 设置透明度值
  ctx.globalAlpha = 0.2;

  // 画半透明圆
  for (var i=0;i<7;i++){
      ctx.beginPath();
      ctx.arc(75,75,10+10*i,0,Math.PI*2,true);
      ctx.fill();
  }
}
draw33();

//rgba() 示例
function draw34() {
  var ctx = document.getElementById('canvas34').getContext('2d');

  // 画背景
  ctx.fillStyle = 'rgb(255,221,0)';
  ctx.fillRect(0,0,150,37.5);
  ctx.fillStyle = 'rgb(102,204,0)';
  ctx.fillRect(0,37.5,150,37.5);
  ctx.fillStyle = 'rgb(0,153,255)';
  ctx.fillRect(0,75,150,37.5);
  ctx.fillStyle = 'rgb(255,51,0)';
  ctx.fillRect(0,112.5,150,37.5);

  // 画半透明矩形
  for (var i=0;i<10;i++){
    ctx.fillStyle = 'rgba(255,255,255,'+(i+1)/10+')';
    for (var j=0;j<4;j++){
      ctx.fillRect(5+i*14,5+j*37.5,14,27.5)
    }
  }
}
draw34();

//线型 Line styles
/**
    lineWidth = value
    设置线条宽度。
    lineCap = type
    设置线条末端样式。
    lineJoin = type
    设定线条与线条间接合处的样式。
    miterLimit = value
    限制当两条线相交时交接处最大长度；所谓交接处长度（斜接长度）是指线条交接处内角顶点到外角顶点的长度。
    getLineDash()
    返回一个包含当前虚线样式，长度为非负偶数的数组。
    setLineDash(segments)
    设置当前虚线样式。
    lineDashOffset = value
    设置虚线样式的起始偏移量。
 */
function draw35() {
  var ctx = document.getElementById('canvas35').getContext('2d');
  for (var i = 0; i < 10; i++){
    ctx.lineWidth = 1+i;
    ctx.beginPath();
    ctx.moveTo(5+i*14,5);
    ctx.lineTo(5+i*14,140);
    ctx.stroke();
  }
}
draw35();

//lineCap 属性的例子
function draw36() {
  var ctx = document.getElementById('canvas36').getContext('2d');
  var lineCap = ['butt','round','square'];

  // 创建路径
  ctx.strokeStyle = '#09f';
  ctx.beginPath();
  ctx.moveTo(10,10);
  ctx.lineTo(140,10);
  ctx.moveTo(10,140);
  ctx.lineTo(140,140);
  ctx.stroke();

  // 画线条
  ctx.strokeStyle = 'black';
  for (var i=0;i<lineCap.length;i++){
    ctx.lineWidth = 15;
    ctx.lineCap = lineCap[i];
    ctx.beginPath();
    ctx.moveTo(25+i*50,10);
    ctx.lineTo(25+i*50,140);
    ctx.stroke();
  }
}
draw36();

//lineJoin 属性的例子
function draw37() {
  var ctx = document.getElementById('canvas37').getContext('2d');
  var lineJoin = ['round','bevel','miter'];
  ctx.lineWidth = 10;
  for (var i=0;i<lineJoin.length;i++){
    ctx.lineJoin = lineJoin[i];
    ctx.beginPath();
    ctx.moveTo(-5,5+i*40);
    ctx.lineTo(35,45+i*40);
    ctx.lineTo(75,5+i*40);
    ctx.lineTo(115,45+i*40);
    ctx.lineTo(155,5+i*40);
    ctx.stroke();
  }
}
draw37();

//miterLimit 属性的演示例子

//使用虚线
var ctx = document.getElementById('canvas38').getContext('2d');
var offset = 0;

function draw38() {
  ctx.clearRect(0,0, 180, 180);
  ctx.setLineDash([4, 2]);
  ctx.lineDashOffset = -offset;
  ctx.strokeRect(10,10, 100, 100);
}

function march38() {
  offset++;
  if (offset > 16) {
    offset = 0;
  }
  draw38();
  setTimeout(march38, 20);
}

march38();

//渐变 Gradients
function draw39() {
  var ctx = document.getElementById('canvas39').getContext('2d');

  // Create gradients
  var lingrad = ctx.createLinearGradient(0,0,0,150);
  lingrad.addColorStop(0, '#00ABEB');
  lingrad.addColorStop(0.5, '#fff');
  lingrad.addColorStop(0.5, '#26C000');
  lingrad.addColorStop(1, '#fff');

  var lingrad2 = ctx.createLinearGradient(0,50,0,95);
  lingrad2.addColorStop(0.5, '#000');
  lingrad2.addColorStop(1, 'rgba(0,0,0,0)');

  // assign gradients to fill and stroke styles
  ctx.fillStyle = lingrad;
  ctx.strokeStyle = lingrad2;
  
  // draw shapes
  ctx.fillRect(10,10,130,130);
  ctx.strokeRect(50,50,50,50);

}
draw39();

//
function draw3A() {
  var ctx = document.getElementById('canvas3A').getContext('2d');

  // 创建渐变
  var radgrad = ctx.createRadialGradient(45,45,10,52,50,30);
  radgrad.addColorStop(0, '#A7D30C');
  radgrad.addColorStop(0.9, '#019F62');
  radgrad.addColorStop(1, 'rgba(1,159,98,0)');
  
  var radgrad2 = ctx.createRadialGradient(105,105,20,112,120,50);
  radgrad2.addColorStop(0, '#FF5F98');
  radgrad2.addColorStop(0.75, '#FF0188');
  radgrad2.addColorStop(1, 'rgba(255,1,136,0)');

  var radgrad3 = ctx.createRadialGradient(95,15,15,102,20,40);
  radgrad3.addColorStop(0, '#00C9FF');
  radgrad3.addColorStop(0.8, '#00B5E2');
  radgrad3.addColorStop(1, 'rgba(0,201,255,0)');

  var radgrad4 = ctx.createRadialGradient(0,150,50,0,140,90);
  radgrad4.addColorStop(0, '#F4F201');
  radgrad4.addColorStop(0.8, '#E4C700');
  radgrad4.addColorStop(1, 'rgba(228,199,0,0)');
  
  // 画图形
  ctx.fillStyle = radgrad4;
  ctx.fillRect(0,0,150,150);
  ctx.fillStyle = radgrad3;
  ctx.fillRect(0,0,150,150);
  ctx.fillStyle = radgrad2;
  ctx.fillRect(0,0,150,150);
  ctx.fillStyle = radgrad;
  ctx.fillRect(0,0,150,150);
}
draw3A();

//图案样式 Patterns 
//createPattern 的例子
function draw3B() {
  var ctx = document.getElementById('canvas3B').getContext('2d');

  // 创建新 image 对象，用作图案
  var img = new Image();
  img.src = '/static/img/Canvas_createpattern.png';
  img.onload = function(){

    // 创建图案
    var ptrn = ctx.createPattern(img,'repeat');
    ctx.fillStyle = ptrn;
    ctx.fillRect(0,0,150,150);
  }
}
draw3B();

//阴影 Shadows
//文字阴影的例子
function draw3C() {
  var ctx = document.getElementById('canvas3C').getContext('2d');

  ctx.shadowOffsetX = 5;
  ctx.shadowOffsetY = 5;
  ctx.shadowBlur = 5;
  ctx.shadowColor = "rgba(0, 0, 0, 0.5)";
 
  ctx.font = "20px Times New Roman";
  ctx.fillStyle = "Black";
  ctx.fillText("Sample String", 5, 30);
}
draw3C();

//Canvas 填充规则
//nonzero 默认值 ,  evenodd ;  这对于自己与自己路径相交或者路径被嵌套的时候是有用的。
function draw3D() {
  var ctx = document.getElementById('canvas3D').getContext('2d'); 
  ctx.beginPath(); 
  ctx.arc(50, 50, 30, 0, Math.PI*2, true);
  ctx.arc(50, 50, 15, 0, Math.PI*2, true);
  ctx.fill("evenodd");
}
draw3D();
/********************************************************************************************************************************************* 3 */


//绘制文本
/**
    font = value
    当前我们用来绘制文本的样式. 这个字符串使用和 CSS font 属性相同的语法. 默认的字体是 10px sans-serif。
    textAlign = value
    文本对齐选项. 可选的值包括：start, end, left, right or center. 默认值是 start。
    textBaseline = value
    基线对齐选项. 可选的值包括：top, hanging, middle, alphabetic, ideographic, bottom。默认值是 alphabetic。
    direction = value
    文本方向。可能的值包括：ltr, rtl, inherit。默认值是 inherit。
 */
function draw41() {
  var ctx = document.getElementById('canvas41').getContext('2d');
  ctx.font = "24px serif";
  ctx.fillText("Hello world", 10, 50);
  var text = ctx.measureText("Hello world"); // TextMetrics object  
  ctx.fillText(text.width, 10+text.width+10, 50);
  
  ctx.strokeText("Hello world", 10, 100);
  ctx.strokeText(text.width, 10+text.width+10, 100);
}
draw41();


/********************************************************************************************************************************************* 4 */

function draw51() {
    var ctx = document.getElementById('canvas51').getContext('2d');
    var img = new Image();
    img.onload = function(){
      ctx.drawImage(img,0,0);
      ctx.beginPath();
      ctx.moveTo(30,96);
      ctx.lineTo(70,66);
      ctx.lineTo(103,76);
      ctx.lineTo(170,15);
      ctx.stroke();
    }
    img.src = 'images/backdrop.png';
  }
draw51();

/*
[缩放 Scaling]
drawImage(image, x, y, width, height)
这个方法多了2个参数：width 和 height，这两个参数用来控制 当像canvas画入时应该缩放的大小

[切片 Slicing]
drawImage(image, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight)
第一个参数和其它的是相同的，都是一个图像或者另一个 canvas 的引用。其它8个参数最好是参照右边的图解，前4个是定义图像源的切片位置和大小，后4个则是定义切片的目标显示位置和大小。

mozImageSmoothingEnabled 属性，值为 false 时，图像不会平滑地缩放。默认是 true 。
cx.mozImageSmoothingEnabled = false;

*/

/********************************************************************************************************************************************* 5 */