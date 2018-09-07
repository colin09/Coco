import logging; logging.basicConfig(level=logging.INFO)
import asyncio,os,json,time
from datetime import datetime
from aiohttp import web

async def index(request):
	# print(request)
	return web.Response(body=b'<h1>CO python</h1>')

async def about(request):
	# print(request)
	return web.Response(body=b'<h1>CO ~ 2016</h1>')
	
async def init(loop):
	app = web.Application(loop=loop)
	app.router.add_route('GET','/',index)
	app.router.add_route('GET','/about',about)
	srv = await loop.create_server(app.make_handler(),'127.0.0.1',8099)
	logging.info('server started at http://127.0.0.1:9000...')
	print('server started at http://127.0.0.1:9000...')

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
'''