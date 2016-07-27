#!/usr/bin/env python 
# -*- coding: utf-8 -*-

L = ['Michael', 'Sarah', 'Tracy', 'Bob', 'Jack']
# 切片（Slice）
print 'L[:3] =' , L[:3]
print 'L[0:3] =' , L[0:3]
print 'L[1:3] =' , L[1:3]

print 'L[-1:] =' , L[-1:]
print 'L[-3:-1] =' , L[-3:-1]

L=range(100)
print 'L =' , L
print 'L[:10] =' , L[:10]
print 'L[:-10] =' , L[:-10]
print 'L[-10:] =' , L[-10:]
# 前10个数，每两个取一个 
print 'L[:10:2] =' , L[:10:2]
# 所有数，每5个取一个
print 'L[::5] =' , L[::5]
print 'L[:] =' , L[:]

# tuple也是一种list，唯一区别是tuple不可变。因此，tuple也可以用切片操作，只是操作的结果仍是tuple

# 字符串'xxx'或Unicode字符串u'xxx'也可以看成是一种list，每个元素就是一个字符,可以用切片操作

print '-------------------------------------\n'

from collections import Iterable

print isinstance('abc', Iterable) # str是否可迭代

print isinstance([1,2,3], Iterable) # list是否可迭代

print isinstance(123, Iterable) # 整数是否可迭代

# enumerate 函数
for i,value in enumerate(L):
	if value % 10 == 0:
		print i , value

for x, y in [(1, 1), (2, 4), (3, 9)]:
	print x, y

print '-------------------------------------\n'

# 列表生成式

ls = [x*x for x in range(1,10)]
print 'ls =' , ls

ls = [x*x for x in range(1,10) if(x%2==0)]
print 'ls =' , ls

ls = [m+n for m in 'ABC' for n in 'XYZ']
print 'ls =' , ls

import os # 导入os模块

ls = [d for d in os.listdir('.')] # os.listdir可以列出文件和目录
print 'ls =' , ls

dic = {'x': 'A', 'y': 'B', 'z': 'C'}
for k , v in dic.iteritems():
	print k , '=' , v

ls = [k+'='+v.lower() for k , v in dic.iteritems()]
print 'ls =' , ls

ls = ['Hello', 'World', 18, 'Apple']
ls = [s.lower() if isinstance(s,str) else s for s in ls]
print 'ls =' , ls

print '-------------------------------------\n'

ls = [x*x for x in range(10)]
print 'ls =' , ls

# 生成器
g = (x*x for x in range(10))
print 'g =' , g

print g.next()
print g.next()
print g.next()

g = (x*x for x in range(10))
for n in g:
	print n

# 斐波拉契数列 - 生成器式实现
def fib(max):
	n , a , b = 0 , 0 , 1
	while(n<max):
		yield b 
		a , b = b , a + b
		n += 1

print fib(6)

for n in fib(10):
	print n

print '-------------------------------------\n'

f = abs
def add(x,y,f):
	return f(x) + f(y) 

# 编写高阶函数，就是让函数的参数能够接收别的函数。
print 'add(9 , -9 ,abs) =' , add(9 , -9 ,abs)

	
def f(x):
	return x*x

# map()函数接收两个参数，一个是函数，一个是序列，map将传入的函数依次作用到序列的每个元素，并把结果作为新的list返回
print 'map(f,[1,2,3,4,5,6]) =' , map(f,[1,2,3,4,5,6])

print 'map(str,[1,2,3,4,5,6]) =' , map(str,[1,2,3,4,5,6])
	
def f(x,y):
	return x * 10 + y

print 'reduce(f,[1,2,3,4,5,6]) =' , reduce(f,[1,2,3,4,5,6])

def char2num(s):
    return {'0': 0, '1': 1, '2': 2, '3': 3, '4': 4, '5': 5, '6': 6, '7': 7, '8': 8, '9': 9}[s]

def str2int(s):
	return reduce(lambda x,y:x*10+y ,map(char2num,s))

print 'str2int(\'436457675\') =', str2int('436457675')


print filter(lambda x:x%2==0,[1,2,3,4,5,6])

print filter(lambda x:x and x.strip(),['A', '', 'B', None, 'C', '  '])

def sushu(x):
	if x<1:return False
	i = x-1
	while (i>1):
		if x % i ==0:
			return False
		i-=1
	return True

print filter(sushu ,range(100))	

print sorted([36, 5, 12, 9, 21])

def order_desc(x, y):
    if x > y:
        return -1
    if x < y:
        return 1
    return 0
	
	
print sorted([36, 5, 12, 9, 21],order_desc)

def cmp_ignore_case(s1, s2):
    u1 = s1.upper()
    u2 = s2.upper()
    if u1 < u2:
        return -1
    if u1 > u2:
        return 1
    return 0

def lazy_sum(*args):
	def sum():
		ax = 0
		for a in args:
			ax += a 
		return ax
	return sum

print lazy_sum(1,2,3,4,5,6,8,9)

s = lazy_sum(1,2,3,4,5,6,8,9)
print s()	
print s.__name__

print '-------------------------------------\n'

# 装饰器（Decorator）
import functools

def log(func):
	@functools.wraps(func)
	def wrapper(*args,**kw):
		print 'call %s():' % func.__name__
		return func(*args,**kw)
	return wrapper

#把@log放到now()函数的定义处，相当于执行了语句：now = log(now)
@log
def now():
	print '2016-04-08'

now()

def log1(text):
	def decorator(func):
		@functools.wraps(func)
		def wrapper(*args,**kw):
			print '[%s] call %s():' % (text,func.__name__)
			return func(*args,**kw)
		return wrapper
	return decorator

# 和两层嵌套的decorator相比，3层嵌套的效果是这样的：now = log('execute')(now)
@log1('execute')
def now1():
	print '2016-04-09'

now1()

def log2(text):
	if callable(text):
		func = text
		@functools.wraps(func)
		def wrapper(*args,**kw):
			print 'call %s():' % (func.__name__)
			return func(*args,**kw)
		return wrapper
	else:
		def decorator(func):
			@functools.wraps(func)
			def wrapper(*args,**kw):
				print '[%s] call %s():' % (text,func.__name__)
				return func(*args,**kw)
			return wrapper
		return decorator

@log2
def now2():
	print '2016-04-09'
	
@log2('kkkkkkkk')
def now3():
	print '2016-04-10'

now2()
now3()

print '-------------------------------------\n'

# 偏函数

print int('12')
print int('12' , 8)
print int('12' , base = 16)

def int2(x,base=2):
	return int(x,base)

print int2('1000000')

# functools.partial就是帮助我们创建一个偏函数的
int8 = functools.partial(int,base=8)

print int8('123')

print int8('123',base=10)

















