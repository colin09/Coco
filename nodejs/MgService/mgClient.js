const MongoClient = require('mongodb').MongoClient;
var uri = 'mongodb://username:password@hostname:port/databasename';
const DB_Conn_Str='mongodb://pmp:123456@192.168.1.182:27017/pmp';


var fn_insert = function(dbName,data,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }

        var collection = db.collection(dbName);
        collection.insert(data,function(err,result){
            if(err){
                console.log(`mongodb install error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}

var fn_insertMany = function(dbName,data,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }

        var collection = db.collection(dbName);
        collection.insertMany(data,function(err,result){
            if(err){
                console.log(`mongodb install error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}

var fn_find = function(dbName,filter,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }

        var collection = db.collection(dbName);
        collection.find(filter).toArray(function(err,result){
            if(err){
                console.log(`mongodb find error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}



var fn_update = function(dbName,filter,update,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }

        var collection = db.collection(dbName);
        collection.update(filter,update,function(err,result){
            if(err){
                console.log(`mongodb update error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}




var fn_remove = function(dbName,filter,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }

        var collection = db.collection(dbName);
        collection.remove(filter,function(err,result){
            if(err){
                console.log(`mongodb remove error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}


/**
 * 调用存储过程
 * procName ： get_count()
 */
var fn_invokeProc = function(procName,filter,callback){
    MongoClient.connect(DB_Conn_Str,function(error,db){
        if(error){
            console.log(`mongodb connect error: ${error}`);
            return;
        }
       
        db.eval(procName,function(err,result){
            if(err){
                console.log(`mongodb remove error: ${err}`);
                return;
            }
            callback(result);
        });
        db.close();
    });
}



module.exports = {
    insert: fn_insert,
    find: fn_find,
    update: fn_update,
    remove: fn_remove,
    invokeProc: fn_invokeProc
}