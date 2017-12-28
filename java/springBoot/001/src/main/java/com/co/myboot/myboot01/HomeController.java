package com.co.myboot.myboot01;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("home")
public class HomeController
{
    @Value("${page}")
    private int page;
    @Value("${pageSize}")
    private int pageSize;

    @Autowired
    private PageProperties pageProperties;

    //@RequestMapping(value = {"/index","/hi"}, method = RequestMethod.GET)
    @GetMapping(value="/hi")
    public String Index(){
        String value ="hi , you !";
        value += "<br />" + page;
        value += "<br />" + pageSize;
        value += "<br />" + pageProperties.getTotal();

        return value;
    }

    /*
    @RequestMapping(value = {"/detail/{id}"}, method = RequestMethod.GET)
    public String Detail(@PathVariable("id") int id){

        String value ="hi , you !";
        value += "<br />" + id;
        value += "<br />" + pageSize;
        value += "<br />" + pageProperties.getTotal();

        return value;
    }
    */


}
