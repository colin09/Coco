#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'coco'

import logging
#; logging.basicConfig(level=logging.INFO)
import asyncio,os,json,time,aiomysql
from datetime import datetime

async def create_pool(loop,**kw):
	logging.info('create database connection pool...')
	global __pool
	__pool = await aiomysql.create_pool(
		host = kw.get('host','localhost'),
		port = kw.get('port',3306),
		user = kw.get('user','root'),
		password = kw.get('password','root'),
		db = kw.get('database','test'),
		charset = kw.get('charset','utf8'),
		autocommit = kw.get('autocommit',True),
		maxsize = kw.get('maxsize',10),
		minsize = kw.get('minsize',1),
		loop=loop
		)

async def select(sql,args,size=None):
	#print(sql)
	logging.info(sql,args)
	global __pool
	with await __pool as conn:
		cur = await conn.cursor(aiomysql.DictCursor)
		await cur.execute(sql.replace('?','%s'),args or ())
		if size:
			rs = await cur.fetchmany(size)
		else:
			rs = await cur.fetchall()
		await cur.close()
		logging.info('rows return: %s' % len(rs))
		return rs

async def execute(sql,args):
	logging.info('SQL:',sql.replace('?','%s'),args)
	with await __pool as conn:
		try:
			cur = await conn.cursor()
			
			await cur.execute(sql.replace('?','%s'),args)
			affected = cur.rowcount
			await cur.close()
		except BaseException as ex:
			raise
		return affected

### ORM #####################################################################

































