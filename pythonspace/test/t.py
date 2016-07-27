#!/usr/bin/env python 
# -*- coding: utf-8 -*-

' a test module '

__author__ =  'coco'

import sys

def test():
	args = sys.argv
	if len(args)==1:
		print 'hello , test !!'
	elif len(args)==2:
		print 'hello , %s !!' %args[1]
	else:
		for s in args:
			print s

if __name__ == '__main__':
	test()

try:
	import cStringIO as StringIO
except ImportError:
	import StringIO

try:
    import json # python >= 2.6
except ImportError:
    import simplejson as json # python <= 2.5

#类似_xxx和__xxx这样的函数或变量就是非公开的（private），不应该被直接引用

def _private_1(name):
    return 'Hello, %s' % name

def _private_2(name):
    return 'Hi, %s' % name

def greeting(name):
    if len(name) > 3:
        return _private_1(name)
    else:
        return _private_2(name)

print sys.path


# __future__ 把下一个新版本的特性导入到当前版本
from __future__ import division

print '10 / 3 =', 10 / 3
print '10.0 / 3 =', 10.0 / 3
print '10 // 3 =', 10 // 3