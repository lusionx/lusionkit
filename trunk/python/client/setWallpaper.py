# -*- coding: utf-8 -*-
#!/usr/bin/python

from ctypes import windll
from win32con import *


def setWallPaperFromBmp(pathToBmp):
    """ Given a path to a bmp, set it as the wallpaper """
    result = windll.user32.SystemParametersInfoA(SPI_SETDESKWALLPAPER, 0, pathToBmp , 0)
    if not result:
        raise Exception("Unable to set wallpaper.")
    
def changeImgFormat(path):
    pass

setWallPaperFromBmp('c:\\a.bmp');
