#!/usr/bin/env python 
# -*- coding: utf-8 -*-

class Student(object):
	def __init__(self,name,age):
		self.name = name 
		self._age = age
	
	def print_S(self):
		print ('name: %s , age: %s' %(self.name , self._age))
	# Python允许在定义class的时候，定义一个特殊的__slots__变量，来限制该class实例能添加的属性
	# __slots__定义的属性仅对当前类实例起作用，对继承的子类是不起作用的;
	# 子类中也定义__slots__，这样，子类实例允许定义的属性就是自身的__slots__加上父类的__slots__
	__slots__ = ('name','_age','addr','_score','_birth')
	
	@property
	def score(self):
		return self._score
	
	@score.setter
	def score(self,value):
		if not isinstance(value,int):
			raise ValueError('score must bu an integer!')
		if value < 0 or value > 100:
			raise ValueError('score must between 0~100!')
		self._score = value
	
	def age(self):
		return 2016-self._brith
	
jack = Student('Jack',12)
print(jack)
print(Student)
jack.print_S()

jack.name = 'Jack'
print(jack.name)

print ('-------------------------------------\n')

# 获得一个str对象的所有属性和方法
print('dir(\'abc\') =' , dir('abc'))

print ('###########################################')

print ('dir(\'jack\') =' , dir('jack'))

print ('-------------------------------------\n')

if(hasattr(jack,'addr')):
	print ('addr =',getattr(jack,'addr','默认地址'))
else:
	setattr(jack,'addr','default address')
	print (jack.addr)

jack.score = 99

#jack.score = 120

print ('jack.score =',jack.score)


class Screen(object):
    @property
    def width(self):
        return self._width
    @width.setter
    def width(self,value):
        self._width=value
    @property
    def height(self):
        return self._height
    @height.setter
    def height(self,value):
        self._height=value
    @property
    def resolution(self):
        return self._width * self._height

# test:
s = Screen()
s.width = 1024
s.height = 768
print(s.resolution)
assert s.resolution == 786432, '1024 * 768 = %d ?' % s.resolution

# MixIn的目的就是给一个类增加多个功能，这样，在设计类的时候，我们优先考虑通过多重继承来组合多个MixIn的功能，而不是设计多层次的复杂的继承关系。


class User(object):
	def __init__(self,name):
		self.name = name
		
	# __str__ print User 调用此方法
	def __str__(self):
		return 'User object (name = %s)' % self.name
		
	# __repr__  User 调用此方法
	__repr__ = __str__

print (User)
User
	

class Fib(object):
	def __init__(self):
		self.a ,self.b = 0 , 1
		
	def __iter__(self):
		return self  # 实例本身就是迭代对象，故返回自己
	
	def __next__(self):
		self.a , self.b = self.b  ,self.a + self.b # 计算下一个值
		if self.a > 10000:  # 退出循环的条件
			raise stopInteration();
		return self.a # 返回下一个值

	def __getitem__(self,n):
		if isinstance(n,int): # n是索引
			a , b =1,1
			for x in range(n):
				a , b = b,a+b
			return a
		if isinstance(n,slice): # n 是切片
			start = n.start
			stop = n.stop
			if start is None:
				start = 0
			a,b = 1,1
			L = []
			for x in range(stop):
				if x >= start:
					L.append(a)
				a , b = b,a+b
			return L 

#for n in Fib():
#	print(n)

f = Fib()

print (f[0]) #需要实现__getitem__()方法

print (f[10])

print (f[100])

print (f[0:10])

class Brand(object):
	def __getattr__(self,attr):
		if attr == 'name':
			return 'default-brand-name'
		if attr == 'id':
			return lambda : 20
			
	def __call__(self):
		print('b-name is %s' % self.name)
	
b=Brand()
print (b.id())
print (b.name)
b.name = 'Nike'
print (b.name)

print(callable(Brand))
print(callable(b))
b()
Brand()
# print('b() =',b())
print('Brand() =',Brand())

class Chain(object):
	def __init__(self,path=''):
		self._path = path

	def __getattr__(self,path):
		return Chain('%s/%s' %(self._path,path))

	def __str__(self):
		return self._path
		
	__repr__ = __str__

print(Chain().status.user.timeline.list)

print ('-------------------------------------\n')

# 枚举
from enum import Enum ,unique

Month = Enum('Month',('Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'))

for name , member in Month.__members__.items():
	print(name,'=>',member ,',',member.value)

# @unique装饰器可以帮助我们检查保证没有重复值
@unique
class Weekday(Enum):
	Sun=0
	Mon=1
	Tue=2
	Web=3
	Thu=4
	Fri=5
	Set=6
print ('-------------------------------------\n')

day1 = Weekday.Mon
print('day1 =', day1)
print(Weekday.Tue)
print(Weekday['Web'])
print(Weekday.Thu.value)
print(Weekday(5))
print(Weekday(1)==day1)
# print(Weekday(7))

for name ,member in Weekday.__members__.items():
	print(name,'=>',member,'=',member.value)

print ('-------------------------------------\n')

def fn(self,name='world'):
	print('hello , %s' %name)
	
# 创建Hello class
Hello = type('Hello',(object,),dict(hello=fn))
h=Hello()
h.hello()
print(type(Hello))
print(type(h))

# type()函数依次传入3个参数：
	# 1、class的名称；
	# 2、继承的父类集合，注意Python支持多重继承，如果只有一个父类，别忘了tuple的单元素写法；
	# 3、class的方法名称与函数绑定，这里我们把函数fn绑定到方法名hello上。


# metaclass是类的模板，所以必须从`type`类型派生：
class ListMetaclass(type):
    def __new__(cls, name, bases, attrs):
        attrs['add'] = lambda self, value: self.append(value)
        return type.__new__(cls, name, bases, attrs)

# __new__()方法接收到的参数依次是：
	# 1、当前准备创建的类的对象；
	# 2、类的名字；
	# 3、类继承的父类集合；
	# 4、类的方法集合。
		
class MyList(list, metaclass=ListMetaclass):
    pass

L= MyList()
L.add(1)
print(L)

print ('---------------------------------------------------\n')
# ORM 
class Field(object):
	def __init__(self,name,columu_type):
		self.name = name
		self.columu_type = columu_type
		
	def __str__(self):
		return '<%s:%s>' %(self.__class__.__name__,self.name)

class StringField(Field):
	def __init__(self,name):
		super(StringField,self).__init__(name,'varchar(100)')
	
class IntegerField(Field):
	def __init__(self,name):
		super(IntegerField,self).__init__(name,'bigint')

class ModelMetaclass(type):
	def __new__(cls,name,bases,attrs):
		if name == 'Model':
			return type.__new__(cls,name,bases,attrs)
		print('Found model: %s' % name)
		mappings = dict()
		for k ,v in attrs.items():
			if isinstance(v,Field):
				print('Found mapping: %s' %(k))
				mappings[k] = v
		for k in mappings.keys():
			attrs.pop(k)
		attrs['__mappings__'] = mappings
		attrs['__table__'] = name
		return type.__new__(cls,name,bases,attrs)
		
class Model(dict,metaclass=ModelMetaclass):
	def __init__(self,**kw):
		super(Model,self).__init__(**kw)

	def __getattr__(self,key):
		try:
			return self[key]
		except KeyError:
			raise AttributeError(r"'Model' object has no attribute %s" % key)
	
	def __setattr__(self,key,value):
		self[key] = value
	
	def save(self):
		fields = []
		params = []
		args = []
		for k , v in self.__mappings__.items():
			fields.append(v.name)
			params.append('?')
			args.append(getattr(self,k,None))
		sql = 'insert into %s (%s) values (%s)' %(self.__table__,','.join(fields),','.join(params))
		print('SQL: %s' % sql)
		print('ARGS: %s' % str(args))

print ('---------------------------------------------------\n')


class Person(Model):
    # 定义类的属性到列的映射：
    id = IntegerField('id')
    name = StringField('name')
    email = StringField('email')
    password = StringField('password')

u = Person(id=12345, name='Michael', email='test@orm.org', password='my-pwd')
u.save()





print ('---------------------------------------------------\n')
import logging
logging.basicConfig(level=logging.INFO)

def foo(s):
    return 10 / int(s)

def bar(s):
    return foo(s) * 2

def main():
    try:
        bar('0')
    
    except Exception as e:
		#logging.info(e)
        print('Error:', e)
	# else:
	#	print('no error!')
    finally:
        print('finally...')
		logging.info('log:finally')

main()

