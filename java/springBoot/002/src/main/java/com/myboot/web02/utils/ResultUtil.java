package com.myboot.web02.utils;

import com.myboot.web02.domain.Result;

public class ResultUtil {

    public static Result success(Object object){
        Result result = new Result();
        result.setCode(200);
        result.setMessage("");
        result.setData(object);

        return result;
    }

    public static Result success( ){
        return success(null);
    }


    public static Result error(Integer code,String msg){
        Result result = new Result();
        result.setCode(code);
        result.setMessage(msg);

        return result;
    }
}
