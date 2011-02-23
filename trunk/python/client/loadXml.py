# -*- coding: utf-8 -*-
#!/usr/bin/python

from xml.dom.minidom import parse, parseString
import kit

f = open(u'D:\\admin\Desktop\doc\\allmodules.htm','r')
dom1 = parse(f)


def getText(node):
    nodelist = node.childNodes
    rc = []
    for node in nodelist:
        if node.nodeType == node.TEXT_NODE:
            rc.append(node.data)
    return kit.trim(''.join(rc))
    

for tr in dom1.getElementsByTagName('tr'):
    tds = tr.getElementsByTagName('td')
    print "update sys_module set URL_PIC_SMALL = '%s', URL_PIC_LARGE = '%s' where MOD_CAT_KEY = %s;" %\
        (getText(tds[3]),getText(tds[4]),getText(tds[5]))
