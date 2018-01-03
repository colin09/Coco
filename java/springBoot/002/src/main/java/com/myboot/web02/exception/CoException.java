package com.myboot.web02.exception;


import com.myboot.web02.enums.ResultEnum;

public class CoException extends RuntimeException {

    private Integer code;

    public CoException(Integer code,String message){
        super(message);
        this.code= code;
    }


    public CoException(ResultEnum resultEnum){
        super(resultEnum.getMessage());
        this.code= resultEnum.getCode();
    }






    public Integer getCode() {
        return code;
    }

    public void setCode(Integer code) {
        this.code = code;
    }
}
