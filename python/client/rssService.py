# -*- coding: utf-8 -*-
#!/usr/bin/python

from sqlalchemy import create_engine
from sqlalchemy.schema import Table, MetaData, Column, ForeignKey
from sqlalchemy.orm import mapper, Session, clear_mappers, relationship
from sqlalchemy.types import Integer, String
import httplib2
from pyquery import PyQuery as pq
from lxml import etree

class Channel(object):
    pass
    
class Item(object):
    pass

def initDB(create=False):
    """call this before tabels models operating"""
    db = create_engine('sqlite:///info.sqlite3')
    #db.echo = True
    metadata = MetaData(db)

    channels = Table('rss_channel', metadata,
        Column('id', Integer, primary_key=True),
        Column('title', String, nullable=False, default=''),
        Column('link', String, nullable=False, default=''),
        Column('description', String, nullable=False, default=''),
        Column('language', String, nullable=False, default=''),
        Column('lastBuildDate', String, nullable=False, default=''),
        Column('generator', String, nullable=False, default=''),
    )

    items = Table('rss_item', metadata,
        Column('id', Integer, primary_key=True),
        Column('channel_id', Integer, ForeignKey('rss_channel.id'), nullable=False),
        Column('link', String, nullable=False, default=''),
        Column('title', String, nullable=False, default=''),
        Column('description', String, nullable=False, default=''),
        Column('author', String, nullable=False, default=''),
        Column('comments', String, nullable=False, default=''),
        Column('guid', String, nullable=False, default=''),
        Column('pubDate', String, nullable=False, default=''),
    )
    if create:
        items.create()
        channels.create()
    
    clear_mappers()

    mapper(Channel, channels, properties={
        'items': relationship(Item)
    })
    mapper(Item, items, properties={
        'channel': relationship(Channel),
    })
    
    return Session()

def loadRss(url):
    """load a rss url,return Channel instence"""
    h = httplib2.Http('.cache')
    resp, content = h.request(url, "GET")
    return content
    

def genChannel(content):
    """generate a Channel instence from content """
    channel = pq(content).children('channel')
    chl = Channel()
    chl.title = channel.children('title').html()
    chl.link = channel.children('link').html()
    chl.description = channel.children('description').html()
    chl.language = channel.children('language').html()
    chl.lastBuildDate = channel.children('lastBuildDate').html()
    chl.generator = channel.children('generator').html()
    chl.items = genItems(channel.children('item'))
    return chl

    
def genItems(itemsNode):
    """generate item list"""
    items = []
    for one in itemsNode:
        item = Item()
        one = pq(one)
        item.title = one.children('title').html()
        item.link = one.children('link').html()
        item.description = one.children('description').html()
        item.author = one.children('author').html()
        item.comments = one.children('comments').html()
        item.enclosure = one.children('enclosure').html()
        item.guid = one.children('guid').html()
        item.pubDate = one.children('pubDate').html()
        item.category = one.children('category').html()
        items.append(item)
    return items

def update(sess,ch):
    ch_o = sess.query(Channel).filter(Channel.link == ch.link).first()
    i = 0
    if ch_o == None:
        sess.add(ch)
        i = len(ch.items)
        sess.commit()
    elif ch_o.lastBuildDate != ch.lastBuildDate:
        # add new items
        dblinks = sess.query(Item.link).filter(Item.channel_id == ch_o.id)
        dblinks = [a[0] for a in dblinks]
        addlinks = [ item for item in ch.items if item.link not in dblinks]
        i=0
        for a in addlinks:
            i+=1
            a.channel = ch_o
            sess.add(a)
        sess.commit()
    else:
        pass #do nothing
    return i

def main(url):
    ss = initDB()
    #url = 'http://bt.ktxp.com/rss-sort-1.xml'
    c = loadRss(url)
    chl = genChannel(c)
    if chl.link:
        print 'Add %s items' % (update(ss,chl),)

if __name__ == '__main__':
    url = 'http://www.cnblogs.com/rss'
    main(url)

