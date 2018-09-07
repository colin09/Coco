# -*- coding: utf-8 -*-

__author__ = 'c'


from os import listdir
from PIL import Image

#
imgs = [Image.open(fn) for fn in listdir() if fn.endswith('.jpg')]

width, height = imgs[0].size

result = Image.new(imgs[0].mode,(width,height*len(imgs)))

for i ,img in enumerate(imgs):
    result.paste(img,box=(0,i*height))

result.save('r.jpg')

