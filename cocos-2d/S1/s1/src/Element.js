
/*******************************************
* [Element]
* 
* program item opearate menu
* 
*  content, font, size, color, 
*  
*  clear
* 
*/


cc.Element = cc.Class.extend({
    // _anchorX: 0,
    // _anchorY: 0,
    // _x: 0,
    // _y: 0,
    // _w: 100,
    // _h: 100,
    _screen: null,
    _screenWidth: 0, //_screen.widht,
    _screenHeight: 0, // _screen.height,
    _selected: false,
    _offset: null,


    _size: null,
    _position: null,
    _anchorPoint: null,

    _type: "",
    _content: "",

    _font: "Arial",
    _fontSize: 14,
    _fontColor: null,

    ctor: function (type) {
        this._type = type;

        this._size = cc.size(100, 100);
        this._position = cc.p(0, 0);
        this._anchorPoint = cc.p(0, 0);
        this._fontColor = cc.color(255, 0, 0, 255)

        this._screen = cc.director.getRunningScene().getChildByTag(1).getChildByTag(103).getChildByTag(900);
        this._screenWidth = this._screen.width;
        this._screenHeight = this._screen.height;
        this._offset = cc.p(0, 0);
    },
    createElement: function () {
        var layer = new cc.LayerColor(cc.color(0, 0, 255, 80), this._size.width, this._size.height);
        layer.setPosition(this._position);
        layer.setAnchorPoint(this._anchorPoint);

        var sprite = this.createSprite();

        layer.name = "Element_" + sprite.name;
        layer.addChild(sprite, 1);
        this.addTouchEventListenser(layer);
        return layer;
    },
    createSprite: function () {
        //var sprite = null;
        /**
            Text / Image / Video / Time 
            Clock / Calendar / DataList / 
            Weather / News / Stock /
            Flash / PPT /
         */ // 创建纹理
        var texture = cc.textureCache.addImage(res.empty);
        //指定纹理创建精灵
        //var sp1 = new cc.Sprite(texture);
        //指定纹理和裁剪的矩形区域来创建精灵
        var sprite = new cc.Sprite(texture, cc.rect(0, 0, this._size.width, this._size.height));
        sprite.setPosition(this._position);
        sprite.setAnchorPoint(this._anchorPoint);
        sprite.name = this._type + "_" + new Date().getTime();
        //执行动画
        var animation = this.createSpriteAnimation();
        sprite.runAction(cc.Animate.create(animation).repeatForever());
        return sprite;
    },
    createSpriteAnimation: function () {
        var animationRes = "res/run/run-";
        switch (this._type.toLowerCase()) {
            case "text": animationRes = "res/run/run-"; break;
            case "image": animationRes = "res/horse/horse-"; break;
            case "video":
            case "time":

            case "clock":
            case "calendar":
            case "datalist":

            case "weather":
            case "news":
            case "stock":

            case "flash":
            case "ppt":
                break;
        }

        var animation = cc.Animation.create();
        animation.setDelayPerUnit(0.1);
        //动画播放完成是否保持在第一帧
        animation.setRestoreOriginalFrame(true);
        //添加动画的每一帧
        for (var a = 1; a < 17; a++) {
            animation.addSpriteFrameWithFile(animationRes + a + ".png");
        }
        return animation;
    },
    addTouchEventListenser: function (layer) {
        var that = this;
        var touchListener = cc.EventListener.create({
            event: cc.EventListener.TOUCH_ONE_BY_ONE,
            swallowTouches: true,
            onTouchMoved: function (touch, event) {
                //this.onTouchesMoved(touch, event);
                cc.log(layer.name + " onTouchMoved");

                var pos = touch.getLocation();
                var posOld = touch.getPreviousLocation();

                if (that._selected) {
                    var newPos = cc.p(pos.x - that._offset.x, pos.y - that._offset.y);
                    that._position = that.checkTouchBounder(newPos);
                    //that._position = newPos;
                    layer.setPosition(that._position);
                }
            },
            onTouchEnded: function (touch, event) {
                //this.onTouchesEnded(touch, event);
                cc.log(layer.name + " onTouchEnded");
                that._selected = false;
            },
            onTouchBegan: function (touch, event) {
                //this.onTouchesBegan(touch,event);

                var pos = touch.getLocation();
                var target = event.getCurrentTarget();
                if (cc.rectContainsPoint(target.getBoundingBox(), pos)) {
                    //target.removeTouchEventListenser();
                    //响应精灵点中
                    that._selected = true;
                    that._offset = cc.p(pos.x - that._position.x, pos.y - that._position.y);

                    cc.log(layer.name + ", pos.x=" + pos.x + ",pos.y=" + pos.y);
                    cc.log("_offset , pos.x=" + pos.x + ",pos.y=" + pos.y);
                    return true;
                }

                return false;
            }
        });
        cc.eventManager.addListener(touchListener, layer);
    },
    checkTouchBounder: function (pos) {
        //var size = cc.winSize;
        if (pos.x < 0)
            pos.x = 0;
        if (pos.y < 0)
            pos.y == 0;
        if (pos.x > this._screenWidth - this._size.width)
            pos.x = this._screenWidth - this._size.width;
        if (pos.y > this._screenHeight - this._size.height)
            pos.y = this._screenHeight - this._size.height;
        return pos;
    }

});