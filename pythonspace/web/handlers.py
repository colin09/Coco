#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__author__ = 'cc'

' url handlers '

import re, time, json, logging, hashlib, base64, asyncio

from coroweb import get,post

from models import User,Blog,Comment,next_id

@get('/')
async def index(request):
    print('get index ...')
    users = await User.findAll()
    return{
            '__template__':'test.html',
            'users':users
        }


@get('/users')
async def index(request):
    print('get users ...')
    users = await User.findAll()
    return{
            '__template__':'test.html',
            'users':users
        }