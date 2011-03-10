# -*- coding: utf-8 -*-
#!/usr/bin/python

import BeautifulSoup, httplib2, urllib

service = '{{service}}'

{% for md in methods %}
def {{md.name}}({{md.pars}}):
    url = service+'{{md.name}}'
    body = dict( {{md.body}} )
    headers = {'Content-type': 'application/x-www-form-urlencoded'}
    http = httplib2.Http()
    response, content = http.request(url, 'POST', headers=headers, body=urllib.urlencode(body))
    return BeautifulSoup.BeautifulSoup(content)
{% endfor %}

if __name__ == '__main__':
    pass
