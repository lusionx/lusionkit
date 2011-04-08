# -*- coding: utf-8 -*-
#!/usr/bin/python


class BaseObj(object):
    def tdict(self):
        """将实例转换为字典"""
        d = self.__dict__
        k = '_sa_instance_state'
        #剔除sqlalchemy给对象的状态字段
        if d.has_key(k):
            del d[k]
        return d
    def __init__(self,dic=None):
        """用一个字典初始化本实例,递归到底层
        主要是用来接收 由json串反序列化出来的字典
        """
        if dic == None:
            return
        for k,v in dic.items():
            exec('self.'+k+'=v')
            if type(v) == list:
                for i in xrange(len(v)):
                    if type(v[i]) == dict:
                        v[i] = BaseObj(v[i])


class User(BaseObj):
    pass

class Email(BaseObj):
    pass

class Apod(BaseObj):
    pass

class Channel(BaseObj):
    pass

class Item(BaseObj):
    pass
    
class Carton(BaseObj):
    pass
