
var Menus = cc.Layer.extend({
    ctor: function () {
        this._super();
        this.init();
    },
    init: function () {
        var size = cc.winSize;

        var lc = new cc.LayerColor(cc.color(0, 0, 255, 80), 20 ,20);
        lc.x = 0;
        lc.y = 0;
        this.addChild(lc, 1);

        var toolLayer = new cc.LayerColor(cc.color(255, 0, 0, 80), size.width, 120);
        toolLayer.x = 0;
        toolLayer.y = size.height - 120;
        this.addChild(toolLayer, 1);

        var progreamTookit = new ProgramMenu();
        progreamTookit.x = 0;
        progreamTookit.y = size.height - 30;
        this.addChild(progreamTookit, 2);


        var progreamTookit = new ProgramItemMenu();
        progreamTookit.x = 0;
        progreamTookit.y = size.height - 60;
        this.addChild(progreamTookit, 2);
    }
})