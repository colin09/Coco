# -*- coding: utf-8 -*-

__author__ = 'c'

from re import findall
from urllib.request import urlopen

url = 'http://www.yaochengwang.com/'
with urlopen(url) as fp:
    content = fp.read().decode()

# print(content)

pattern = '<img src="(.+?)"'
result = findall(pattern,content)

for index,item in enumerate(result):
    print(item)
    try:
        with urlopen(str(url+item)) as fp:
            with open('yc/'+str(index)+'.png','wb') as fp1:
                fp1.write(fp.read())
    except Exception as e:
        print(e)



