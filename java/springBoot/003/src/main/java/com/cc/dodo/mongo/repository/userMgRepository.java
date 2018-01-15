package com.cc.dodo.mongo.repository;

import org.springframework.data.mongodb.repository.MongoRepository;

public interface userMgRepository extends MongoRepository<User,Long>{

    User findById(Long id) ;

}
