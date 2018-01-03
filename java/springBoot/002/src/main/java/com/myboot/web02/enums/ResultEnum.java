package com.myboot.web02.enums;

public enum ResultEnum {

    UNKNOW_ERROR(500,"未知错误"),
    SUCCESS(200,"成功"),

    CLIENT_ERROR(400,"客户端错误");





    private Integer code;
    private String message;

    ResultEnum(Integer code,String message){
        this.code= code;
        this.message = message;
    }

    public Integer getCode() {
        return code;
    }

    public String getMessage() {
        return message;
    }
}
