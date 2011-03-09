# -*- coding: utf-8 -*-
#!/usr/bin/python

import ctypes
import Image
import os
import sys
import extPy


def wallpaper_dir(path):
    try:
        directory = os.listdir(path)
    except WindowsError:
        print "Directory does not exist"
        exit(0)
    wallpapers = []
    types = ['.jpg', '.png', '.gif']
    for file in directory:
        ext = os.path.splitext(file)[1]
        if ext in types:
            wallpapers.append(path + file)
    return wallpapers

import random

def pick_wallpaper(wallpapers):
    return random.choice(wallpapers)
    
def set_wallpaper(path):
    im = Image.open(path)
    path = 'wallpaper.bmp'
    im.save(path)
    ctypes.windll.user32.SystemParametersInfoA(20, 0, path , 0)


def main(argv):
    if len(argv) > 1:
        wallpapers = wallpaper_dir(argv[1]);
        if len(wallpapers) > 0:
            path = pick_wallpaper (wallpapers)
            set_wallpaper(path)
        else:
            print "No wallpapers were found."
    else:
        print "Usage: wallpaper.py <dir>"

if __name__ == "__main__":
    #main(sys.argv)
    main(['',os.path.todir(u'D:\\admin\\Pictures\\w我的壁纸不可能这么萌\\001')])
