#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__author__ = 'cc'

'web spider'


import urllib.request, re, time
from elasticsearch import Elasticsearch

# by default we connect to localhost:9200
es = Elasticsearch()

es = Elasticsearch([
	{'host': 'localhost'},
	{'host':'localhost','port':443,'url_prefix'='pes'}
	])

def read_page(url):
	#url = 'http://www.163.com/'
	print('read url: %s' % url)
	header = {'User-Agent':'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36'} 
	request = urllib.request.Request(url=url,headers=header)
	response = urllib.request.urlopen(request,timeout=10)
	'''
	print('geturl :' , res.geturl())
	print('info :' ,res.info())
	'''
	print('Code :' ,response.getcode())
	data =response.read()
	data = data.decode('utf-8')
	# print(data)

	lnklist = ''
	imglist = ''

	title = re.findall(r'<title>(.*?)</title>',data)
	keywords = re.findall(r'<meta name="keywords" .*? content="(.*?)"',data)
	description = re.findall(r'<meta name="description" .*? content="(.*?)"',data)
	# links = re.findall(r'<a*?href=("|\')http:[^s](*?)("|\')*?>(*?)</a>',data)
	# links = re.findall(r'<a .*?href=("http:[^s].*?").*?>(.*?)</a>',data)
	links = re.findall(r'<a .*?href=(".*?").*?>(.*?)</a>',data)
	# images = re.findall(r'(http:[^s]*?(jpg|png|gif))',data)
	images = re.findall(r'<img.*?src=("http:[^s].*?").*?>',data)

	for l in links:
		#print(l)
		a_url = l[0].replace('"','')
		if not a_url.startswith('http://'):
			a_url = url[0:url[7:].index('/')+7] + a_url

		lnklist += '<a href="%s">%s</a> \n' % (a_url,l[1]) 
		if a_url not in S:
			S.append(a_url)
			Q.append(a_url)
			print('append url: %s' % a_url)
	
	print('---%s--------------------------------------------------------------------' % title)
	print('---%s--------------------------------------------------------------------' % keywords)
	print('---%s--------------------------------------------------------------------' % description)

	for l in images:
		#print(l)
		imglist += '<img src=%s /> \n' % l
	
	fname ='txt/' + str(time.time())+'.txt'
	with open(fname,'w') as w:
		w.write(title[0]+'\n')
		w.write(lnklist)
		w.write('===============================================================')
		w.write(imglist)
		print('成功写入links.txt 文件！')

	# print(data)

	es.index(index='pyIndex',doc_type='pyType',id = 1,body={"title" :title,"href":lnklist})
	es.get(index='pyIndex',doc_type='pyType',id = 1)['_source']


S = []
Q = []
init_url = 'http://www.cnblogs.com/'

def main():
	count = 0
	while count < 1000:
		if len(Q)>0:
			try:
				url = Q[0]
				# print('url ==> %s' % url)
				read_page(url)
				print('over <====')
				count += 1
			except Exception as e:
				print(e)
			finally:
				Q.pop(0)
			
		else:
			print('. %s' % len(Q))
			time.sleep(1000)

Q.append(init_url)
S.append(init_url)
main()