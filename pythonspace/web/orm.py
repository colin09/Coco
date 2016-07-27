#!/usr/bin/env python3
# -*- coding: utf-8 -*-

__author__ = 'coco'


import logging; logging.basicConfig(level=logging.INFO)
import dbm

class Field(object):

	def __init__(self,name,column_type,primary_key,default):
		self.name = name
		self.column_type = column_type
		self.primary_key = primary_key
		self.default = default

	def __str__(self):
		return '<%s ,%s : %s>' % (self.__class__.__name__,self.column_type,self.name)
	
class StringField(Field):
	def __init__(self,name=None,primary_key=False,default=None,ddl='varchar(50)'):
		super().__init__(name,ddl,primary_key,default)

class BoolField(Field):
	def __init__(self,name=None,primary_key=False,default=False,ddl='bool'):
		super().__init__(name,ddl,primary_key,default)

class FloatField(Field):
	def __init__(self,name=None,primary_key=False,default=0,ddl='float'):
		super().__init__(name,ddl,primary_key,default)

class TextField(Field):
	def __init__(self,name=None,primary_key=False,default=None,ddl='text'):
		super().__init__(name,ddl,primary_key,default)

		
def create_args_string(num):
	L=[]
	for n in range(num):
		L.append('?')
	return ','.join(L)
		

class ModelMetaclass(type):
	def __new__(cls,name,bases,attrs):
		if name == 'Model':
			return type.__new__(cls,name,bases,attrs)
		tableName = attrs.get('__table__',None) or name
		logging.info('found model:%s (table：%s)' % ( name, tableName))
		
		mappings = dict()
		fields = []
		primaryKey = None
		for k,v in attrs.items():
			if isinstance(v,Field):
				logging.info('found mapping:%s ==> %s' % (k,v))
				mappings[k] = v
				if v.primary_key:
					if primaryKey:
						raise RuntimeError('Duplicate primary key for field:%s' % k)
					primaryKey = k
				else:
					fields.append(k)
		if not primaryKey:
			raise RuntimeError('Primary key not found')
		for k in mappings.keys():
			attrs.pop(k)
		
		escaped_fields = list(map(lambda f:'`%s`' % f,fields))
		attrs['__mappings__'] = mappings # 保存属性和列的映射关系
		attrs['__table__'] = tableName
		attrs['__primary_key__'] = primaryKey
		attrs['__fields__'] = fields
		
		attrs['__select__'] = 'select `%s` ,%s from `%s`' % (primaryKey,','.join(escaped_fields),tableName)
		
		attrs['__inster__'] = 'insert into `%s` (%s ,`%s`) values (%s)' % (tableName,','.join(escaped_fields),primaryKey,create_args_string(len(escaped_fields)+1))
		
		attrs['__update__'] = 'update `%s` set %s where `%s` = ?' % (tableName,','.join(map(lambda f:'`%s`=?' % (mappings.get(f).name or f),fields)),primaryKey)
		
		attrs['__delete__'] = 'delete from `%s` where `%s` = ?' %(tableName,primaryKey)
		
		return type.__new__(cls,name,bases,attrs)


class Model(dict, metaclass = ModelMetaclass):
	def __init__(self,**kw):
		super(Model,self).__init__(**kw)
	
	def __getattr__(self,key):
		try:
			return self[key]
		except KeyError:
			raise AttributeError(r"'Model' object has no attribute '%s'" % key)
	
	def __setattr__(self,key,value):
		self[key] = value
	
	def getValue(self,key):
		return getattr(self,key,None)
	
	def getValueOrDefault(self ,key):
		value = getattr(self,key,None)
		if value is None:
			field  = self.__mappings__[key]
			if field.default is not None:
				value = field.default() if callable(field.default) else field.default
				logging.info('using default value for %s : %s' % (key,str(value)))
				setattr(self,key,value)
		return value
	
	
	@classmethod
	async def find(cls,pk):
		logging.info('find object by primary key.')
		rs = await dbm.select('%s where `%s` = ?' % (cls.__select__,cls.__primary_key__,[pk],1))
		if len(rs) == 0:
			return None
		return cls(**rs[0])
	
	@classmethod
	async def findAll(cls,where=None,args=None,**kw):
		logging.info('find all object by where clause')
		sql = [cls.__select__]
		if where:
			sql.append('where')
			sql.append(where)
		if args is None:
			args = []
		orderBy = kw.get('orderBy',None)
		if orderBy:
			sql.append('order by')
			sql.append(orderBy)
		limit = kw.get('limit',None)
		if limit is not None:
			sql.append('limit')
			if isinstance(limit,int):
				sql.append('?')
				args.append(limit)
			if isinstance(limit,tuple) and len(limit) ==2:
				sql.append('?,?')
				args.extend(limit)
			else:
				raise ValueError('Invalid limit value: %s' % str(limit))
		rs = await dbm.select(' '.join(sql),args)
		return [cls(**r) for r in rs]
	
	
	async def save(self):
		args = list(map(self.getValueOrDefault,self.__fields__))
		args.append(self.getValueOrDefault(self.__primary_key__))
		row = await dbm.execute(self.__inster__,args)
		if row != 1:
			logging.info('filed to insert record:affedted rows: %s' % rows)







































