const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const nunjucks = require('nunjucks');

require('body-parser-xml')(bodyParser);

//引入token刷新
const getToken = require('./libs/getAccessToken');
getToken();

//引入路由
const weixin = require('./routes/weixin');


//app 配置
const app = express();
app.set('views',path.join(__dirname,'../views'));

//解析 xml
app.use(bodyParser.xml({
	limit:'1MB',
	xmlParseOptions:{
		normalize:true,
		normalizeTags:true,
		explicitArray:false
	}
}));


//启用路由
app.use('/wechat',weixin);




//启用nunjucks模板
app.engine('html',nunjucks.render);
app.set('view engine','html');





app.get('/',function (req,res) {
	res.render('index.html');
})


module.exports = app;