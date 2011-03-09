# -*- coding: utf-8 -*-
#!/usr/bin/python

import ctypes
import Image
import os
import sys
import random
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

def set_wallpaper(wallpapers):
    random.seed()
    random.shuffle(wallpapers)
    wallpaper = random.sample(wallpapers,1)
    im = Image.open(wallpaper[0])
    path = 'wallpaper.bmp'
    im.save(path)
    ctypes.windll.user32.SystemParametersInfoA(20, 0, path , 0)


def main(argv):
    if len(argv) > 1:
        wallpapers = wallpaper_dir(argv[1]);
        if len(wallpapers) > 0:
            set_wallpaper(wallpapers)
        else:
            print "No wallpapers were found."
    else:
        print "Usage: wallpaper.py <dir>"

if __name__ == "__main__":
    #main(sys.argv)
    main(['',os.path.todir(u'D:/admin/Pictures/h和谐社/MoeGril')])
