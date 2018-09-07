var SushiSprite = cc.Sprite.extend({
    onEnter: function () {
        cc.log("onEnter");
        this._super();

        this.disappearAction = this.createDisappearAction();
        this.disappearAction.retain();
    },
    onExit: function () {
        this.disappearAction.release();
        cc.log("onExit");
    },
    addTouchEventListenser: function () {
        this.touchListener = cc.EventListener.create({
            event: cc.EventListener.TOUCH_ONE_BY_ONE,
            // When "swallow touches" is true, then returning 'true' from the onTouchBegan method will "swallow" the touch event, preventing other listeners from using it.
            swallowTouches: true,
            //onTouchBegan event callback function                      
            onTouchBegan: function (touch, event) {
                var pos = touch.getLocation();
                var target = event.getCurrentTarget();
                if (cc.rectContainsPoint(target.getBoundingBox(), pos)) {
                    cc.log("touched")
                    return true;
                }
                return false;
            }
        });
        cc.eventManager.addListener(this.touchListener, this);
    },
    createDisappearAction : function() {
        var frames = [];
        for (var i = 0; i < 11; i++) {
            var str = "sushi_1n_"+i+".png"
            //cc.log(str);
            var frame = cc.spriteFrameCache.getSpriteFrame(str);
            frames.push(frame);
        }
    
        var animation = new cc.Animation(frames, 0.02);
        var action = new cc.Animate(animation);
    
        return action;
    },

});