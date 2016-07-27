#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'cc'

'''
async web application.
'''

import logging; logging.basicConfig(level=logging.INFO)
import asyncio,os,json,time

from datetime import datetime
from aiohttp import web
from jinja2 import Environment,FileSystemLoader

import orm, dbm
from coroweb import add_routes,add_static
from config import configs


async def init(loop):
	#await dbm.create_pool(loop=loop,host='192.168.0.103',user='sa',password='Sa123@456',database='blog')
	await dbm.create_pool(loop=loop,**configs.db)
	app = web.Application(loop=loop,middlewares=[
		    logger_factory,response_factory
		])
	init_jinja2(app,filters=dict(datetime=datetime_filter))
	add_routes(app,'handlers')
	add_static(app)
	srv = await loop.create_server(app.make_handler(),'127.0.0.1',8099)
	logging.info('server started at http://127.0.0.1:8099...')
	return srv


#########################################

def init_jinja2(app,**kw):
	logging.info('init jinja2 ...')
	logging.info('**kw: %s' % kw)
	options = dict(
		autoescape = kw.get('autoescape',True),
		block_start_string = kw.get('block_start_string','{%'),
		block_end_string = kw.get('block_end_string','%}'),
		variable_start_string = kw.get('variable_start_string','{{'),
		variable_end_string = kw.get('variable_end_string','}}'),
		auto_reload = kw.get('auto_reload',True)
	)
	path = kw.get('path',None)
	if path is None:
		path = os.path.join(os.path.dirname(os.path.abspath(__file__)),'templates')
	logging.info('set jinja2 template path: %s' % path)
	env = Environment(loader=FileSystemLoader(path),**options)
	filters = kw.get('filter',None)
	if filters is not None:
		for name ,f in filters.items():
			env.filters[name] = f
	app['__templating__'] = env


async def logger_factory(app,handler):
    async def logger(request):
        logging.info('logger_factory ==> Request: %s %s' % (request.method,request.path))
        #await asyncio.sleep(0.3)
        return (await handler(request))
    return logger


async def data_factory(app,handler):
    async def parse_data(request):
        logging.info('data_factory ==> request: %s' % request)
        logging.info('data_factory ==> handler: %s' % handler)
        if request.content_type.startwith('application/json'):
            request.__data__ = await request.json()
            logging.info('request json: %s' % str(request.__data__))
        elif request.content_type.startwith('application/x-www-form-urlencoded'):
            request.__data__ = await request.post()
            logging.info('request form: %s' % str(request.__data__))
        return await handler(request)
    return parse_data

async def response_factory(app,handler):
	async def response(request):
		logging.info('response_factory ==> Response handler ...')
		logging.info('response_factory ==> handler: %s' % handler)
		logging.info('response_factory ==> request: %s' % request)
		r = await handler(request)
		if isinstance(r,web.StreamResponse):
			return r
		if isinstance(r,bytes):
			resp = web.Response(body=r)
			return resp
		if isinstance(r,str):
			if r.startwith('redirect:'):
				return web.HTTPFound(r[9:])
			resp = web.Response(body=r.encode('utf-8'))
			resp.content_type = 'text/html;charset=utf-8'
			return resp
		if isinstance(r,dict):
			template = r.get('__template__')
			if template is None:
				resp = web.Response(body=json.dumps(r,ensure_ascii=False,default=lambda o:o.__dict__).encode('utf-8'))
				resp.content_type = 'application/json;charset=utf-8'
				return resp
			else:
				resp = web.Response(body=app['__templating__'].get_template(template).render(**r).encode('utf-8'))
				resp.content_typ = 'text/html;charset=utf-8'
				return resp
		if isinstance(r,int) and r >= 100 and r < 600:
			return web.Response(r)
		if isinstance(r,tuple) and len(r) == 2:
			t,m = r
			if isinstance(r,int) and t>=100 and t <600:
				return web.Response(t,str(m))
        #default
		resp = web.Response(body=str(r).encode('utf-8'))
		resp.content_type = 'text/plain;charset=utf-8'
		return resp
	return response


def datetime_filter(t):
	times = int(time.time()-t)
	if times <60:
		return u'一分钟前'
	if times <3600:
		return u'%s分钟前' % (times / 60)
	if times < 86400:
		return u'%s小时前' % (times / 3600)
	if times < 604800:
		return u'%s天前' % (times / 86400)
	dt = datetime.fromtimestamp(t)
	return u'%s年%s月%s日' % (dt.year,dt.month,dt.day)


loop = asyncio.get_event_loop()
loop.run_until_complete(init(loop))
loop.run_forever()


'''
 pip install pillow
 pip install pymysql
 pip install cymysql
 pip install sqlalchemy
 pip install flask
 pip install aiohttp
 pip install aiomysql

 python F:\百度云同步盘\magicHourse\pythonspace\web\app.py
'''