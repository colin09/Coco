
var Screen1 = cc.Layer.extend({
    ctor: function () {
        this._super();
        this.init();
    },
    init: function () {
        var size = cc.winSize;

        var lc = new cc.LayerColor(cc.color(0, 0, 255, 80), 20, 20);
        lc.x = 0;
        lc.y = 200;
        this.addChild(lc, 1);

        var screenWidth = $("#divEditer").data("width");
        var screenHeight = $("#divEditer").data("height");

        cc.log("screen: w-" + screenWidth + ", h-" + screenHeight);

        var background = new cc.LayerColor(cc.color(0, 255, 0, 80), screenWidth, screenHeight);
        background.x = 0;
        background.y = 0;
        background.name="layer_screen";
        this.addChild(background, 201, 900);

    }
})