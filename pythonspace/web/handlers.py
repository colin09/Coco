#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__author__ = 'cc'

' url handlers '

import re, time, json, logging, hashlib, base64, asyncio

import cookie

from coroweb import get,post

from models import User,Blog,Comment,next_id



def get_page_index(page):
    p=1
    try:
        p = int(page)
    except ValueError as e:
        pass
    if p < 1:
        p=1
    return p















@get('/')
async def index(request):
    print('get index ...')
    users = await User.findAll()
    return{
            '__template__':'index.html',
            'users':users
        }


@get('/regist')
async def regist(request):
    return{
            '__template__':'regist.html'
        }

_RE_EMAIL = re.compile(r'^[a-z0-9\.\-\_]+\@[a-z0-9\-\_]+(\.[a-z0-9\-\_]+){1,4}$')
_RE_SHA1 = re.compile(r'^[0-9a-f]{40}$')

@post('/api/regist')
async def api_regist(*,name,email,password):
    if not name or not name.strip():
        raise APIValueError('name')
    if not email or not _RE_EMAIL.match(email):
        raise APIValueError('email')
    if not password or not _RE_SHA1.match(password):
        raise APIValueError('password')
    users = User.findAll('email=?',[email])
    if len(users)>0:
        raise APIError('regist failed, email is already in use.')
    uid = next_id()
    sha1_pwd = '%s:%s' % (uid,password)
    user = User(Id=uid,Name=name,EMail=email,Password=hashlib.sha1(sha1_pwd.encode('utf-8')).hexdigest(),Logo='http://img.hb.aicdn.com/1c862eb3b5bda3a17d8da2714076152ecb60048446ec7-pUpOLP_fw658')
    await User.save(user)
    r = web.Response()
    r.set_cookie(cookie.COOKIE_NAME,cookie.user2cookie(user,7200),max_age=7200,httponly=True)
    uses.password='******'
    r.content_type='application/json'
    r.body=josn.dumps(user,ensure_ascii=False).encode('utf-8')
    return r


@get('/signin')
async def signin(request):
    return{
            '__template__':'signin.html'
        }

@post('/api/signin')
async def signin(*,email,password):
    if not email:
        raise APIValueError('email','Invalid email.')
    if not password:
        raise APIValueError('email','Invalid password.')
    users = await User.findAll('email=?' ,[email])
    if len(users)==0:
        raise APIError('email','Email not exist.')
    user = users[0]
    sha1_pwd = '%s:%s' % (user.Id,user.Password)
    if password!=hashlib.sha1(sha1_pwd.encode('utf-8')).hexdigest():
        raise APIError('password','Invalid password.')

    r = web.Response()
    r.set_cookie(cookie.COOKIE_NAME,cookie.user2cookie(user,7200),max_age=7200,httponly=True)
    uses.password='******'
    r.content_type='application/json'
    r.body=josn.dumps(user,ensure_ascii=False).encode('utf-8')
    return r




@get('/signout')
async def signout(request):
    referer = request.headers.get('Referer')
    r = web.HTTPFound(referer or '/')
    r.set_cookie(cookie.COOKIE_NAME, '-delete-', max_age=0, httponly=True)
    logging.info('user sign out')
    return r
    '''
    return{
            '__template__':'signout.html'
        }
    '''


@get('/users')
async def users(request):
    print('get users ...')
    users = await User.findAll()
    return{
            '__template__':'test.html',
            'users':users
        }

@get('/api/users')
async def api_get_users(*,page=1):
    # page_index = get_page_index(page)
    users = await User.findAll()
    for u in users:
        u.Password = '******'

    return dict(users=users)
