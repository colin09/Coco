const userService = require('../MgService/mgUserService')

var index = 0;

var fn_signIn = async (ctx, next) => {
    var email = ctx.request.body.email || '',
        pwd = ctx.request.body.password || '';
        console.log(`email:${email}, pwd:${pwd}`);
        //if(email === 'mgr@xx.com' && pwd==='456789'){
        if(pwd==='456789'){
            console.log('sing in ok');

            let cookie = {
                id: index++,
                name: `user_${index}`,
                image: `/static/img/${index % 6}.jpg`
            };
            ctx.cookies.set('fwCookie',Buffer.from(JSON.stringify(cookie)).toString('base64'));
            userService.newUser(email,pwd);

            ctx.render('index.html',{
                title:'free W',
                name: email
            });
        }else{
            console.log('sing in faild');
            ctx.render('signin.html',{
                title:'free W',
                name:'Mgr',
                msg:'sing in faild'
            });
        }
}

var fn_login = async(ctx,next)=>{
    ctx.render('signin.html',{
        title :'free W'
    })
}



module.exports ={
    'POST /signin' : fn_signIn,
    'GET /' : fn_login
}