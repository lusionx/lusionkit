# -*- coding: utf-8 -*-
#!/usr/bin/python

import ctypes
import Image
import os
import sys
import extPy
from lxml import etree


def getWallpapers(dirpath):
    try:
        directory = os.listdir(dirpath)
    except WindowsError:
        print "Directory does not exist"
        exit(0)
    wallpapers = []
    types = ['.jpg', '.png']
    for file in directory:
        ext = os.path.splitext(file)[1]
        if ext in types:
            wallpapers.append(dirpath + file)
    return wallpapers
    
def set(path):
    im = Image.open(path)
    path = 'wallpaper.bmp'
    im.save(path)
    ctypes.windll.user32.SystemParametersInfoA(20, 0, path , 0)


def main(argv):
    cfg = os.path.dirname(__file__) + '/doc/Wallpapers.xml'
    doc = etree.parse(cfg)
    pics = []
    for a in doc.getroot()[0]:
        pics.extend(getWallpapers(a.text))
    try:
        i = pics.index(doc.getroot()[1].text) + 1
        i = i % len(pics)
    except ValueError:
        i = 0
    path = pics[i]
    set(path)
    doc.getroot()[1].text = path
    doc.write(cfg,encoding = "utf-8")


if __name__ == "__main__":
    #main(sys.argv)
    main('')
