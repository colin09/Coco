
/*******************************************
 * [ProgramItemMenu]
 * 
 * one program page opearate menu
 * 
 *  new Text / Image / Video / Time 
 *      Clock / Calendar / DataList / 
 *      Weather / News / Stock /
 *      Flash / PPT /
 *  
 *  remove
 * 
* 
    var screenWidth = $("#divEditer").data("width");
    var screenHeight = $("#divEditer").data("height");
 * 
 */


var ProgramItemMenu = cc.Layer.extend({
    _screen: null, //cc.director.getRunningScene().getChildByTag(1).getChildByTag(103).getChildByTag(900),
    _screenWidth: 0, //_screen.widht,
    _screenHeight: 0, // _screen.height,

    ctor: function () {
        this._super();
        this.init();
    },
    init: function () {

        var lc = new cc.LayerColor(cc.color(0, 0, 255, 80), 20, 20);
        lc.attr({ x: 0, y: 0 });
        this.addChild(lc, 1);

        cc.MenuItemFont.setFontName("Arial");
        cc.MenuItemFont.setFontSize(14);

        var menuPointX = 0;
        menuPointX += this.addMenuI(menuPointX);
        menuPointX += this.addMenuII(menuPointX);
        menuPointX += this.addMenuIII(menuPointX);
        menuPointX += this.addMenuIIII(menuPointX);
    },
    addMenuI: function (menuPointX) {
        var pointX = 0;

        var newText = this.createMenuItem("TEXT", pointX);
        pointX += newText.width + 10;
        var newImage = this.createMenuItem("IMAGE", pointX);
        pointX += newImage.width + 10;
        var newVideo = this.createMenuItem("VIDEO", pointX);
        pointX += newVideo.width + 10;
        var newTime = this.createMenuItem("Time", pointX);
        pointX += newTime.width + 10;

        var menu = cc.Menu.create(newText, newImage, newVideo, newTime);
        menu.attr({ anchorX: 0, anchorY: 0, x: menuPointX, y: 0 });
        this.addChild(menu);

        return pointX;
    },
    addMenuII: function (menuPointX) {
        var pointX = 0;

        var newClock = this.createMenuItem("CLOCK", pointX);
        pointX += newClock.width + 10;
        var newCalendar = this.createMenuItem("CALENDAR", pointX);
        pointX += newCalendar.width + 10;
        var newDataList = this.createMenuItem("DATA", pointX);
        pointX += newDataList.width + 10;

        var menu = cc.Menu.create(newClock, newCalendar, newDataList);
        menu.attr({ anchorX: 0, anchorY: 0, x: menuPointX, y: 0 });
        this.addChild(menu);

        return pointX;
    },
    addMenuIII: function (menuPointX) {
        var pointX = 0;

        var newWeather = this.createMenuItem("WEATHER", pointX);
        pointX += newWeather.width + 10;
        var newNews = this.createMenuItem("NEWS", pointX);
        pointX += newNews.width + 10;
        var newStock = this.createMenuItem("STOCK", pointX);
        pointX += newStock.width + 10;

        var menu = cc.Menu.create(newWeather, newNews, newStock);
        menu.attr({ anchorX: 0, anchorY: 0, x: menuPointX, y: 0 });
        this.addChild(menu);

        return pointX;
    },
    addMenuIIII: function (menuPointX) {
        var pointX = 0;

        var newFlash = this.createMenuItem("Flash", pointX);
        pointX += newFlash.width + 10;
        var newPPT = this.createMenuItem("PPT", pointX);
        pointX += newPPT.width + 10;

        var menu = cc.Menu.create(newFlash, newPPT);
        menu.attr({ anchorX: 0, anchorY: 0, x: menuPointX, y: 0 });
        this.addChild(menu);

        return pointX;
    },
    createMenuItem: function (type, pointX) {
        var menuItem = cc.MenuItemFont.create(type.toUpperCase(), function (sender) {
            var id = new Date().getTime();
            cc.log("add " + type + ", Id :" + id);
            this.createSprite(type);
        }, this);
        menuItem.attr({ anchorX: 0, anchorY: 0, x: pointX, y: 0 });
        return menuItem;
    },
    createSprite: function (type) {
        /*
        var sprite = new cc.Sprite(res.empty);
        var sprite = new cc.Sprite(res.empty);
        sprite.attr({
            anchorX: 0,
            anchorY: 0,
            x: 0,
            y: 0,
            // width: 100,
            // height: 100
        });
        sprite.setColor(255, 0, 0, 90);
        sprite.setTextureRect(cc.rect(0, 0, 100, 100));
        */
        var element = new cc.Element(type);
        var sprite = element.createElement();
        this.addToScreen(sprite);
    },
    addToScreen: function (sprite) {
        //因为此刻this为ControlLayer对象 用this.addchild(cover,2);显示不出来精灵
        // var self = cc.director.getRunningScene().getChildByTag(1);//通过获取scene然后用getChildByTag来得到MainLayer对象
        // cc.log(self);
        this._screen = cc.director.getRunningScene().getChildByTag(1).getChildByTag(103).getChildByTag(900);
        this._screenWidth = this._screen.widht;
        this._screenHeight = this._screen.height;
        this._screen.addChild(sprite, 210);
    }

});
