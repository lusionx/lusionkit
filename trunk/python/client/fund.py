# -*- coding: utf-8 -*-
#!/usr/bin/python

import urllib2, urllib
import BeautifulSoup as bs


url = 'http://fund.eastmoney.com/fund.html'
def dd():
    f = urllib2.urlopen(url)

    soup = bs.BeautifulSoup(f.read())

    i = 0
    f = open('C:/a.txt','w')
    for tr in soup.findAll('tr',bgcolor="#F5FFFF"):
        if i == 0:
            for td in soup.findAll('td'):
                #sname = tds[3].contents[0]
                f.write(td)
                f.write('\n')
        i+=1
    print i
    f.close()
    

