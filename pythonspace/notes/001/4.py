#-*- coding: utf-8 -*-

import re
import csv
import requests
from tqdm import tqdm
from urllib.parse import urlencode
from requests.exceptions import RequestException
from bs4 import BeautifulSoup

def getOnePage(city,keyWord,region,page):
    paras={
        'jl':city,'kw':keyWord,
        'isadv':0,'isfilter':1,
        'p':page,'re':region
    }
    headers = {
       'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36',
       'Host': 'sou.zhaopin.com',
       'Referer': 'https://www.zhaopin.com/',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8',
       'Accept-Encoding': 'gzip, deflate, br',
       'Accept-Language': 'zh-CN,zh;q=0.9'
    }
    #url = 'https://sou.zhaopin.com/jobs/searchresult.ashx?' + urlencode(paras)
    url = 'https://sou.zhaopin.com/jobs/searchresult.ashx?' 
    try:
        #response = requests.get(url,headers=headers)
        response = requests.get(url, params=paras, headers=headers)
        if response.status_code==200:
           return response.text
        return None
    except RequestException as e:
        print(e)
        return None

def getDeatilPage(url):
    headers = {
       'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36',
       'Host': 'jobs.zhaopin.com',
       'Referer': 'https://www.zhaopin.com/',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8',
       'Accept-Encoding': 'gzip, deflate, br',
       'Accept-Language': 'zh-CN,zh;q=0.9'
    }
    try:
        #response = requests.get(url,headers=headers)
        response = requests.get(url, headers=headers)
        
        if response.status_code==200:
           #print(response.text)
           return response.text
        return None
    except RequestException as e:
        print(e)
        return None
   

def parsePage(html):
    # 正则表达式进行解析
    #pattern = re.compile('<a style=.*? target="_blank">(.*?)</a>.*?'        # 匹配职位信息
    #    '<td class="gsmc"><a href="(.*?)" target="_blank">(.*?)</a>.*?'     # 匹配公司网址和公司名称
    #    '<td class="zwyx">(.*?)</td>', re.S)

    pattern = re.compile('<td class="zwmc".*?href="(.*?)" target="_blank">(.*?)</a>.*?' # 匹配职位详情地址和职位名称
        '<td class="gsmc">.*? target="_blank">(.*?)</a>.*?'                             # 匹配公司名称
        '<td class="zwyx">(.*?)</td>', re.S)

    items= re.findall(pattern,html)
    for item in items:
        jobName = item[1]
        jobName = jobName.replace('<b>', '')
        jobName = jobName.replace('</b>', '')
        yield {
            'url':item[0],
            'job':jobName,
            #'web':item[1],
            'com':item[2],
            'salary':item[3]
        }

def getJobDetail(html):
    soup = BeautifulSoup(html,'html.parser')
    #print (soup.find_all('ul',class_='terminal-ul clearfix'))
    years=''
    edu=''
    requirement = ''
    for ul in soup.find_all('ul',class_='terminal-ul clearfix'):
        #ul=soup.find_all('ul',class_='terminal-ul clearfix'):
        lst = ul.find_all('strong')
        years = lst[4].get_text()
        edu = lst[5].get_text()

     # 筛选任职要求
    for terminalpage in soup.find_all('div', class_='terminalpage-main clearfix'):
        for box in terminalpage.find_all('div', class_='tab-cont-box'):
            cont = box.find_all('div', class_='tab-inner-cont')[0]
            ps = cont.find_all('p')
            # "立即申请"按钮也是个p标签，将其排除
            for i in range(len(ps) - 1):
                requirement += ps[i].get_text()#.replace("\n", "").strip()   # 去掉换行符和空格

    # 筛选公司规模，该标签内有四个或五个<li>标签，但是第一个就是公司规模
    scale = '' #soup.find(class_='terminal-ul clearfix terminal-company mt20').find_all('li')[0].strong.get_text()
    return {'years': years, 'education': edu, 'requirement': requirement, 'scale': scale}

def writeCsv(path,header,rows):
    with open(path,'a',encoding='gb18030',newline='') as f:
        f_csv= csv.DictWriter(f,header)
        f_csv.writeheader()
        f_csv.writerows(rows)

def writeCsvHeader(path,header):
    with open(path,'a',encoding='gb18030',newline='') as f:
        f_csv= csv.DictWriter(f,header)
        f_csv.writeheader()

def writeCsvRows(path,header,rows):
    with open(path,'a',encoding='gb18030',newline='') as f:
        f_csv= csv.DictWriter(f,header)
        if type(rows) == type({}):
            f_csv.writerow(rows)
        else:
            f_csv.writerows(rows) 

def writeTxt(path, txt):
    with open(path, 'a', encoding='gb18030') as f:
        f.write(txt)

def main(city,keyWord,region,pages):
    filecCsv = 'csv/'+city+'_'+keyWord+'.csv'
    fileTxt = 'csv/'+city+'_'+keyWord+'.txt'
    headers = ['job', 'years', 'education', 'salary', 'company', 'scale', 'job_url']
    writeCsvHeader(filecCsv, headers)
    for i in tqdm(range(pages)):
        html = getOnePage(city,keyWord,region,i)
        items = parsePage(html)
        job={}
        for item in items:
            detailHtml = getDeatilPage(item.get('url'))
            if detailHtml== None:
                continue
            jobDetail = getJobDetail(detailHtml)

            job['job'] = item.get('job')
            job['salary'] = item.get('salary')
            job['company'] = item.get('com')
            job['job_url'] = item.get('url')
            #job['years'] = jobDetail.get('years')
            #job['education'] = jobDetail.get('education')
            #job['scale'] = jobDetail.get('scale')

        writeCsvRows(filecCsv,headers,job)
        writeTxt(fileTxt,jobDetail.get('requirement'))
        print(jobDetail.get('requirement'))


def main2(city,keyWord,region,pages):
    filecCsv = 'csv/'+city+'_'+keyWord+'.csv'
    fileTxt = 'csv/'+city+'_'+keyWord+'.txt'
    headers = ['job', 'years', 'education', 'salary', 'company', 'scale', 'job_url']
    writeCsvHeader(filecCsv, headers)
    for i in tqdm(range(pages)):
        html = getOnePage(city,keyWord,region,i)
        items = parsePage(html)
        job={}
        for item in items:
            detailHtml = getDeatilPage(item.get('url'))
            if detailHtml== None:
                continue
            jobDetail = getJobDetail(detailHtml)

            job['job'] = item.get('job')
            job['salary'] = item.get('salary')
            job['company'] = item.get('com')
            job['job_url'] = item.get('url')
            job['years'] = jobDetail.get('years')
            job['education'] = jobDetail.get('education')
            job['scale'] = jobDetail.get('scale')
            job['desc'] = jobDetail.get('requirement')

            if job['salary'].find('-')>0:
                s = job['salary'].split("-")
                if(int(s[1])>100*100*1.5):
                    print(job)

if __name__ == '__main__':
    #main('武汉','C#',0,10)
    main2('武汉','python',0,10)