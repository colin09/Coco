package com.co.myboot.myboot01;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;

@Service
public class StoreService {

    @Autowired
    private StoreRepository storeRepository;

    // 事物
    @Transactional
    public void insertTwo(){
        Store storeA = new Store();
        storeA.setName("storeA");
        storeA.setDescription("T-storeA");

        storeA.setStatus(1);
        storeA.setMobile("19900001111");
        storeA.seteMail("99@199.com");
        storeA.setAddress("T-storeA");
        storeA.setContact("9-9-9-9-9");
        storeA.setLogo("999.99.g");

        storeA.setcDate(new Date());
        storeA.setuDate(new Date());
         storeRepository.save(storeA);

        Store storeB = new Store();
        storeB.setName("storeB");
        storeB.setDescription("T-storeB");

        storeB.setStatus(1);
        storeB.setMobile("19900001111");
        storeB.seteMail("99@199.com");
        storeB.setAddress("T-storeB");
        storeB.setContact("9-9-9-9-9");
        storeB.setLogo("999.99.g");

        storeB.setcDate(new Date());
        storeB.setuDate(new Date());
         storeRepository.save(storeB);
    }


}
