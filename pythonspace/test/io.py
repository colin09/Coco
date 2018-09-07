#!/usr/bin/env python 
# -*- coding: utf-8 -*-



print ('\n---------------------------------------------------')

# 标示符'r'表示读 ，读取文本文件，并且是UTF-8编码的文本文件
f=open('C:\workspace\log.txt','r')
# read()方法可以一次读取文件的全部内容
msg = f.read()
# print('log: %s' % msg)
# 文件使用完毕后必须关闭，因为文件对象会占用操作系统的资源
f.close()


# 由于必须关闭
try:
	f = open('C:\workspace\log.txt','r')
	msg = f.read()
	# print('log: %s' % msg)
finally:
	f.close()

# with 自动关闭 -- 相当于using 
with open('C:\workspace\log.txt','r') as f:
	print(f.read())

'''
with open('C:\workspace\log2.log','r') as f:
	for line in f.readlines():
		print(line.strip())  # 把末尾的'\n'删掉
'''

print ('\n---------------------------------------------------')

# 要读取二进制文件，比如图片、视频等等，用'rb'模式打开文件即可
# 要读取非UTF-8编码的文本文件，需要给open()函数传入encoding参数，如:读取GBK编码的文件：
with open('../../gbk.txt', 'r', encoding='gbk') as f:
	print(f.read())

with open('../../img.jpg', 'rb') as f:
	#print(f.read())
	print('f.read() = 图片二进制数据')


with open('../../w.txt', 'w') as w:
	w.write(msg)
	print('成功写入w.txt 文件！')


print ('\n---------------------------------------------------')
# 内存中读写 StringIO、BytesIO
from io import StringIO

f = StringIO()
f.write('aaaa')
f.write('bbbb')
f.write('cccc')
f.write('dddd')
f.write('zzzz')

print(f.getvalue())

f = StringIO('StringIO : \n=============\n=============\n=============\n=============')

while True:
	s = f.readline()
	if s == '':
		break
	print(s.strip())
		

from io import BytesIO

f= BytesIO()
f.write('为选择的路'.encode('utf-8'))
print(f.getvalue())

f = BytesIO('为选择的路'.encode('utf-8'))
print(f.getvalue())
print(f.read())

print ('\n---------------------------------------------------')


import os
print (os.name) # 操作系统类型
# uname()函数在Windows上不提供
# print (os.uname())
print('环境变量：%s' % os.environ)

print('环境变量.PATH：%s' % os.environ.get('PATH'))
print('环境变量.X：%s' % os.environ.get('X','default'))

print ('\n---------------------------------------------------')

# 操作文件和目录
cpath = os.path.abspath('.')
print ('当前目录的绝对路径：', cpath)
npath = os.path.join(cpath , 'mkdir') 
os.mkdir(npath)
os.rmdir(npath)

# 后一部分总是最后级别的目录或文件名
os.path.split('/Users/michael/testdir/file.txt')
# ('/Users/michael/testdir', 'file.txt')

os.path.splitext('/path/to/file.txt')
# ('/path/to/file', '.txt')

# 对文件重命名:
#os.rename('w.txt', 'wr.txt')
# 删掉文件:
# os.remove('wr.txt')

ls = [x for x in os.listdir('.') if os.path.isdir(x)]
print(ls)

ls = [x for x in os.listdir('.') if os.path.isfile(x)]
print(ls)

ls = [x for x in os.listdir('.') if os.path.isfile(x) and os.path.splitext(x)[1]=='.py']
print(ls)

from datetime import datetime

pwd = os.path.abspath('.')

print('      Size     Last Modified  Name')
print('------------------------------------------------------------')

for f in os.listdir(pwd):
    fsize = os.path.getsize(f)
    mtime = datetime.fromtimestamp(os.path.getmtime(f)).strftime('%Y-%m-%d %H:%M')
    flag = '/' if os.path.isdir(f) else ''
    print('%10d  %s  %s%s' % (fsize, mtime, f, flag))


print ('\n---------------------------------------------------')
# 序列化

import pickle

d = dict(name='Jack',age=12,score=90)
p = pickle.dumps(d)
print(p)

with open('../../dump.txt','wb') as f:
	pickle.dump(d,f)

with open('../../dump.txt','rb') as f:
	dt = pickle.load(f)
	print('loca pickle :' ,dt)


import json

j = json.dumps(d)
print('json-str:' , j)


with open('../../json.txt','w') as f:
	json.dump(d,f)

with open('../../json.txt','r') as f:
	dt = json.loads(f.read())
	print('loads json :' , dt)

with open('../../json.txt','r') as f:
	dt = json.load(f)
	print('load json :' , dt)





class Student(object):
    def __init__(self, name, age, score):
        self.name = name
        self.age = age
        self.score = score

def student2dict(std):
    return {
        'name': std.name,
        'age': std.age,
        'score': std.score
    }

s = Student('Bob', 20, 88)
sjson = json.dumps(s, default=student2dict)
# print('Student to json :' , sjson)
sjson = json.dumps(s, default=lambda obj: obj.__dict__)
print('Student to json :' , sjson)

rebuild = json.loads(sjson,object_hook=lambda d:Student(d['name'],d['age'],d['score']))
print('rebuild Student :' , rebuild)











