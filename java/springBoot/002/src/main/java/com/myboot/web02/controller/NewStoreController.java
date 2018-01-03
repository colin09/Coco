package com.myboot.web02.controller;


import com.myboot.web02.domain.Result;
import com.myboot.web02.entity.Store;
import com.myboot.web02.repository.StoreRepository;
import com.myboot.web02.service.StoreService;
import com.myboot.web02.utils.ResultUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.validation.BindingResult;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.Date;

@RestController
@RequestMapping(value = "newStore")
public class NewStoreController {

    @Autowired
    private StoreRepository storeRepository;

    @Autowired
    private StoreService storeService;

    @PostMapping(value = "/")
    public Result Add(@Valid Store store, BindingResult bindingResult){
        if(bindingResult.hasErrors()){
            System.out.println(bindingResult.getFieldError().getDefaultMessage());
            return ResultUtil.error(400, bindingResult.getFieldError().getDefaultMessage());
        }

        store.setStatus(1);
        store.setMobile("19900001111");
        store.seteMail("99@199.com");
        store.setAddress(store.getDescription());
        store.setContact("9-9-9-9-9");
        store.setLogo("999.99.g");

        store.setcDate(new Date());
        store.setuDate(new Date());
        return ResultUtil.success(storeRepository.save(store));
    }


    @GetMapping(value = "/{id}")
    public Result getOne(@PathVariable("id") Integer id) {
        return ResultUtil.success(storeRepository.findOne(id));
    }

    @GetMapping(value = "/state/{state}")
    public Result getStatus(@PathVariable("state") Integer state) throws Exception {
        return storeService.getList(state);
    }

}
