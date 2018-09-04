var canvas;
var context;
var programNotes = [];

var isDragging = false;
var previousSelectedNote;

window.onload = function () {
    canvas = document.getElementById("canvasEditer");
    context = canvas.getContext("2d");

    canvas.onmousedown = canvasClick;
    canvas.onmouseup = stopDragging;
    canvas.onmouseout = stopDragging;
    canvas.onmousemove = dragNote;
};


function noteDefault() {
    var note = {
        id: new Date().getTime(),
        type: "text",
        size: 20,
        color: "#ff0000",
        content: "text info",
        rect: {
            x: 10, y: 10,
            w: 400, h: 300,
        },
        isSelected: false,
    };
    return note;
}

function drawCanvasNote() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    for (var i = 0; i < programNotes.length; i++) {
        var note = programNotes[i];
        switch (note.type) {
            case "text": drawText(note); break;
            case "image": drawImage(note); break;
            case "video": drawVideo(note); break;
        }
    }
}


function canvasClick(e) {
    // 取得画布上被单击的点
    var clickX = e.pageX - canvas.offsetLeft;
    var clickY = e.pageY - canvas.offsetTop;

    for (var i = 0; i < programNotes.length; i++) {
        var note = programNotes[i];
        if (isInRect(note.rect, clickX, clickY)) {
            note.isSelected = true;
            previousSelectedNote = note;
            isDragging = true;
            console.log(note.content);
        } else
            note.isSelected = false;
    }
}
function isInRect(rect, x, y) {
    if (x > rect.x
        && x < rect.x + rect.w
        && y > rect.y
        && y < rect.y + rect.h)
        return true;
    else
        return false;
}

function stopDragging() {
    isDragging = false;
}
function dragNote(e) {
    console.log("c: x=" + e.clientX + "  y=" + e.clientY);
    console.log("p: x=" + e.pageX + "  y=" + e.pageY);
    // 判断圆圈是否开始拖拽
    if (isDragging == true) {
        // 判断拖拽对象是否存在
        if (previousSelectedNote != null) {
            // 取得鼠标位置
            var x = e.pageX - canvas.offsetLeft;
            var y = e.pageY - canvas.offsetTop;
            console.log("x: x=" + x + "  y=" + y);

            var rect = previousSelectedNote.rect;
            // 将圆圈移动到鼠标位置
            previousSelectedNote.rect.x = x + rect.w > canvas.width ? canvas.width - rect.w : x;
            previousSelectedNote.rect.y = y + rect.h > canvas.height ? canvas.height - rect.h : y;

            // 更新画布
            drawCanvasNote();
        }
    }
}



$(function () {
    // var program = document.getElementById("cvsEditer");
    // var context = program.getContext("2d");

    $("#menuNewTxt").click(function (e) {
        var def = "他的诗歌作品是高尚的理想主义、完美的艺术的代表，并且罕有地结合了心灵与智慧。";
        var note = noteDefault();
        note.content = def;
        programNotes.push(note);
        drawCanvasNote();
    });
    $("#menuNewImg").click(function (e) {
        var def = "IMAGE";
        var note = noteDefault();
        note.content = def;
        programNotes.push(note);
        drawCanvasNote();
    });
    $("#menuNewVio").click(function (e) {
        var def = "VIDEO";
        var note = noteDefault();
        note.content = def;
        programNotes.push(note);
        drawCanvasNote();
    });
    $("#menuNewDel").click(function (e) {

    });
})


// Canvas - 文本
// font - 定义字体
// fillText(text,x,y) - 在 canvas 上绘制实心的文本
// strokeText(text,x,y) - 在 canvas 上绘制空心的文本
function drawText(note) {
    context.font = note.size + "px Arial";
    context.textAlign = "start";
    //context.textBaseline = "alphabetic";
    context.textBaseline = "top";
    context.fillStyle = note.color;
    //context.fillText(note.content, note.rect.x, note.rect.y, note.rect.w);
    //context.fillText(note.content, note.rect.x, note.rect.y);


    var lineWidth = 0, lineHeight = note.size, lastSubStrIndex = 0, pointY = note.rect.y;
    for (let i = 0; i < note.content.length; i++) {
        lineWidth += context.measureText(note.content[i]).width;
        if (lineWidth > note.rect.w) {//减去initX,防止边界出现的问题
            context.fillText(note.content.substring(lastSubStrIndex, i - 1), note.rect.x, pointY);
            pointY += lineHeight;
            lineWidth = context.measureText(note.content[i]).width;
            lastSubStrIndex = i - 1;
        }
    }
    if (lastSubStrIndex < note.content.length)
        context.fillText(note.content.substring(lastSubStrIndex, note.content.length), note.rect.x, pointY);

    drawBorder(note.rect);
}

function drawImage(note) {
    //context.drawImage(img,sx,sy,swidth,sheight,x,y,width,height);
    context.drawImage(note.content, note.rect.x, note.rect.y, note.rect.w, note.rect.h);
    drawBorder(note.rect);
}

function drawVideo(note) {
    context.drawImage(note.content, note.rect.x, note.rect.y, note.rect.w, note.rect.h);
    drawBorder(note.rect);
}

function drawBorder(rect) {
    context.lineWidth = 1;
    context.strokeStyle = "#333";
    context.strokeRect(rect.x, rect.y, rect.w, rect.h);
}