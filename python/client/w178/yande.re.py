# coding: utf-8

import httplib2, urllib
from BeautifulSoup import BeautifulSoup
import argparse, json, os

#https://yande.re/data/preview/ec/46/ec462f5f8b538642ee33ec35688a578d.jpg

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('id', nargs=1, help='https://yande.re/post/show/[id]')
    results = parser.parse_args()
    u = results.URL[0]
    if len(u) < 5 or u[:4] != u'http':
        u = 'http://manhua.7k7k.com/manhua/%s.html' % u
    if results.l :
        loadDir(u)
    if results.i:
        loadInfo(u)
    if results.d :
        downImg(results.URL[0])
if __name__ == '__main__':
    main()