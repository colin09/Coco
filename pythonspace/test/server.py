# -*- coding: utf-8 -*-

# server.py，负责启动WSGI服务器，加载application()函数：


# 从wsgiref模块导入:
from wsgiref.simple_server import make_server
# 导入我们自己编写的application函数:
from page1 import application

# 创建一个服务器，IP地址为空，端口是8000，处理函数是application:
httpd = make_server('', 8099, application)
print('Serving HTTP on port 8099...')
# 开始监听HTTP请求:
httpd.serve_forever()