const router = require('express').Router();
const wxAuth = require('../libs/wxAuth');
//const turingRobot = require('../libs/turingRobot');
const autoReply = require('../libs/wxAutoReply');

router.get('/',wxAuth);

router.post('/',function(req,res){
	res.writeHead(200,{'Context-Type':'application/xml'});

	if(req.body.xml.event == 'subscribe'){
		var resMsg = autoReply('text',req.body.xml,'welcome!!!')
		res.end(resMsg);
	}else{
		var info = encodeURI(req.body.xml.content);
		var resMsg = autoReply('text',req.body.xml,info)
		res.end(resMsg);
	}
});


module.exports = router;