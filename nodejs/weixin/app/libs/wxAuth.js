const crypto = require('crypto');
const path = require('path');
const url = require('url');

const config = require('../../config');


function sha1(str){
	var shasum = crypto.createHash('sha1');
	shasum.update(str);
	str = shasum.digest('hex');
	return str;
}


function wechatAuth(req,res){
	var query = url.parse(req.url,true).query;
	var signature = query.signature;
	var echostr= query.echostr;
	var timestamp = query['timestamp'];
	var noce = query.noce;

	var reqArray = [noce,timestamp,config.token];
	reqArray.sort();
	var sortStr = reqArray.join('');
	var sha1Str = sha1(sortStr);

	if(signature==sha1Str)
		res.end(echostr);
	else{
		res.end('false');
		console.log('授权失败。');
	}
}


module.exports = wechatAuth;