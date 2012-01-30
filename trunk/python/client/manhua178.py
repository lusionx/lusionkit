# coding: utf-8

import httplib2
from BeautifulSoup import BeautifulSoup
import argparse, json, os

def loadDir(u):
    h = httplib2.Http(".cache")
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    soup = BeautifulSoup(content)
    links = []
    for div in soup.findAll('div',attrs={'class':'cartoon_online_border'}):
        for a in div.findAll('a'):
            links.append((a['href'],a.string))
    links = sorted(links,key = lambda x:x[0])
    for k,v in links:
        print '%s : %s' % (k,v)
        
def downImg(u):
    h = httplib2.Http(".cache")
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    content = content.split('\n')
    ss = [line for line in content if line[:12]=='var pages = '][0][13:-3]
    title = [line for line in content if line[:21]=='var g_chapter_name = '][0][22:-3]
    ss = json.loads(ss)
    domain = u'http://imgfast.manhua.178.com/'
    for a in ss:
        u = domain + a
        s = title + u'_' + os.path.split(a)[1]
        print 'Download %s to %s' % (u,s)
        resp, byts = h.request(u, "GET")
        f = open(s,'wb')
        f.write(byts)
        f.close()
    
        
if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('-l', action="store", dest='url', help='load list')
    parser.add_argument('-d', action="store", dest='url2', help='down img')
    results = parser.parse_args()
    if results.url :
        loadDir(results.url)
    if results.url2 :
        downImg(results.url2)
    