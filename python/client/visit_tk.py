# -*- coding: utf-8 -*-
#!/usr/bin/python
import os
import urllib2
import sqlite3
import socket
socket.setdefaulttimeout(10)

_connstr = os.path.join(os.path.dirname(__file__),'info.sqlite3')

def new_conn():
    return sqlite3.connect(_connstr)

def get_proxy():
    conn = new_conn()
    cursor = conn.cursor()
    cursor.execute('''
            SELECT rowid, address, port
            from proxy
            where enable=1
            --limit 0,10''')
    rows = cursor.fetchall()
    cursor.close()
    conn.close()
    return rows

def add_proxy(address='',port='80',area = ''):
    conn = new_conn()
    conn.execute('''
        insert into proxy
        (address, port, enable,area)
        values
        (?,?,?,?)''',(address,port,1,area))
    conn.commit()
    conn.close()

def test_proxy(address,port):
    #url = 'http://7y8.com/V/ip.asp'
    url = 'http://xhsh.tk/tk.txt'
    add = 'http://'+address+':'+str(port)
    proxy_support = urllib2.ProxyHandler({
        'http': add
        })
    opener = urllib2.build_opener(proxy_support, urllib2.HTTPHandler)
    urllib2.install_opener(opener)
    try:
        result = urllib2.urlopen(url)
        print add+'->'+url+'->'+result.read()
        return True
    except urllib2.URLError, e:
        return False

def disable_proxy(rowid):
    conn = new_conn()
    conn.execute('''
        update proxy
        set enable = 0,modify_datetime = datetime('now')
        where rowid = ''' + str(rowid))
    conn.commit()
    conn.close()

from xml.dom.minidom import parse

def save_proxy_list(filename = 'proxy.xml'):
    dirpath = os.path.dirname(__file__)
    dom = parse(os.path.join(dirpath,filename))
    for row in dom.getElementsByTagName('tr'):
        aa = []
        for col in row.getElementsByTagName('td'):
            aa.append(getText(col.childNodes))
        if len(aa) > 0:
            print aa
            add_proxy(aa[1],aa[2],aa[3])


def getText(nodelist):
    rc = []
    for node in nodelist:
        if node.nodeType == node.TEXT_NODE:
            rc.append(node.data)
    return ''.join(rc)

def test_all_proxy():
    for one in get_proxy():
        print one
        if test_proxy(one[1],one[2]):
            pass
        else:
            disable_proxy(one[0])

def main():
    #save_proxy_list()
    #test_all_proxy()
    raw_input('end\n')

if __name__ == '__main__':
    main()
