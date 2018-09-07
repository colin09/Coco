package com.myboot.web02.repository;

import com.myboot.web02.entity.Store;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface StoreRepository extends JpaRepository<Store,Integer> {


    public List<Store> findByStatus(Integer Status);

}
