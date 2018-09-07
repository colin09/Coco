package com.co.myboot.myboot01;

import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface StoreRepository extends JpaRepository<Store,Integer> {


    public List<Store> findByStatus(Integer Status);

}
