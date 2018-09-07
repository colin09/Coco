package com.myboot.web02.handle;

import com.myboot.web02.aspect.HttpAspect;
import com.myboot.web02.domain.Result;
import com.myboot.web02.exception.CoException;
import com.myboot.web02.utils.ResultUtil;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;


@ControllerAdvice
public class ExceptionHandle {

    private final static Logger logger = LoggerFactory.getLogger(HttpAspect.class);

    @ExceptionHandler(value = Exception.class)
    @ResponseBody
    public Result exceptionHandle(Exception ex) {

        if (ex instanceof CoException) {
            CoException exception = (CoException) ex;
            return ResultUtil.error(exception.getCode(), exception.getMessage());
        }
        logger.info("exception:{}" ,ex);
        return ResultUtil.error(500, ex.getMessage());
    }


}
