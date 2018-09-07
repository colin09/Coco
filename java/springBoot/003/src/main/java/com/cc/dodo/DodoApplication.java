package com.cc.dodo;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class DodoApplication {

    public static void main(String[] args) {
        // SpringApplication.run(DodoApplication.class, args);

        SpringApplication springApplication = new SpringApplication(DodoApplication.class);
        // 禁止命令行设置参数
        springApplication.setAddCommandLineProperties(false);
        springApplication.run(args);

    }
}
