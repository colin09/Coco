package com.myboot.web02.aspect;


import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

import javax.servlet.http.HttpServletRequest;


@Aspect
@Component
public class HttpAspect {

    private final static Logger logger = LoggerFactory.getLogger(HttpAspect.class);
    /*
    @Before("execution(public * com.myboot.web02.controller.NewStoreController.*(..))")
    public  void Log(){
        System.out.println("get session ...");
    }
    @After("execution(public * com.myboot.web02.controller.NewStoreController.*(..))")
    public void logResponse(){
        System.out.println("get session ...");
    }
    */

    @Pointcut("execution(public * com.myboot.web02.controller.NewStoreController.*(..))")
    public void log() {
    }

    @Before("log()")
    public void doBefore(JoinPoint joinPoint) {
        System.out.println("------->");
        logger.info("------->");

        ServletRequestAttributes attributes = (ServletRequestAttributes) RequestContextHolder.getRequestAttributes();
        HttpServletRequest request = attributes.getRequest();

        logger.info("url : {}",request.getRequestURI());
        logger.info("method : {}",request.getMethod());
        logger.info("ip : {}",request.getRemoteAddr());
        logger.info("class_m : {}",joinPoint.getSignature().getDeclaringTypeName()+""+joinPoint.getSignature().getName());
        logger.info("args={}",joinPoint.getArgs());
    }

    @After("log()")
    public void doAfter() {
        System.out.println("-------||");
        logger.info("-------||");
    }


    @AfterReturning(returning = "object", pointcut = "log()")
    public void doAfterReturning(Object object){
        logger.info("response={}",object.toString());
    }
}
