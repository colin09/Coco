package com.myboot.web02.controller;


import com.myboot.web02.entity.Store;
import com.myboot.web02.repository.StoreRepository;
import com.myboot.web02.service.StoreService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;

@RestController
@RequestMapping("store")
public class StoreController {

    @Autowired
    private StoreRepository storeRepository;

    @Autowired
    private StoreService storeService;

    @GetMapping(value = "/")
    public List<Store> getAll() {
        return storeRepository.findAll();
    }

    @PostMapping(value = "/")
    public Store add(@RequestParam("name") String name,
                     @RequestParam("desc") String desc) {
        Store store = new Store();
        store.setName(name);
        store.setDescription(desc);

        store.setStatus(1);
        store.setMobile("19900001111");
        store.seteMail("99@199.com");
        store.setAddress(desc);
        store.setContact("9-9-9-9-9");
        store.setLogo("999.99.g");

        store.setcDate(new Date());
        store.setuDate(new Date());
        return storeRepository.save(store);
    }


    @GetMapping(value = "/{id}")
    public Store getOne(@PathVariable("id") Integer id) {
        return storeRepository.findOne(id);
    }

    @PutMapping(value = "/{id}")
    public Store updateOne(@PathVariable("id") Integer id,
                           @RequestParam("name") String name,
                           @RequestParam("desc") String desc) {
        Store store = new Store();
        store.setId(id);
        store.setName(name);
        store.setDescription(desc);
        return storeRepository.save(store);
    }

    @GetMapping(value = "/status/{status}")
    public List<Store> getStatus(@PathVariable("status") Integer status){
        return storeRepository.findByStatus(status);
    }

    @DeleteMapping(value = "/{id}")
    public void deleteOne(@PathVariable("id") Integer id) {
        storeRepository.delete(id);
    }

    @GetMapping(value = "/two")
    public void insertTwo(){
        storeService.insertTwo();
    }

}

