#!/usr/bin/env python 
# -*- coding: utf-8 -*-

__auth__ = 'cc'

'redis'


'''
from rediscluster import StrictRedisCluster

# Requires at least one node for cluster discovery. Multiple nodes is recommended.
startup_nodes = [{"host": "192.168.1.182", "port": "6379"}]

# Note: decode_responses must be set to True when used with python3
rc = StrictRedisCluster(startup_nodes=startup_nodes, decode_responses=True)

rc.set("foo", "bar")

print(rc.get("foo"))
'''

import redis
r = redis.StrictRedis(host='192.168.1.182', port=6379, db=0)
r.set('foo', 'bar')

r.get('foo')