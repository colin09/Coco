package com.cc.dodo.controller;

import com.cc.dodo.entity.User;
import org.springframework.web.bind.annotation.*;

import java.util.*;

public class userController {
    // create 线程安全的Map
    static Map<Long,User> users = Collections.synchronizedMap(new HashMap<Long, User>());

    @GetMapping(value = "/")
    public List<User> getList(){
        List<User> list = new ArrayList<User>(users.values());
        return list;
    }

    @PostMapping(value = "/")
    public String postUser(@ModelAttribute User user){
        if(users.containsKey(user.getId()))
            return "error";
        users.put(user.getId(),user);
        return "success";
    }


    @GetMapping(value = "/{id}")
    public User getOne(@PathVariable Long id){
        return users.get(id);
    }

    @PutMapping(value = "/{id}")
    public String setUser(@PathVariable Long id,@ModelAttribute User user){
        users.replace(id,user);
        return "success";
    }

    @DeleteMapping(value = "/{id}")
    public String deleteUser(@PathVariable Long id){
        if(users.containsKey(id)){
            users.remove(id);
            return "success";
        }
        return "error";
    }
}
