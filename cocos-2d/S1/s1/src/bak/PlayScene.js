var PlayLayer = cc.Layer.extend({
    bgSprite: null,
    ctor: function () {
        this._super();

        var size = cc.winSize;

        // add bg
        this.bgSprite = new cc.Sprite(res.bg_2);
        this.bgSprite.attr({
            x: size.width / 2,
            y: size.height / 2,
            //scale: 0.5,
            rotation: 180,
            SushiSprites: null
        });
        this.SushiSprites = [];
        this.addChild(this.bgSprite, 0);

        //this.addSushi();
        /**
         * schedule(callback_fn, interval, repeat, delay)
         * scheduleOnce(callback_fn, delay) 该函数只调用一次callback_fn的方法
         * scheduleUpdate()该函数会每一帧都调用，调用的方法名为"update"
         * 
         * callback_fn：调用的方法名
         * interval：间隔多久再进行调用(秒)
         * repeat：重复的次数
         * delay：延迟多久再进行调(秒)
         */
        this.schedule(this.update, 1, 16 * 1024, 1);

        return true;
    },
    update: function () {
        this.addSushi();
        this.removeSushi();
    },
    addSushi: function () {
        // var sushi = new cc.Sprite(res.del);
        var sushi = new SushiSprite(res.del);
        var size = cc.winSize;

        var x = sushi.width / 2 + size.width / 2 * cc.random0To1();
        sushi.attr({ x: x, y: size.height - 30, disappearAction: null });
        this.SushiSprites.push(sushi);
        this.addChild(sushi, 5);

        var dropAction = cc.MoveTo.create(5, cc.p(sushi.x, -30));
        sushi.runAction(dropAction);
    },
    removeSushi: function () {
        //移除到屏幕底部的sushi
        for (var i = 0; i < this.SushiSprites.length; i++) {
            cc.log("removeSushi.........");
            if (0 >= this.SushiSprites[i].y) {
                cc.log("==============remove:" + i);
                this.SushiSprites[i].removeFromParent();
                this.SushiSprites[i] = undefined;
                this.SushiSprites.splice(i, 1);
                i = i - 1;
            }
        }
    }
});

var PlayScene = cc.Scene.extend({
    onEnter: function () {
        this._super();
        var layer = new PlayLayer();
        this.addChild(layer);
    }
});