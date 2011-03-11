# -*- coding: utf-8 -*-
#!/usr/bin/python

"""下载nasa的每日一图"""

import httplib2
import BeautifulSoup
import datetime
import os

def info(url):
    h = httplib2.Http()
    print "load:" + url
    resp, content = h.request(url, "GET")
    soup = BeautifulSoup.BeautifulSoup(content)
    p = soup.body.center.p.nextSibling
    dir = os.path.split(url)[0] + '/'
    imgurl = dir + p.a['href']
    date = p.contents[0].strip('\n ')
    print 'imgurl:'+imgurl
    resp, bytes = h.request(imgurl,'GET')
    return date, imgurl, bytes


def save(url):
    imgdate, imgurl, bytes = info(url)
    ext = os.path.splitext(imgurl)[1]
    dt = datetime.datetime.strptime(imgdate,'%Y %B %d').strftime('%Y-%m-%d')
    savepath = 'nasa/'+dt+ext
    print 'save:'+savepath
    f = open(savepath,'wb')
    f.write(bytes)
    f.close()


def loadAll():
    #以往所有图片索引
    url = 'http://apod.nasa.gov/apod/archivepix.html'
    h = httplib2.Http('.cache')
    resp, content = h.request(url, "GET")
    soup = BeautifulSoup.BeautifulSoup(content)
    dir = os.path.split(url)[0]+'/'
    for a in soup.body.b.findAll('a'):
        print dir + a['href']
    
def saveAll():
    f = open('doc/nasaErr.txt','a')
    for line in open('doc/nasaAll.txt'):
        try:
            save(line.strip('\n'))
        except Exception:
            print "error:"+line
            f.write(line)
            f.flush()
        finally:
            print 'next'
    f.close()

if __name__ == '__main__':
    #某天
    #save('http://apod.nasa.gov/apod/ap110220.html')
    saveAll()
    


