# -*- coding: utf-8 -*-
#!/usr/bin/python

"""通过google feed api取得rss"""

import rssService as rss
import urllib, httplib2
import simplejson as json
from extPy import *

def load(url):
    h = httplib2.Http('.cache')
    data = dict(q=url,v='1.0')
    url = 'https://ajax.googleapis.com/ajax/services/feed/load?'
    url += urllib.urlencode(data)
    resp, content = h.request(url, "GET")
    results = json.loads(content)
    feed = adDict(results)
    print feed.responseData.feed.entries[1].link.text()

def main(arg):
    load(arg)
if __name__ == '__main__':
    urls = []
    urls.append('http://www.cnblogs.com/rss')
    main(urls[0])

