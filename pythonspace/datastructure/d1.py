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

print(map(convert_to_str,[1,2,3,4]))

num1 = 12345
num2 = 67890

num_lst1 = [int(i) for i in str(num1)]
num_lst2 = [int(i) for i in str(num2)]

#两个lst中整数两两相乘
int_martix = [[i*j for i in num_lst1] for j in num_lst2]
print(int_martix)

str_martix = [map(convert_to_str,int_martix[i]) for i in range(len(int_martix))]
# str_martix = [convert_to_str(int_martix[i]) for i in range(len(int_martix))]
print(str_martix)

martix = [[int(str_martix[i][j][k]) for j in range(len(str_martix))] for i in range(len(str_martix)) for k in range(2)]
print(martix)