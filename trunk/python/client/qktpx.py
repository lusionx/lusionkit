# -*- coding: utf-8 -*-
#!/usr/bin/python

import kit

def query(keys):
    keys =[ a for a in keys.split(' ') if a != '']
    if len(keys) == 0:
        return None
    sql = "select rowid,title,link from btktpx where "
    q = []
    for a in keys:
        q.append(' title like ? ')
    sql = sql + 'and'.join(q)
    print sql
    warp = lambda e:'%'+e+'%'
    rows = kit.exec_rows(sql,tuple(map(warp,keys)))
    sql = ''
    for a in rows:
        print '%s-%s-%s' % (a[0],a[1].encode('GBK'),a[2].encode('GBK'))
    return None

def main2():
    query(u'澄空 rmvb')


def main():
    while(True):
        print u'输入查询关键字'
        keys = raw_input()
        if(keys=='quit'):
            break
        else:
            query(keys)
    raw_input()

if __name__ == '__main__':
    main()
