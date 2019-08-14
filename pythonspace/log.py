#!/usr/bin/env python 
# -*- coding: utf-8 -*-

# python log.py


import re
import os
import time

print ('\n---------------------------------------------------')



# 遍历指定目录，显示目录下的所有文件名
def eachFile(rootPath):
    pathDir =  os.listdir(rootPath)
    for allDir in pathDir:
        child = os.path.join('%s%s' % (rootPath, allDir))
        print(child) # .decode('gbk')是解决中文显示乱码问题
        readLogFile(child)


def readLogFile(filePath):
    msg = '非配送完成订单'
    with open('C:\\Users\\Colin\\Desktop\\'+str(time.time())+'.sql', 'w', encoding='UTF-8') as w:
        w.write('INSERT INTO [Message]( [MessageType], [FailedMessage], [Locked], [CreateTime], [Category], [Body]) VALUES')
        w.write('\n')
        with open(filePath, 'r', encoding='UTF-8') as f:
            for line in f.readlines():
                flag = line.find(msg) >= 0
                if flag :
        #            print(line)
                    message = re.findall(r"request(.+?)返回结果", line)
                    body = message[0].strip('：,，')
                    body = body.replace('"','\\"')
                    body = '(255, \'非配送完成订单\', \'0\', GETDATE(), 1,\'{"messageId":null,"syncType":"RabbitMQ_SCMJiupiOrderComplete","messageSource":2,"syncData":"'+body+'"}\'),'
                    print(body)
                    w.write(body)
                    w.write('\n')
                    print('---------------------------------------------------------')
    w.close()


rootPath = 'C:\\Users\\Colin\\Desktop\\log\\'
eachFile(rootPath)

