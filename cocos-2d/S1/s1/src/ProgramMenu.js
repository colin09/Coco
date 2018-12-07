/*******************************************
 * [ProgramMenu]
 * 
 *  programs opearate menu
 * 
 *  new page
 * 
 *  move page [up,down]
 *  
 *  remove page
 * 
 */

var ProgramMenu = cc.Layer.extend({
    ctor: function () {
        this._super();
        this.init();
    },
    init: function () {
        var size = cc.winSize;

        var lc = new cc.LayerColor(cc.color(0, 0, 255, 80), 20, 20);
        lc.attr({ x: 0, y: 0 });
        this.addChild(lc, 1);

        cc.MenuItemFont.setFontName("Arial");
        cc.MenuItemFont.setFontSize(14);

        //var label = cc.LabelBMFont.create("new Page", "Arial");
        //var item1 = cc.MenuItemLabel.create("newPage", function (sender) {
        var item1 = cc.MenuItemFont.create("NEWPAGE", function (sender) {
            var id = new Date().getTime();
            cc.log("newPage, Id :" + id);
        }, this);
        var item2 = cc.MenuItemFont.create("MOVEDOWN", function (sender) {
            cc.log("program[?] down");
        }, this);
        var item3 = cc.MenuItemFont.create("CLEAR", function (sender) {
            cc.log("program[?] remove");
            var children = cc.director.getRunningScene().getChildByTag(1).getChildByTag(103).getChildByTag(900).children;
            if (children != null) {
                for (var i = children.length-1; i >= 0; i--) {
                    children[i].removeFromParent();
                }
                /*
                $.each(children, function (index, item) {
                    item.removeFromParent();
                });*/
            }
        }, this);

        item1.attr({ anchorX: 0, anchorY: 0, x: 2, y: 0 });
        item2.attr({ anchorX: 0, anchorY: 0, x: 102, y: 0 });
        item3.attr({ anchorX: 0, anchorY: 0, x: 202, y: 0 });

        var menu = cc.Menu.create(item1, item2, item3);

        // item1.setPosition(2, 0);
        // item2.setPosition(102, 0);
        // item3.setPosition(202, 0);

        //menu.setPosition(0, 0);
        menu.attr({ anchorX: 0, anchorY: 0, x: 0, y: 0 });

        this.addChild(menu);
    }
});


