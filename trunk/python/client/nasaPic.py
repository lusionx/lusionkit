# -*- coding: utf-8 -*-
#!/usr/bin/python

"""下载nasa的每日一图"""

import httplib2
import BeautifulSoup

def info():
    url = 'http://apod.nasa.gov/apod/'
    h = httplib2.Http('.cache')
    resp, content = h.request(url, "GET")
    soup = BeautifulSoup.BeautifulSoup(content)
    p = soup.body.center.p.nextSibling
    imgurl = url + p.a['href']
    date = p.contents[0].strip('\n ')
    print imgurl
    resp, bytes = h.request(imgurl,'GET')
    return date, imgurl, bytes


import datetime
import os

def main():
    imgdate, imgurl, bytes = info()
    ext = os.path.splitext(imgurl)[1]
    dt = datetime.datetime.strptime(imgdate,'%Y %B %d').strftime('%Y-%m-%d')
    savepath = 'nasa/'+dt+ext
    f = open(savepath,'wb')
    f.write(bytes)
    f.close()

if __name__ == '__main__':
    main()


