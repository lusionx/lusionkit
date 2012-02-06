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
    links = sorted(links,key = lambda x:x[1])
    for k,v in links:
        print '%s : %s' % (k,v)
        
def downImg(u):
    h = httplib2.Http()
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    content = content.split('\n')
    ss = [line for line in content if line[:12]=='var pages = '][0][13:-3]
    title = [line for line in content if line[:21]=='var g_chapter_name = '][0][22:-3]
    ss = json.loads(ss)
    domain = u'http://imgfast.manhua.178.com/'
    for a in ss:
        f = open(title + u'_' + os.path.split(a)[1],'wb')
        print 'Download %s to %s' % (a, f.name)
        resp, byts = h.request(domain + a, "GET")
        f.write(byts)
        f.close()
    
        
if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument('-l', action="store_true", help='load list from URL')
    group.add_argument('-d', action="store_true", help='down imgs from URL')
    parser.add_argument('URL', nargs=1, help='[http://manhua.178.com/]<xxxx> | URL')
    results = parser.parse_args()
    if results.l :
        u = results.URL[0]
        if u[:4] != 'http':
            u = 'http://manhua.178.com/' + u
        loadDir(u)
    if results.d :
        downImg(results.URL[0])
    