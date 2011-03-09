# -*- coding: utf-8 -*-
#!/usr/bin/python

"""生成 web服务访问代理
"""
import BeautifulSoup as bsp
import httplib2
import os

#endwith .asxm
service = 'http://124.205.94.73:8001/ServiceTest.asmx'

def methods():
    h = httplib2.Http('.cache')
    resp, content = h.request(service, "GET")
    soup = bsp.BeautifulSoup(content)
    lis = soup.findAll('li')
    #print soup.findAll('li')
    dir = os.path.split(service)[0]+'/'
    hrefs = map(lambda li:dir + li.a['href'],lis)
    return hrefs

def methodInfo(url):
    h = httplib2.Http('.cache')
    resp, content = h.request(url, "GET")
    soup = bsp.BeautifulSoup(content)
    form = soup.form
    action = form['action']
    name = os.path.split(action)[1]
    pars = [a['name'] for a in form.findAll('input',type='text')]
    return name, action, pars

def genClient(f,name,action,pars):
    f.write('def '+name+'('+','.join(pars)+'):\n')
    f.write("    url = service+'"+name+"'\n")
    f.write("    body = {"+','.join([ "'"+a+"':"+a for a in pars])+"}\n")
    f.write("    headers = {'Content-type': 'application/x-www-form-urlencoded'}\n")
    f.write('    http = httplib2.Http()\n')
    f.write("    response, content = http.request(url, 'POST', headers=headers, body=urllib.urlencode(body))\n")
    f.write("    return BeautifulSoup.BeautifulSoup(content)\n\n")


def main():
    f = open('client.py','w')
    f.write('# -*- coding: utf-8 -*-\n')
    f.write('#!/usr/bin/python\n\n')
    f.write('import BeautifulSoup, httplib2, urllib\n\n')
    f.write('service = "'+service+'/"\n\n')
    for a in methods():
        name, action, pars = methodInfo(a)
        genClient(f,name, action, pars)
    f.close()
if __name__ == '__main__':
    main()
