#!/usr/bin/env python3
# -*- coding: utf-8 -*-

print ('\n---------------------------------------------------')

from sqlalchemy import Column,String,create_engine
from sqlalchemy.orm import sessionmaker
from sqlalchemy.ext.declarative import declarative_base

# 创建对象基类
Base =  declarative_base()

class User(Base):
	__tablename = 'user'
	
	id = column(String(20),primary_key(True))
	name = column(String(20))
	# 一对多:
	book = relationship('Book')

class Book(Base):
	__tablename__ = 'book'
	
	id = column(String(20),primary_key(True))
	name = column(String(20))
	# “多”的一方的book表是通过外键关联到user表的:
	user_id = column(String(20),foreignKey('User.id'))

	
	
	
	
	
	
	
	
	
	
	
	
# 初始化数据库连接 ('数据库类型+数据库驱动名称://用户名:口令@机器地址:端口号/数据库名')
engine = create_engine('mysql+mysqlconnector://root:password@localhost:3306/test')

# 创建DBSession类型
DBSession = sessionmaker(bind=engine)

session = DBSession()
newUser = User(id='5',name='Jack')
session.add(newUser)
session.commit()
session.close()

session = DBSession()
# user = session.query(User).filter(User.id='5').all()
user = session.query(User).filter(User.id='5').one()
print('type:' ,type(user))
print('name:' ,user.name)
session.close()

































