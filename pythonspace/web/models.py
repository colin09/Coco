#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'coco'


import time , uuid

from orm import Model,StringField,BoolField,FloatField,TextField,Field

def next_id():
	return '%015d%s000' % (int(time.time() * 1000),uuid.uuid4().hex)

class User(Model):
	__table__ = 'user'
	
	Id = StringField(primary_key=True,default=next_id,ddl='varchar(32)')
	Name = StringField()
	EMail = StringField()
	Password = StringField()
	Admin = BoolField()
	Logo = StringField(ddl = 'varchar(500)')
	CreateDate = FloatField(default=time.time)

class Blog(Model):
    __table__ = 'blogs'

    Id = StringField(primary_key=True, default=next_id, ddl='varchar(50)')
    UserId = StringField()
    UserName = StringField()
    UserLogo = StringField(ddl='varchar(500)')
    Title = StringField()
    Summary = StringField(ddl='varchar(200)')
    Content = TextField()
    CreateDate = FloatField(default=time.time)

class Comment(Model):
    __table__ = 'comments'

    Id = StringField(primary_key=True, default=next_id, ddl='varchar(50)')
    BlogId = StringField()
    UserId = StringField()
    UserName = StringField()
    UserLogo = StringField(ddl='varchar(500)')
    Content = TextField()
    CreateDate = FloatField(default=time.time)

