#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__auth__ = 'cc'

'time'


import time

print('----------')

print('\n当前时间戳，time.time() :', time.time())

print('\n本地时间，time.localtime(time.time()):', time.localtime(time.time()))

print('\n格式化本地时间，time.asctime(time.localtime(time.time())):', time.asctime(time.localtime(time.time())))

print('\n格式化日期，time.strftime("%Y-%m-%d %H:%M:%S", time.localtime()):',time.strftime("%Y-%m-%d %H:%M:%S", time.localtime()))

print ('\n格式化日期，time.strftime("%a %b %d %H:%M:%S %Y", time.localtime()):', time.strftime("%a %b %d %H:%M:%S %Y", time.localtime()))

a = "Sat Mar 28 22:24:24 2016"
print ('\n格式化字符串转为时间戳，time.mktime(time.strptime(a,"%a %b %d %H:%M:%S %Y"))：', time.mktime(time.strptime(a,"%a %b %d %H:%M:%S %Y")))




















