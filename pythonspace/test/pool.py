#!/usr/bin/env python3
# -*- coding: utf-8 -*-

print ('\n---------------------------------------------------')

import os

# fork
'''
# Unix/Linux操作系统提供了一个fork()系统调用
# 由于Windows没有fork调用，下面的代码在Windows上无法运行
# 由于Mac系统是基于BSD（Unix的一种）内核，所以，在Mac下运行是没有问题的

print('Process (%s) start...' % os.getpid())
# Only works on Unix/Linux/Mac:
pid = os.fork()
if pid == 0:
    print('I am child process (%s) and my parent is %s.' % (os.getpid(), os.getppid()))
else:
    print('I (%s) just created a child process (%s).' % (os.getpid(), pid))
'''

# multiprocessing

from multiprocessing import Process

def run_proc(name):
	print ('Run child process %s (%s)' % (name,os.getpid()))

if __name__ == '__main__1':
	print('Parent process %s .' % os.getpid())
	p = Process(target=run_proc,args=('test',))
	print('Child process will start.')
	p.start()
	p.join()  # 等待子进程结束后再继续往下运行
	print('Child process end.')

# Pool 进程池

from multiprocessing import Pool
import time,random

def long_time_task(name):
	print('Run task %s (%s)...' % (name,os.getpid() ))
	start = time.time()
	time.sleep(random.random()*3)
	end = time.time()
	print ('Task %s runs %0.2f seconds.' %( name ,end - start))

if __name__ == '__main__2':
	print('Parent process %s.' % os.getpid())
	p = Pool(4)
	for i in range(6):
		p.apply_async(long_time_task,args=(i,))
	print('Waiting for all subprocesses done...')
	p.close()
	p.join()
	print('All subprocesses done.')


import subprocess

# subprocess模块可以让我们非常方便地启动一个子进程，然后控制其输入和输出

print('$ nslookup www.python.org')
r = subprocess.call(['nslookup','www.python.org'])
print('Exit code:', r)



'''
print('$ nslookup')
p = subprocess.Popen(['nslookup'], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
output, err = p.communicate(b'set q=mx\npython.org\nexit\n')
print(output.decode('utf-8'))
print('Exit code:', p.returncode)
'''

# 进程间通信
# Python的multiprocessing模块包装了底层的机制，提供了Queue、Pipes等多种方式来交换数据
from multiprocessing import Queue


def write(q):
	print('Process to write: %s' % os.getpid())
	for value in ['A','B','C','D']:
		print('Put %s to queue' % value)
		q.put(value)
		time.sleep(random.random())

def read(q):
	print('Process to Read: %s' % os.getpid())
	while True:
		value = q.get(True)
		print('Get %s from queue' % value)

if __name__ == '__main__3':
	q = Queue()
	qw = Process(target=write,args=(q,))
	qr = Process(target=read,args=(q,))
	
	qw.start()
	qr.start()
	
	# qr.join()
	qw.join()
	# pr进程里是死循环，无法等待其结束，只能强行终止:
	qr.terminate()



print ('\n---------------------------------------------------')
	
# 多线程

import threading

def loop():
	print('thread %s is running...' % threading.current_thread().name)
	for n in range(6):
		print('thread %s ==>> %s' %( threading.current_thread().name , n))
		time.sleep(1)
	print('thread %s ended.' % threading.current_thread().name)

	
print('thread %s is running...' % threading.current_thread().name)
t = threading.Thread(target=loop,name='LoopThread')
t.start()
t.join()
print('thread %s ended.' % threading.current_thread().name)


lock = threading.Lock()
balance = 0

def change_it(n):
	global balance
	balance += n
	balance -= n
	balance = balance + n
	balance = balance - n

def run_thread(n):
	print('thread %s is running...' % threading.current_thread().name)
	for i in range(100000):
		# 先要获取锁:
		lock.acquire()
		try:
			change_it(n)
		finally:
			# 改完了一定要释放锁:
			lock.release()
	print('thread %s ended.' % threading.current_thread().name)

t1 = threading.Thread(target=run_thread,args=(5,))
t2 = threading.Thread(target=run_thread,args=(9,))

t1.start()
t2.start()

t1.join()
t2.join()

print(balance)


print ('\n---------------------------------------------------')
import  multiprocessing

def loops():
    x = 0
    while True:
        x = x ^ 1

'''
for i in range(multiprocessing.cpu_count()):
    t = threading.Thread(target=loops)
    t.start()
'''

local_school = threading.local()

def process_student():
	std = local_school.student
	print('hello , %s (in %s)' % (std,threading.current_thread().name))

def process_thread(name):
    # 绑定ThreadLocal的student:
    local_school.student = name
    process_student()

t1 = threading.Thread(target= process_thread, args=('Alice',), name='Thread-A')
t2 = threading.Thread(target= process_thread, args=('Bob',), name='Thread-B')
t1.start()
t2.start()
t1.join()
t2.join()


