#!/usr/bin/env python 
# -*- coding: utf-8 -*-
# coding=utf-8

# 第一行:告诉Linux/OS X系统，这是一个Python可执行程序，Windows系统会忽略这个注释；
# 第二行:告诉Python解释器，按照UTF-8编码读取源代码，否则，你在源代码中写的中文输出可能会有乱码
# 第三行:作用等同第二行

print '[Test First] \n\n'

print '==>test I/O \n'

name = '###'  #raw_input()
# 注释
print 'hello' , name ,'\n'

a = -100
if a > 10 :
	print a
else :
    print -a
	
	
print '\\\t\\'

print r'\\\t\\'

print '''line1
... line2
... line3'''

print '''line1
line2
line3'''

print '-------------------------------------\n'

print r'''line1
line2
line3'''

print '-------------------------------------\n'

a = 123 # a是整数
print a
a = 'ABC' # a变为字符串
print a

print '-------------------------------------\n'

#全部大写的变量名表示常量是一个习惯上的用法
PI = 3.14159265359
print PI

print '-------------------------------------\n'

print '10 / 3 =' , 10 / 3  

print '10 % 3 =' , 10 % 3  

print '10.0 / 3 =' , 10.0 / 3 

print '-------------------------------------\n'

#ASCII 转数字
print ord('A')
#ASCII 转字符
print chr(65)

print u'中文'
print u'中'

print u'abc'.encode('utf-8')
print u'中文'.encode('utf-8')
#print '\xe4\xb8\xad\xe6\x96\x87'.encode('unicode')

print '-------------------------------------\n'


# %d	整数
# %f	浮点数
# %s	字符串
# %x	十六进制整数

print 'hi ,%s !' % 'AA'
print 'hi ,%s ,you hava $%d' %('AA',10000)

print '[%2d] , [%02d] ' %(2,3)

print '%.4f' % 3.14159265359
# %s永远起作用
print 'str:%s , number:%s , float:%s , bool:%s' % ('string' ,100, 10.00, True)

print 'growth rete: %d %%' % 90

print '-------------------------------------\n'

# 可变列表 list 
ls = ['AAA','BBB','CCC',123,3.14159,True]
print 'ls.length=' , len(ls)
print 'ls[0]=' , ls[0]
print u'ls最后一个元素， ls[-1]=' , ls[-1]
print u'ls倒数第二个元素， ls[-2]=' , ls[-2]
for l in ls:
	print l

print '-------------------------------------\n'
ls.append('appItem.1')
ls.append('appItem.2')
ls.insert(3,'DDD')
ls[1] = 'BBB.BBB'

# 删除末尾元素
ls.pop()
# 删除index=3 的元素
ls.pop(3)

for l in ls:
	print l

print '-------------------------------------\n'

pls = ['asp', 'php', 'python', 'java', ls, 'scheme']
print 'pls[4][1] =' , pls[4][1]
for p in pls:
	print p

print '-------------------------------------\n'

#不可变列表 tuple (元组)
tp = (1,2,'A','B',True)

for t in tp:
	print t
	
# 括号()既可以表示tuple，又可以表示数学公式中的小括号，这就产生了歧义，因此，Python规定，这种情况下，按小括号进行计算，计算结果自然是1
t = (1)
print t

t = (1,)
print t

# tuple 所谓的“不变”是说，tuple的每个元素，指向永远不变
tp = ('a', 'b', ['A', 'B'])
tp[2][0] = 'A.A'
tp[2][1] = 'B.B'
for t in tp:
	print t
print '-------------------------------------\n'

age = 3
if age >= 18:
    print 'adult'
elif age >= 6:
    print 'teenager'
else:
    print 'kid'

# 只要x是非零数值、非空字符串、非空list等，就判断为True，否则为False。
x=1
if x:
    print 'True'

print range(5)

sum =0
for x in range(100):
	sum += x
print sum

while sum >100 :
	sum -= 9
print sum 


# raw_input() 接收结果为字符串
# birth = int(raw_input('birth: ')) 

print '-------------------------------------\n'

dict = {'Michael': 95, 'Bob': 75, 'Tracy': 85}
print dict['Bob']
dict['Adam'] = 67
dict['Jack'] = 90

dict.pop('Bob')

print 'Thomas in dict :' , 'Thomas' in dict
print 'Thomas in dict :' , dict.get('Thomas')
print 'Thomas in dict :' , dict.get('Thomas' , 60)
print 'Thomas in dict :' , dict.get('Jack' , 60)

for d in dict:
	print d , '=', dict.get(d)

print '-------------------------------------\n'
# set和dict的唯一区别仅在于没有存储对应的value
s = set([1,2,3,3,4])
s.add(4)
s.add(5)
s.remove(4)
print s ,'\n'

s1 = set([1, 2, 3])
s2 = set([2,3,4])
print 's1 = ' , s1
print 's2 = ' , s2
print 's1 & s2 =' ,s1 & s2
print 's1 | s2 =' ,s1 | s2

print '-------------------------------------\n'

print 'abs(-100) =' , abs(-100)
print 'cmp(1,2) =' , cmp(1 , 2)

int('123')
int(12.34)
float('12.34')
str(1.23)
unicode(100)
bool(1)
bool('')

a = abs # 变量a指向abs函数
a(-1) # 所以也可以通过a调用abs函数


# 定义函数
# 如果没有return语句，函数执行完毕后也会返回结果，只是结果为None。
# return None 可以简写为 return
def my_abs(x):
	if not isinstance(x,(int,float)):
		raise TypeError('bad operand type')
		
	if(x >= 0):
		return x
	else:
		return -x

# 空函数
def nop():
	pass

import math

# Python的函数返回多值其实就是返回一个tuple
def move(x, y, step, angle=0):
    nx = x + step * math.cos(angle)
    ny = y - step * math.sin(angle)
    return nx, ny

x, y = move(100, 100, 60, math.pi / 6)
print x, y

r = move(100, 100, 60, math.pi / 6)
print r

print '-------------------------------------\n'

def power(x):
	return x*x

def power(x,n=2):
	s=1
	while(n>0):
		s *= x
		n -= 1
		
	return s

print 'power(3) =',power(3)
print 'power(3 ,3 ) =',power(3 , 3)


def add_end(L=[]):
    L.append('END')
    return L
	
def add_end2(L=None):
    if L is None:
        L = []
    L.append('END')
    return L
	
print add_end()
print add_end()
print add_end()

def calc(*numbers):
	sum =0 
	for n in numbers:
		sum += n*n
	return sum


print 'calc(1, 2) =' , calc(1, 2)

#在list或tuple前面加一个*号，把list或tuple的元素变成可变参数传进去
nums = [1, 2, 3]
calc(*nums)

def person(name, age, **kw):
    print 'name:', name, 'age:', age, 'other:', kw

# *args是可变参数，args接收的是一个tuple；
# **kw是关键字参数，kw接收的是一个dict。
def func(a, b, c=0, *args, **kw):
    print 'a =', a, 'b =', b, 'c =', c, 'args =', args, 'kw =', kw

func(1, 2)
#a = 1 b = 2 c = 0 args = () kw = {}

func(1, 2, c=3)
#a = 1 b = 2 c = 3 args = () kw = {}

func(1, 2, 3, 'a', 'b')
#a = 1 b = 2 c = 3 args = ('a', 'b') kw = {}

func(1, 2, 3, 'a', 'b', x=99)
#a = 1 b = 2 c = 3 args = ('a', 'b') kw = {'x': 99}

print '-------------------------------------\n'

# 递归函数
def fact(n):
    if n==1:
        return 1
    return n * fact(n - 1)

print 'fact(1) =' , fact(1)
print 'fact(5) =' , fact(5)
print 'fact(100) =' , fact(100)

# 解决递归调用栈溢出的方法是通过尾递归优化  RuntimeError: maximum recursion depth exceeded
# 尾递归是指，在函数返回的时候，调用自身本身，并且，return语句不能包含表达式

def fact_iter(num, product):
    if num == 1:
        return product
    return fact_iter(num - 1, num * product)

print 'fact_iter(100) =' , fact_iter(100, 2.5)






















