

var StartLayer = cc.Layer.extend({
    sprite: null,
    ctor: function () {
        this._super();
        var size = cc.winSize;

        // add bg
        this.bgSprite = new cc.Sprite(res.bg_2);
        this.bgSprite.attr({
            x: size.width / 2,
            y: size.height / 2,
            name: "bgSprite"
        });
        this.addChild(this.bgSprite, 0, 101);

        var toolkit = new Menus();
        this.addChild(toolkit, 10, 102);

        var screen = new Screen1();
        this.addChild(screen, 10, 103);

        this.test();




        /*
        //add toolkit
        var toolLayer = new cc.LayerColor(cc.color(255, 0, 0, 80), size.width, 75);
        toolLayer.x = 0;
        toolLayer.y = size.height - 75 - 2;
        this.addChild(toolLayer, 1);
        //add program
        var programLayer = new cc.LayerColor(cc.color(0, 0, 255, 80), size.width, size.height-80);
        programLayer.x = 0;
        programLayer.y = 0;
        this.addChild(programLayer, 2);

        */





        return true;
    },
    test: function () {
        var texture = cc.textureCache.addImage(res.empty);
        //指定纹理创建精灵
        //var sp1 = new cc.Sprite(texture);
        //指定纹理和裁剪的矩形区域来创建精灵
        var sprite = new cc.Sprite(texture, cc.rect(0, 0, 125, 125));

        var animation = cc.Animation.create();
        animation.setDelayPerUnit(0.1);
        //动画播放完成是否保持在第一帧
        animation.setRestoreOriginalFrame(true);
        //添加动画的每一帧
        for (var a = 1; a < 17; a++) {
            animation.addSpriteFrameWithFile("/res/run/run-" + a + ".png");
        }
        //sprite.runAction(cc.Repeat.create(cc.Animate.create(animation)));
        sprite.runAction(cc.Animate.create(animation).repeatForever());
        sprite.setPosition(cc.p(200,200));
        this.addChild(sprite, 10);
    }
});

var StartScene = cc.Scene.extend({
    onEnter: function () {
        this._super();
        var layer = new StartLayer();

        this.addChild(layer, 1, 1);
    }
});