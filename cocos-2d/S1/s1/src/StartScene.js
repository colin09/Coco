

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
    }
});

var StartScene = cc.Scene.extend({
    onEnter: function () {
        this._super();
        var layer = new StartLayer();

        this.addChild(layer, 1, 1);
    }
});