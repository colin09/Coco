
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

        var background = new cc.LayerColor(cc.color(255, 0, 0, 80), size.width, 120);
        background.x = 0;
        background.y = size.height - 120;
        this.addChild(background, 301);

        var programTookit = new ProgramMenu();
        programTookit.x = 0;
        programTookit.y = size.height - 30;
        this.addChild(programTookit, 310);


        var programItemTookit = new ProgramItemMenu();
        programItemTookit.x = 0;
        programItemTookit.y = size.height - 60;
        this.addChild(programItemTookit, 320);
    }
})