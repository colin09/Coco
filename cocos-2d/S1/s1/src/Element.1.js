
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

    _selected: false,


    _size: null,
    _position: null,
    _anchorPoint: null,

    _type: "",
    _content: "",

    _font: "Arial",
    _fontSize: 14,
    _fontColor: null,

    ctor: function (type) {
        this._size = cc.size(100, 100);
        this._position = cc.p(0, 0);
        this._anchorPoint = cc.p(0, 0);

        //this.type = "text";
        this._type = type;

        this._fontColor = cc.color(255, 0, 0, 255)

        //this.createElement();
    },
    createElement: function () {
        var layer = new cc.LayerColor(cc.color(0, 0, 255, 80), this._size.width, this._size.height);
        //layer.attr({ anchorX: 0, anchorY: 0 });
        //layer.attr({ x: 0, y: 0 });
        layer.setPosition(this._position);
        layer.setAnchorPoint(this._anchorPoint);

        var sprite = this.createSprite();
        sprite.name = this._type + "_" + new Date().getTime();
        this.addTouchEventListenser(sprite);


        layer.name = "Element_" + sprite.name;
        layer.addChild(sprite, 1);

        return layer;
    },
    createSprite: function () {
        var sprite = null;
        /**
            Text / Image / Video / Time 
            Clock / Calendar / DataList / 
            Weather / News / Stock /
            Flash / PPT /
         */
        switch (this._type.toLowerCase()) {
            case "text":
            case "image":
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
                sprite = this.createTextSprite();
                break;
        }
        sprite.attr({
            anchorX: 0,
            anchorY: 0,
            x: 0,
            y: 0,
        });
        return sprite;
    },
    createTextSprite: function () {
        var sourceW = res.empty.width;
        var sourceH = res.empty.height;
        //var sprite = new cc.Sprite(res.empty);
        //var sprite = new cc.Sprite();
        //缩放精灵大小以适配 LayerColor.size


        var texture = cc.textureCache.addImage(res.empty);
        //指定纹理创建精灵
        //var sp1 = new cc.Sprite(texture);
        //指定纹理和裁剪的矩形区域来创建精灵
        var sprite = new cc.Sprite(texture, cc.rect(0, 0, this._size.width, this._size.height));


        return sprite;
    },
    addTouchEventListenser: function (sprite) {
        var touchListener = cc.EventListener.create({
            event: cc.EventListener.TOUCH_ONE_BY_ONE,
            swallowTouches: true,
            onTouchMoved: function (touch, event) {
                //this.onTouchesMoved(touch, event);
                cc.log(sprite.name + " onTouchMoved");

                var pos = touch.getLocation();
                var posOld = touch.getPreviousLocation();
                var elementName = "Element_" + sprite.name;
                if (this._selected) {
                    sprite.parent.setPosition(pos);
                    sprite.setPosition(pos);
                }
            },
            onTouchEnded: function (touch, event) {
                //this.onTouchesEnded(touch, event);
                cc.log(sprite.name + " onTouchEnded");
                this._selected = false;
            },
            onTouchBegan: function (touch, event) {
                //this.onTouchesBegan(touch,event);

                var pos = touch.getLocation();
                var target = event.getCurrentTarget();
                if (cc.rectContainsPoint(target.getBoundingBox(), pos)) {
                    //target.removeTouchEventListenser();
                    //响应精灵点中
                    cc.log(sprite.name + ",pos.x=" + pos.x + ",pos.y=" + pos.y);
                    this._selected = true;
                }

                return true;
            }
        });
        cc.eventManager.addListener(touchListener, sprite);
    }

});