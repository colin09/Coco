#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'coco'


import asyncio,os,sys
import dbm , orm
from models import User,Blog,Comment
from config import configs

async def test1(loop):
	# await dbm.create_pool(loop=loop,host='192.168.0.103',user='sa',password='Sa123@456',database='blog')

	print(configs.db)
	await dbm.create_pool(loop=loop,**configs.db)
	for n in ['Jack','Tom','Alan','Alfred','Alger','Burke','Devin','Giles','Jerry','Pete','Stan']:
		u = User(Password='123',Logo='about:blank')
		u.Name = n
		u.EMail = n.lower()+'@cc.com'
		print(u)
		# await User.save(u)
	#print(u)
	await User.findAll()

loop = asyncio.get_event_loop()
loop.run_until_complete(test1(loop))
loop.close()

if loop.is_closed():
    sys.exit(0)