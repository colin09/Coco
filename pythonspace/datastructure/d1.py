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


ss = "abc ttt,kmd,uuu xyz;dfe";
print(ss.split(','))
print(re.split(',| |;',ss))