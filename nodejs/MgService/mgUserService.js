const mgClient =  require('./mgClient');
const DBName = 'mgUser';

var init_user = {
    //_id:'',
    name:'',
    pwd:'',
    phone:'',
    email:'',
    cTime: new Date(),
    uTime: new Date(),
    uUser:0
};


var fn_addUser = function(email,pwd){
    var m = init_user;
    m.name=email.substring(0,email.indexOf('@'));
    m.pwd=pwd;
    m.email = email
    mgClient.insert(DBName,m,function(result){
        console.log(result);
    })
};




module.exports ={
    newUser : fn_addUser
}