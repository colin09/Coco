#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__author__ = 'cc'

'web spider'

import urllib,urllib3,re

queue Q
set S

user_agent = 'Mozilla/4.0 (compatible; MSIE 5.5; Windows NT)'  
headers = { 'User-Agent' : user_agent } 
url = 'http://www.163.com/'
http = urllib3.PoolManager()

r = http.request('GET', url, headers)

print ('state:' , r.status)

for key in r.headers:
	print(key,' ==> ', r.headers[key])

for d in dir(r):
	print(d)


print('---------------------------------------------')

for d in dir(urllib3.request):
	print(d)

data = urllib3.request.urlopen(url).read()
data = data.decode('UTF-8')
print(data)














m = re.match(r'hello','hello,were')
print(m.group())

# 匹配如下内容：单词+空格+单词+任意字符  
m = re.match(r'(\w+) (\w+)(?P<sign>.*)', 'hello world!')  
  
print("m.string:", m.string)
print("m.re:", m.re)
print("m.pos:", m.pos)
print("m.endpos:", m.endpos)
print("m.lastindex:", m.lastindex)
print("m.lastgroup:", m.lastgroup)

print("m.group():", m.group())
print("m.group(1,2):", m.group(1, 2))
print("m.groups():", m.groups())
print("m.groupdict():", m.groupdict())
print("m.start(2):", m.start(2))
print("m.end(2):", m.end(2))
print("m.span(2):", m.span(2))
print(r"m.expand(r'\g<2> \g<1>\g<3>'):", m.expand(r'\2 \1\3'))