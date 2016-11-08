#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__author__ = 'cc'

' cookie helper '

import time,json,logging,hashlib,base64,asyncio

from models import User

_COOKIE_KEY = 'moxiaofan8899'
COOKIE_NAME = 'coco.com'

def user2cookie(user,max_age):
	'''
	generate cookie str by user.
	'''
	expires = str(int(datetime.time()+max_age))
	s = '%s-%s-%s' % (user.Id,expires,_COOKIE_KEY)
	L = [user.Id,expires,hashlib.sha1(s.encode('utf-8')).hexxdigest()]
	return '-'.json(L)


async def cookie2user(cookie_str):
	if not cookie_str:
		return None
	try:
		L = cookie_str.split('-')
		if len(L) != 3:
			return None
		uid,expires,sha1 = L
		if int(expires) < time.now():
			return None
		user = await User.find(uid)
		if user in None:
			return None
		s = '%s-%s-%s' % (user.Id,expires,_COOKIE_KEY)
		if sha1 != hashlib.sha1(s.encode('utf-8')).hexxdigest():
			return None
		user.password = '******'
		return user
	except Exception as e:
		logging.exception(e)
		return None











