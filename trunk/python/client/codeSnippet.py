# -*- coding: utf-8 -*-
#!/usr/bin/python

import kit

def load():
    '''从 codeSnippet.txt 读取 首行是标签,之后是内容'''
    f = open(u'codeSnippet.txt','r')
    i = 0
    labels = []
    text=''
    for line in f.readlines():
        if i==0:
            labels = [kit.trim(a) for a in line.split(',')]
        else:
            text += line
        i+=1

    return (text,labels)

def add(codetext,lables):
    '''增加 代码段'''
    sql = 'select max(id) + 1 from cd_snippet'
    newId = kit.exec_scalar(sql)
    sql = 'insert into cd_snippet (id,text) values (?,?)'
    kit.exec_non(sql,(newId, codetext))
    sql = 'insert into cd_label (snippet_id,name) values (?,?)'
    for a in lables:
        if len(a) > 0:
            kit.exec_non(sql,(newId,a))
    return None
    
def main():
    a,b = load()
    print a
    print b

if __name__ == '__main__':
    main()
