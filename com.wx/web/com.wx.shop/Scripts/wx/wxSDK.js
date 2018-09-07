/*
* 注意：
* 1. 所有的JS接口只能在公众号绑定的域名下调用，公众号开发者需要先登录微信公众平台进入“公众号设置”的“功能设置”里填写“JS接口安全域名”。
* 2. 如果发现在 Android 不能分享自定义内容，请到官网下载最新的包覆盖安装，Android 自定义分享接口需升级至 6.0.2.58 版本及以上。
* 3. 完整 JS-SDK 文档地址：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
*
* 如有问题请通过以下渠道反馈：
* 邮箱地址：weixin-open@qq.com
* 邮件主题：【微信JS-SDK反馈】具体问题
* 邮件内容说明：用简明的语言描述问题所在，并交代清楚遇到该问题的场景，可附上截屏图片，微信团队会尽快处理你的反馈。
*/

$(function () {
    //wxSubmitConfigReady();
});

function wxSubmitConfig() { }

wx.config({
    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
    appId: $("#configAppId").val(), // 必填，公众号的唯一标识
    timestamp: $("#configTimestamp").val(), // 必填，生成签名的时间戳
    nonceStr: $("#configNonce").val(), // 必填，生成签名的随机串
    signature: $("#configSignature").val(), // 必填，签名，见附录1
    jsApiList: [
        'checkJsApi',
        'onMenuShareTimeline',
        'onMenuShareAppMessage',
        'onMenuShareQQ',
        'onMenuShareWeibo'
        ] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
});




wx.ready(function () {
    // 1 判断当前版本是否支持指定 JS 接口，支持批量判断
    //document.querySelector('#checkJsApi').onclick = function () {};

    wx.checkJsApi({
        jsApiList: [
        'onMenuShareTimeline',
        'onMenuShareAppMessage',
        'onMenuShareQQ',
        'onMenuShareWeibo'
        ],
        success: function (res) {
            //alert("r" + JSON.stringify(res));
        }
    });


    // 2. 分享接口
    // 2.1 监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
    //document.querySelector('#onMenuShareAppMessage').onclick = function () { alert('已注册获取“发送给朋友”状态事件');};
    wx.onMenuShareAppMessage({
        title: '药城送福利啦！商品免费兑换！',
        desc: '嗨，我已成为药城尊贵会员，首次注册、每日签到可获得积分，积分可取代现金在‘兑换区’免费兑换商品，一起参加吧。',
        link: 'http://weixin.yaochengwang.com/pass/r.aspx?r=' + $("#personCode").val(),
        imgUrl: $("#imgPersonCodeImg").attr("src"),
        trigger: function (res) {
            // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
            //alert('用户点击发送给朋友');
        },
        success: function (res) {
            //alert('已分享');
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });


    // 2.2 监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
    //document.querySelector('#onMenuShareTimeline').onclick = function () { alert('已注册获取“分享到朋友圈”状态事件');};
    wx.onMenuShareTimeline({
        title: '药城送福利啦！商品免费兑换！',
        link: 'http://weixin.yaochengwang.com/pass/r.aspx?r=' + $("#personCode").val(),
        imgUrl: $("#imgPersonCodeImg").attr("src"),
        trigger: function (res) {
            // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
            //alert('用户点击分享到朋友圈');
        },
        success: function (res) {
            //alert('已分享');
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });


    // 2.3 监听“分享到QQ”按钮点击、自定义分享内容及分享结果接口
    //document.querySelector('#onMenuShareQQ').onclick = function () {alert('已注册获取“分享到 QQ”状态事件');};
    wx.onMenuShareQQ({
        title: '药城送福利啦！商品免费兑换！',
        desc: '嗨，我已成为药城尊贵会员，首次注册、每日签到可获得积分，积分可取代现金在‘兑换区’免费兑换商品，一起参加吧。',
        link: 'http://weixin.yaochengwang.com/pass/r.aspx?r=' + $("#personCode").val(),
        imgUrl: $("#imgPersonCodeImg").attr("src"),
        trigger: function (res) {
            //alert('用户点击分享到QQ');
        },
        complete: function (res) {
            //alert(JSON.stringify(res));
        },
        success: function (res) {
            //alert('已分享');
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });



    // 2.4 监听“分享到微博”按钮点击、自定义分享内容及分享结果接口
    //document.querySelector('#onMenuShareWeibo').onclick = function () { alert('已注册获取“分享到微博”状态事件'); };
    wx.onMenuShareWeibo({
        title: '药城送福利啦！商品免费兑换！',
        desc: '上药城，买放心药。请点击下面的链接，注册成为‘药城’尊贵会员，立即得到对应积分奖励，每日签到还可获得相应签到积分，积分可在‘兑换区’免费兑换商品，还在等什么，快点来戳。',
        link: 'http://weixin.yaochengwang.com/pass/r.aspx?r=' + $("#personCode").val(),
        imgUrl: $("#imgPersonCodeImg").attr("src"),
        trigger: function (res) {
            //alert('用户点击分享到微博');
        },
        complete: function (res) {
            //alert(JSON.stringify(res));
        },
        success: function (res) {
            //alert('已分享');
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });



    /*
    // 3 智能接口
    var voice = {
    localId: '',
    serverId: ''
    };
    // 3.1 识别音频并返回识别结果
    document.querySelector('#translateVoice').onclick = function () {
    if (voice.localId == '') {
    alert('请先使用 startRecord 接口录制一段声音');
    return;
    }
    wx.translateVoice({
    localId: voice.localId,
    complete: function (res) {
    if (res.hasOwnProperty('translateResult')) {
    alert('识别结果：' + res.translateResult);
    } else {
    alert('无法识别');
    }
    }
    });
    };

    // 4 音频接口
    // 4.2 开始录音
    document.querySelector('#startRecord').onclick = function () {
    wx.startRecord({
    cancel: function () {
    alert('用户拒绝授权录音');
    }
    });
    };

    // 4.3 停止录音
    document.querySelector('#stopRecord').onclick = function () {
    wx.stopRecord({
    success: function (res) {
    voice.localId = res.localId;
    },
    fail: function (res) {
    alert(JSON.stringify(res));
    }
    });
    };

    // 4.4 监听录音自动停止
    wx.onVoiceRecordEnd({
    complete: function (res) {
    voice.localId = res.localId;
    alert('录音时间已超过一分钟');
    }
    });

    // 4.5 播放音频
    document.querySelector('#playVoice').onclick = function () {
    if (voice.localId == '') {
    alert('请先使用 startRecord 接口录制一段声音');
    return;
    }
    wx.playVoice({
    localId: voice.localId
    });
    };

    // 4.6 暂停播放音频
    document.querySelector('#pauseVoice').onclick = function () {
    wx.pauseVoice({
    localId: voice.localId
    });
    };

    // 4.7 停止播放音频
    document.querySelector('#stopVoice').onclick = function () {
    wx.stopVoice({
    localId: voice.localId
    });
    };

    // 4.8 监听录音播放停止
    wx.onVoicePlayEnd({
    complete: function (res) {
    alert('录音（' + res.localId + '）播放结束');
    }
    });

    // 4.8 上传语音
    document.querySelector('#uploadVoice').onclick = function () {
    if (voice.localId == '') {
    alert('请先使用 startRecord 接口录制一段声音');
    return;
    }
    wx.uploadVoice({
    localId: voice.localId,
    success: function (res) {
    alert('上传语音成功，serverId 为' + res.serverId);
    voice.serverId = res.serverId;
    }
    });
    };

    // 4.9 下载语音
    document.querySelector('#downloadVoice').onclick = function () {
    if (voice.serverId == '') {
    alert('请先使用 uploadVoice 上传声音');
    return;
    }
    wx.downloadVoice({
    serverId: voice.serverId,
    success: function (res) {
    alert('下载语音成功，localId 为' + res.localId);
    voice.localId = res.localId;
    }
    });
    };

    // 5 图片接口
    // 5.1 拍照、本地选图
    var images = {
    localId: [],
    serverId: []
    };
    document.querySelector('#chooseImage').onclick = function () {
    wx.chooseImage({
    success: function (res) {
    images.localId = res.localIds;
    alert('已选择 ' + res.localIds.length + ' 张图片');
    }
    });
    };

    // 5.2 图片预览
    document.querySelector('#previewImage').onclick = function () {
    wx.previewImage({
    current: 'http://img5.douban.com/view/photo/photo/public/p1353993776.jpg',
    urls: [
    'http://img3.douban.com/view/photo/photo/public/p2152117150.jpg',
    'http://img5.douban.com/view/photo/photo/public/p1353993776.jpg',
    'http://img3.douban.com/view/photo/photo/public/p2152134700.jpg'
    ]
    });
    };
    
    // 5.3 上传图片
    document.querySelector('#uploadImage').onclick = function () {
    if (images.localId.length == 0) {
    alert('请先使用 chooseImage 接口选择图片');
    return;
    }
    var i = 0, length = images.localId.length;
    images.serverId = [];
    function upload() {
    wx.uploadImage({
    localId: images.localId[i],
    success: function (res) {
    i++;
    alert('已上传：' + i + '/' + length);
    images.serverId.push(res.serverId);
    if (i < length) {
    upload();
    }
    },
    fail: function (res) {
    alert(JSON.stringify(res));
    }
    });
    }
    upload();
    };

    // 5.4 下载图片
    document.querySelector('#downloadImage').onclick = function () {
    if (images.serverId.length === 0) {
    alert('请先使用 uploadImage 上传图片');
    return;
    }
    var i = 0, length = images.serverId.length;
    images.localId = [];
    function download() {
    wx.downloadImage({
    serverId: images.serverId[i],
    success: function (res) {
    i++;
    alert('已下载：' + i + '/' + length);
    images.localId.push(res.localId);
    if (i < length) {
    download();
    }
    }
    });
    }
    download();
    };

    // 6 设备信息接口
    // 6.1 获取当前网络状态
    document.querySelector('#getNetworkType').onclick = function () {
    wx.getNetworkType({
    success: function (res) {
    alert(res.networkType);
    },
    fail: function (res) {
    alert(JSON.stringify(res));
    }
    });
    };

    // 7 地理位置接口
    // 7.1 查看地理位置
    document.querySelector('#openLocation').onclick = function () {
    wx.openLocation({
    latitude: 23.099994,
    longitude: 113.324520,
    name: 'TIT 创意园',
    address: '广州市海珠区新港中路 397 号',
    scale: 14,
    infoUrl: 'http://weixin.qq.com'
    });
    };

    // 7.2 获取当前地理位置
    document.querySelector('#getLocation').onclick = function () {
    wx.getLocation({
    success: function (res) {
    alert(JSON.stringify(res));
    },
    cancel: function (res) {
    alert('用户拒绝授权获取地理位置');
    }
    });
    };

    // 8 界面操作接口
    // 8.1 隐藏右上角菜单
    document.querySelector('#hideOptionMenu').onclick = function () {
    wx.hideOptionMenu();
    };

    // 8.2 显示右上角菜单
    document.querySelector('#showOptionMenu').onclick = function () {
    wx.showOptionMenu();
    };

    // 8.3 批量隐藏菜单项
    document.querySelector('#hideMenuItems').onclick = function () {
    wx.hideMenuItems({
    menuList: [
    'menuItem:readMode', // 阅读模式
    'menuItem:share:timeline', // 分享到朋友圈
    'menuItem:copyUrl' // 复制链接
    ],
    success: function (res) {
    alert('已隐藏“阅读模式”，“分享到朋友圈”，“复制链接”等按钮');
    },
    fail: function (res) {
    alert(JSON.stringify(res));
    }
    });
    };

    // 8.4 批量显示菜单项
    document.querySelector('#showMenuItems').onclick = function () {
    wx.showMenuItems({
    menuList: [
    'menuItem:readMode', // 阅读模式
    'menuItem:share:timeline', // 分享到朋友圈
    'menuItem:copyUrl' // 复制链接
    ],
    success: function (res) {
    alert('已显示“阅读模式”，“分享到朋友圈”，“复制链接”等按钮');
    },
    fail: function (res) {
    alert(JSON.stringify(res));
    }
    });
    };

    // 8.5 隐藏所有非基本菜单项
    document.querySelector('#hideAllNonBaseMenuItem').onclick = function () {
    wx.hideAllNonBaseMenuItem({
    success: function () {
    alert('已隐藏所有非基本菜单项');
    }
    });
    };

    // 8.6 显示所有被隐藏的非基本菜单项
    document.querySelector('#showAllNonBaseMenuItem').onclick = function () {
    wx.showAllNonBaseMenuItem({
    success: function () {
    alert('已显示所有非基本菜单项');
    }
    });
    };

    // 8.7 关闭当前窗口
    document.querySelector('#closeWindow').onclick = function () {
    wx.closeWindow();
    };

    // 9 微信原生接口
    // 9.1.1 扫描二维码并返回结果
    document.querySelector('#scanQRCode0').onclick = function () {
    wx.scanQRCode();
    };
    // 9.1.2 扫描二维码并返回结果
    document.querySelector('#scanQRCode1').onclick = function () {
    wx.scanQRCode({
    needResult: 1,
    desc: 'scanQRCode desc',
    success: function (res) {
    alert(JSON.stringify(res));
    }
    });
    };

    // 10 微信支付接口
    // 10.1 发起一个支付请求
    document.querySelector('#chooseWXPay').onclick = function () {
    // 注意：此 Demo 使用 2.7 版本支付接口实现，建议使用此接口时参考微信支付相关最新文档。
    wx.chooseWXPay({
    timestamp: 1414723227,
    nonceStr: 'noncestr',
    package: 'addition=action_id%3dgaby1234%26limit_pay%3d&bank_type=WX&body=innertest&fee_type=1&input_charset=GBK&notify_url=http%3A%2F%2F120.204.206.246%2Fcgi-bin%2Fmmsupport-bin%2Fnotifypay&out_trade_no=1414723227818375338&partner=1900000109&spbill_create_ip=127.0.0.1&total_fee=1&sign=432B647FE95C7BF73BCD177CEECBEF8D',
    signType: 'SHA1', // 注意：新版支付接口使用 MD5 加密
    paySign: 'bd5b1933cda6e9548862944836a9b52e8c9a2b69'
    });
    };

    // 11.3  跳转微信商品页
    document.querySelector('#openProductSpecificView').onclick = function () {
    wx.openProductSpecificView({
    productId: 'pDF3iY_m2M7EQ5EKKKWd95kAxfNw'
    });
    };

    // 12 微信卡券接口
    // 12.1 添加卡券
    document.querySelector('#addCard').onclick = function () {
    wx.addCard({
    cardList: [
    {
    cardId: 'pDF3iY9tv9zCGCj4jTXFOo1DxHdo',
    cardExt: '{"code": "", "openid": "", "timestamp": "1418301401", "signature":"64e6a7cc85c6e84b726f2d1cbef1b36e9b0f9750"}'
    },
    {
    cardId: 'pDF3iY9tv9zCGCj4jTXFOo1DxHdo',
    cardExt: '{"code": "", "openid": "", "timestamp": "1418301401", "signature":"64e6a7cc85c6e84b726f2d1cbef1b36e9b0f9750"}'
    }
    ],
    success: function (res) {
    alert('已添加卡券：' + JSON.stringify(res.cardList));
    }
    });
    };

    // 12.2 选择卡券
    document.querySelector('#chooseCard').onclick = function () {
    wx.chooseCard({
    cardSign: '97e9c5e58aab3bdf6fd6150e599d7e5806e5cb91',
    timestamp: 1417504553,
    nonceStr: 'k0hGdSXKZEj3Min5',
    success: function (res) {
    alert('已选择卡券：' + JSON.stringify(res.cardList));
    }
    });
    };

    // 12.3 查看卡券
    document.querySelector('#openCard').onclick = function () {
    alert('您没有该公众号的卡券无法打开卡券。');
    wx.openCard({
    cardList: [
    ]
    });
    };

    var shareData = {
    title: '微信JS-SDK Demo',
    desc: '微信JS-SDK,帮助第三方为用户提供更优质的移动web服务',
    link: 'http://demo.open.weixin.qq.com/jssdk/',
    imgUrl: 'http://mmbiz.qpic.cn/mmbiz/icTdbqWNOwNRt8Qia4lv7k3M9J1SKqKCImxJCt7j9rHYicKDI45jRPBxdzdyREWnk0ia0N5TMnMfth7SdxtzMvVgXg/0'
    };
    wx.onMenuShareAppMessage(shareData);
    wx.onMenuShareTimeline(shareData);
    */
});

wx.error(function (res) {
    //alert(res.errMsg);
});












/*
* 注意：
* 1. 所有的JS接口只能在公众号绑定的域名下调用，公众号开发者需要先登录微信公众平台进入“公众号设置”的“功能设置”里填写“JS接口安全域名”。
* 2. 如果发现在 Android 不能分享自定义内容，请到官网下载最新的包覆盖安装，Android 自定义分享接口需升级至 6.0.2.58 版本及以上。
* 3. 常见问题及完整 JS-SDK 文档地址：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
*
* 开发中遇到问题详见文档“附录5-常见错误及解决办法”解决，如仍未能解决可通过以下渠道反馈：
* 邮箱地址：weixin-open@qq.com
* 邮件主题：【微信JS-SDK反馈】具体问题
* 邮件内容说明：用简明的语言描述问题所在，并交代清楚遇到该问题的场景，可附上截屏图片，微信团队会尽快处理你的反馈。
*/
/*
wx.config({
debug: false,
appId: 'wxf8b4f85f3a794e77',
timestamp: 1430369873,
nonceStr: 'BZIkDkXTrtS2TZkn',
signature: 'ed9efe305ca25137460e9bc6ee9490938da94839',
jsApiList: [
'checkJsApi',
'onMenuShareTimeline',
'onMenuShareAppMessage',
'onMenuShareQQ',
'onMenuShareWeibo',
'hideMenuItems',
'showMenuItems',
'hideAllNonBaseMenuItem',
'showAllNonBaseMenuItem',
'translateVoice',
'startRecord',
'stopRecord',
'onRecordEnd',
'playVoice',
'pauseVoice',
'stopVoice',
'uploadVoice',
'downloadVoice',
'chooseImage',
'previewImage',
'uploadImage',
'downloadImage',
'getNetworkType',
'openLocation',
'getLocation',
'hideOptionMenu',
'showOptionMenu',
'closeWindow',
'scanQRCode',
'chooseWXPay',
'openProductSpecificView',
'addCard',
'chooseCard',
'openCard'
]
});
*/

