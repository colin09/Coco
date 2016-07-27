#!/usr/bin/env python3
# -*- coding: utf-8 -*-

print ('\n---------------------------------------------------')

import pymysql

conn = pymysql.connect(host='rdsxldfm1u8zne43r6b5.mysql.rds.aliyuncs.com',user='opc_demo',password='opcdemo_123',db='magichorse_demo',charset='utf8')

''' # 官方 Example
import pymysql.cursors

# Connect to the database
connection = pymysql.connect(host='localhost',user='user',password='passwd',
                             db='db',charset='utf8mb4',cursorclass=pymysql.cursors.DictCursor)

try:
    with connection.cursor() as cursor:
        # Create a new record
        sql = "INSERT INTO `users` (`email`, `password`) VALUES (%s, %s)"
        cursor.execute(sql, ('webmaster@python.org', 'very-secret'))

    # connection is not autocommit by default. So you must commit to save
    # your changes.
    connection.commit()

    with connection.cursor() as cursor:
        # Read a single record
        sql = "SELECT `id`, `password` FROM `users` WHERE `email`=%s"
        cursor.execute(sql, ('webmaster@python.org',))
        result = cursor.fetchone()
        print(result)
finally:
    connection.close()
'''


try:
	with conn.cursor() as cursor:
		sql = "select * from `hotword`"
		cursor.execute(sql)
		# result = cursor.fetchone()
		result = cursor.fetchall()
		for row in result:
			print(row)
finally:
	conn.close()
		



print ('\n---------------------------------------------------')


























