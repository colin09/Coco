const request = require('request');
const qs = require('querystring');
const fs = require('fs');
const config = require('../../config');
const isExsitSync = require('./util').isExsitSync;

const getAccessToken = function(){
	"use strict";
	let queryParams = {
		'grant_type':'client_credential',
		'appid':config.appId,
		'secret':config.appSecret
	};

	let wxGetAccessTokenUrl = 'https://api.weixin.qq.com/cgi-bin/token?'+qs.stringify(queryParams);

	let options = {
		method: 'GET',
		url : wxGetAccessTokenUrl
	};

	return new Promise((resolve,reject)=>{
		request(options,function(err,res,body){
			if(res){
				resolve(JSON.parse(body));
			}else{
				reject(err);
			}
		});
	});
};


const saveToken = function(){
	getAccessToken().then(res=>{
		"use strict";
		let token = res['access_token'];
		fs.writeFile('./token',token,function(err){
			console.log('save access_token err:'+err);
		});
	});
};


const refreshToken = function(){
	saveToken();
	setInterval(function(){
		saveToken();
	},7000*1000);
};




module.exports = refreshToken;