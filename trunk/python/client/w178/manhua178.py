# coding: utf-8

import httplib2
from BeautifulSoup import BeautifulSoup
import argparse, json, os

def loadDir(u):
    h = httplib2.Http()
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    soup = BeautifulSoup(content)
    links = []
    for div in soup.findAll('div', attrs={'class':'cartoon_online_border'}):
        for a in div.findAll('a'):
            links.append((a['href'],a.string))
    #links = sorted(links,key = lambda x:x[1].replace(u'第',''))
    for k,v in links:
        print 'cmd -d %s %s' % (k,v)
    return links

def loadInfo(u):
    h = httplib2.Http(".cache")
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    soup = BeautifulSoup(content)
    title = soup.find('h1').string
    soup = soup.find('div', attrs={'class':'anim-main_list'})
    tds = soup.findAll('td')
    print u'%s by %s, states %s, last %s' % (title, tds[2].find().string, tds[4].find().string, tds[8].find().string)
    
def downImg(u):
    h = httplib2.Http()
    resp, content = h.request(u, "GET")
    content = content.decode('utf-8')
    content = content.split('\n')
    ss = [line for line in content if line[:12]=='var pages = '][0][13:-3]
    title = [line for line in content if line[:21]=='var g_chapter_name = '][0][22:-3]
    ss = json.loads(ss)
    domain = u'http://imgfast.manhua.178.com/'
    i = 0
    for a in ss:
        i+=1
        f = open(title + u'_' + os.path.split(a)[1],'wb')
        print '%s/%s %s to %s ' % (i, len(ss), a, f.name, )
        resp, byts = h.request(domain + a, "GET")
        f.write(byts)
        f.close()
    
        
def main():
    parser = argparse.ArgumentParser()
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument('-l', action="store_true", help='load list from URL')
    group.add_argument('-i', action="store_true", help='show URL info ')
    group.add_argument('-d', action="store_true", help='down imgs from URL')
    parser.add_argument('URL', nargs=1, help='[http://manhua.178.com/]<xxxx> | URL')
    results = parser.parse_args()
    u = results.URL[0]
    if len(u) < 5 or u[:4] != u'http':
        u = 'http://manhua.178.com/' + u
    if results.l :
        loadDir(u)
    if results.i:
        loadInfo(u)
    if results.d :
        downImg(results.URL[0])
if __name__ == '__main__':
    main()