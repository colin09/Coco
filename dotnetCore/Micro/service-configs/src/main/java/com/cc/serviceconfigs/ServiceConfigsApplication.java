package com.cc.serviceconfigs;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.config.server.EnableConfigServer;

@SpringBootApplication
@EnableConfigServer
public class ServiceConfigsApplication {

    public static void main(String[] args) {
        SpringApplication.run(ServiceConfigsApplication.class, args);
    }
}
