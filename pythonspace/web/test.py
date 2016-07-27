#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'coco'


import asyncio,os,sys
import dbm , orm
from models import User,Blog,Comment


async def test1(loop):
	await dbm.create_pool(loop=loop,host='192.168.1.182',user='sa',password='Sa123@456',database='blog')

	#u = User(Name='Jack',EMail='jack@cc.com',Password='123',Logo='about:blank')
	#print(u)
	
	await User.findAll()

loop = asyncio.get_event_loop()
loop.run_until_complete(test1(loop))
loop.close()

if loop.is_closed():
    sys.exit(0)