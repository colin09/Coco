# -*- coding: utf-8 -*-

__author__ = 'c'


from random import randint
from os import listdir
from PIL import Image

watermark = 'yc/14.png'

img = Image.open(watermark)
img = img.convert("RGB")
width,height = img.size
print('width:'+ str(width) +', height:'+ str(height))

pixels = dict()


imgp = img.load()
for w in range(width):
    for h in range(height):
        #print(img.getpixel((w,h)))
        c=img.getpixel((w,h))[:3]
        if c!=(255,255,255):
            pixels[(w,h)]=c
            print(c)


def addWaterMark(srcDir):
    picFiles = [fn for fn in listdir(srcDir) if fn.endswith(('.jpg','.png'))]

    for pic in picFiles:
        print(srcDir+pic)
        wpic = Image.open(srcDir+pic)
        wpic = wpic.convert("RGB")
        w,h=wpic.size
        if w<width or h<height:
            continue
        
        points = {0:(0,0),1:((w-width)/2,(h-height)/2),2:(w-width,h-height)}
        position = randint(0,2)
        top,left=points.get(position,(0,0))

        for p,c in pixels.items():
            print(p[0],p[1],c)
            wpic.putpixel((p[0]+int(top),p[1]+int(left)),c)

        #保存加入水印之后的新图像文件
        wpic.save('waterImg/w_'+pic[:-4] + '_new' + pic[-4:])

addWaterMark('img/')


