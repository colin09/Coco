__author__ = 'coco'

# /article/datastructure/31546


array = [4,6,1,7,2,9,3,8]

print(array)

# 插入排序
for j in range(1,len(array)):
	key = array[j]
	i=j-1
	while i>=0 and array[i] >key :
		array[i+1] = array[i]
		i -= 1
	array[i+1] =key

print(array)

print('# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # ')
print('\n正则表达式 ====>\n')

import re


# 将正则表达式编译成Pattern对象
pattern = re.compile(r'hello')
 
# 使用Pattern匹配文本，获得匹配结果，无法匹配时将返回None
match = pattern.match('hello world!')
 
if match:
    # 使用Match获得分组信息
    print(match.group())




# pattern = re.compile('(\d+)')
match = re.match(r'(\d+)','c:/abc/xyz/k[11..19].dat')
# print(match.group())



fs = 'c:/abc/xyz/k[11..19].dat'
pattern = re.compile(r"\[([0-9]+)\.\.([0-9]+)\]")
match = pattern.search(fs)
if match:
	start = match.group(1)
	end = match.group(2)
	for i in range(int(start),int(end)):
		print(fs.replace(match.group(),str(i)))
else:
	print('no match')


print('\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # \n')
print('冒泡排序 ====>\n')

array = [4,6,1,7,2,9,3,8]

for i in range(len(array)-1,-1,-1):
	for j in range(i):
		if array[j] > array[j+1]:
			tem = array[j]
			array[j] = array[j+1]
			array[j+1] = tem

print(array)


print('\n简单选择排序，与冒泡排序相比交换的次数少了====>\n') 
array = [4,6,1,7,2,9,3,8]

for i in range(len(array)):
	minIndex = i
	for j in range(i+1,len(array)):
		if array[j] < array[minIndex]:
			minIndex = j
	if minIndex != i:
		tem = array[i]
		array[i] = array[minIndex]
		array[minIndex] = tem

print(array)


print('\n二分法查找 7 ====>\n') 
begin = 0
end = len(array)-1
num = 7

while end>begin:
	mid = int((begin+end)/2)
	if array[mid] == num:
		print(mid)
		break
	elif array[mid] > num:
		end = mid - 1 
	elif array[mid] < num:
		begin = mid + 1



print('\n字符串拆分 ====>\n')

ss = "abc ttt,kmd,uuu xyz;dfe";
print(ss)
print(ss.split(','))
print(re.split(',| |;',ss))





print('\n归并排序 - 1 ====>\n')

def merge_sort1(arr):
	if len(arr)<=1:
		return arr
	mid = int(len(arr)/2)
	left = merge_sort1(arr[:mid]) 
	right = merge_sort1(arr[mid:])
	return merge1(left,right)

def merge1(left,right):
	result = []
	i,j=0,0
	while i<len(left) and j < len(right):
		if left[i] <= right[j]:
			result.append(left[i])
			i += 1
		else:
			result.append(right[j])
			j += 1
	result += left[i:]
	result += right[j:]
	return result

array = [4,5,7,9,7,5,1,0,7,-2,3,-99,6]
print(array)
print(merge_sort1(array))


print('\n归并排序 - 2 ====>\n')

def merge_sort2(arr):
	if len(arr) <=1:
		return arr

	def merge(left,right):
		result = []

		while left and right:
			result.append(left.pop(0) if left[0] <= right[0] else right.pop(0))

		while left:
			result.append(left.pop(0))

		while right:
			result.append(right.pop(0))

		return result

	mid = int(len(arr) / 2)
	left = merge_sort2(arr[:mid])
	right = merge_sort2(arr[mid:])
	return merge(left,right)

array = [4,5,7,9,7,5,1,0,7,-2,3,-99,6]
print(merge_sort2(array))


print('\n归并排序 - 3 ====>\n')
# python的模块heapq中就提供了归并排序的方法

from heapq import merge

def merge_sort3(arr):
	if len(arr) <=1:
		return arr

	mid = int(len(arr)/2)
	left = merge_sort3(arr[:mid])
	right = merge_sort3(arr[mid:])
	return list(merge(left,right))  # heapq.merge()

array = [4,5,7,9,7,5,1,0,7,-2,3,-99,6]
print(merge_sort3(array))


print('\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # \n')
print('20位学员的5门课的成绩 ====>\n')

import random

course_list = ['c','j','n','p','g']
course_num = len(course_list)
student_num = 20

score_list1 = [[random.randint(0,100) for i in range(student_num)] for j in range(len(course_list))]
for i in range(len(course_list)):
	print('score of every one in %s' %course_list[i])
	print('   ==> %s' %score_list1[i])

'''
score_list2 = [[random.randint(0,100) for i in range(len(course_list))] for j in range(student_num)]
for i in range(student_num):
	print('\n score of every student %s' %(i+1))
	print('   ==> %s' %score_list2[i])
'''

all_score = [score_list1[i][j] for i in range(course_num) for j in range(student_num)]
# all_total = sum([score_list1[i][j] for i in range(course_num) for j in range(student_num)])

all_total = sum(all_score)
print('all total score : %s' %all_total)
print('all avg score : %s' %(all_total / student_num / course_num))

student_score_list = [[score_list1[i][j] for i in range(course_num)] for j in range(student_num)]
student_avg = [sum(student_score_list[i]) / len(student_score_list[i]) for i in range(student_num)]
print('student avg score : %s' % student_avg)

for i in range(student_num):
	print('%s : %s , sum=%s , avg=%s' %(i,student_score_list[i],sum(student_score_list[i]),sum(student_score_list[i]) / len(student_score_list[i])))



print('\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # \n')
print('数组中的元素都向前移一位 ====>\n')

array = [1,2,3,4,5,6,7,8,9,'a','b','c','d','e','f']
print(array)
f = array.pop(0)
array.append(f)
print(array)

stu_num = 40
stu_score = [random.randint(0,100) for i in range(stu_num)]
avg_score = sum(stu_score) / stu_num
print(stu_score)
print('avg score: %s' %avg_score)
less_score = [s for s in stu_score if s < avg_score]
print(less_score)


print('\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # \n')
print('大整数相乘 ====>\n')

'''
Python支持“无限精度”的整数，一般情况下不用考虑整数溢出的问题,
而且Python Int类型与任意精度的Long整数类可以无缝转换，超过Int 范围的情况都将转换成Long类型。
“无限精度”是有引号的。事实上也是有限制的，对于32位的机器，其上限是：2^32-1。真的足够大了。
'''
print(2899887676637907866*1788778992788348277389943)

def convert_to_str(num):
	if num < 10:
		return '0'+str(num)
	else:
		return str(num)

def summ_left(lst):
	summ = []
	x = [i for i in range(len(lst))]
	y = [j for j in range(len(lst[0]))]
	sx = [i for i in x if i%2==0]
	for i in sx:
		s = 0
		j = 0
		while i>=0 and j<=y[-1]:
			s = s + lst[i][j]
			if i%2 ==1:
				j = j+1
			else:
				j = j
			i = i-1
		summ.append(s)
	return summ

def summ_end(lst):
	summ = []
	y = [j for j in range(len(lst[0]))]
	ex = len(lst) - 1
	for m in range(len(y)):
		s = 0
		i = ex
		j = m
		while i>=0 and j <= y[-1]:
			s += lst[i][j]
			if i%2 ==1:
				j = j+1
			else:
				j=j
			i = i-1
		summ.append(s)
	return summ

def take_digit(lst):
	tmp = 0
	digit_list = []
	for m in range(len(lst)):
		lstm = 0
		lstm = lst[m]+tmp
		if lstm < 10:
			tmp = 0
			digit_list.append(str(lstm))
		else:
			tmp = lstm/10
			mm = lstm - tmp*10
			digit_list.append(str(mm))
	return digit_list




print('\n计算 12345 × 67890 ====>')

num1 = 12345
num2 = 67890

num_lst1 = [int(i) for i in str(num1)]
num_lst2 = [int(i) for i in str(num2)]

print('\n两个lst中整数两两相乘 ')
int_martix = [[i*j for i in num_lst1] for j in num_lst2]
print(int_martix)

print('\n将上述元素为数字的list转化为元素类型是str，主要是将9-->09  ')
str_martix = [list(map(convert_to_str,int_martix[i])) for i in range(len(int_martix))]
print(str_martix)

print("\n将上述各个list中的两位数字分开：['01','29','03']-->[0,2,0],[1,9,3] ")
martix = [[int(str_martix[i][j][k]) for j in range(len(str_martix))] for i in range(len(str_martix)) for k in range(2)]
print(martix)

print('\n计算阿拉伯乘法表的左侧开始各项和')
sum_left = summ_left(martix)
print(sum_left)

print('\n计算阿拉伯乘法表的底部开始各项和')
sum_end = summ_end(martix)
print(sum_end)

print('\n将上述两个结果合并后翻转')
sum_left.extend(sum_end)
sum_left.reverse()
print(sum_left)

print('\n取得各个和的个位的数字（如果进位则加上）')
result = take_digit(sum_left)
print(result)

print('\n翻转结果并合并为一个结果字符串数值')
result.reverse()
int_result = "".join(result)
print('%d × %d = %s' %(num1,num2,int_result))
print(int_result)
print(num1 * num2)





print('\n# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # \n')
print('二叉树 ====>\n')

class Node:
	def __init__(self,data):
		# 二叉树节点结构，包括：左枝、右枝、节点数据三个变量
		self.left = None
		self.right = None
		self.data = data

	def insert(self,data):
		# 
		if data < self.data:
			if self.left is None:
				self.left = Node(data)
			else:
				self.left.insert(data)
		elif data > self.data:
			if self.right is None:
				self.right = Node(data)
			else:
				self.right.insert(data)

	def search(self,data,parent=None):
		#
		if data < self.data:
			if self.left is None:
				return None ,None
			return self.left.search(data,self)
		elif data > self.data:
			if self.right is None:
				return None,None
			return self.right.search(data,self)
		else:
			return self,parent

	def children_count(self):
		cnt = 0
		if self.left:
			cnt += 1
		if self.right:
			cnt += 1
		return cnt

	def delete(self,data):
		node ,parent = self.search(data)
		if node is not None:
			children_count = node.children_count()
			if children_count == 0 :
				if parent.left is node:
					parent.left = None
				else:
					parent.right = None
				del node
			elif children_count == 1:
				if node.left:
					n = node.left
				else:
					n = node.right
				if parent:
					if parent.left is node:
						parent.left = n
					else:
						parent.right = n
				del node
			else:
				parent = node
				successor = node.right
				while successor.left:
					parent=successor
					successor = successor.left
				node.data = successor.data
				if parent.left == successor:
					parent.left = successor.right
				else:
					parent.right = successor.right
		return self

	def compare_trees(self,node): # 比较两个二叉树
		if node is None:
			return False
		if self.data != node.data:
			return False
		res = True
		if self.left is None:
			if node.left:
				return Fasel
		else:
			res = self.left.compare_trees(self.left)
		if res is False:
			return False
		if self.right is None:
			if data.right:
				return False
		else:
			res = self.right.compare_trees(self.right)
		return res

	def tree_data(self):
		stack = []
		node = self
		while stack or node:
			# print('%s - %s' %(stack , node))
			if node:
				stack.append(node)
				node = node.left
			else:
				node = stack.pop()
				yield node.data
				node = node.right
	
	def minValue(self):
		node = self
		while node.left is not None:
			node = node.left
		return node.data

	def maxDepth(self):
		root = self
		if root is None:
			return 0
		else:
			ldepth = root.left.maxDepth()
			rdepth = root.right.maxDepth()
			return max(ldepth,rdepth)


root = Node(8)
root.insert(3)
root.insert(10)
root.insert(1)

root.insert(6)
root.insert(4)
root.insert(7)
root.insert(14)
root.insert(13)

# print(root)
print('\n search ==>')
print(root.search(6))

print('\n delete ==>')
print(root.delete(1))

print('\n 所有树元素的生成器 ==>')
for data in root.tree_data():
    print(data)

print('\n min value ==>')
print(root.minValue())

print('\n max depth ==>')
print(root.maxDepth())