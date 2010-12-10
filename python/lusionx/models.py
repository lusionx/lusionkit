#!/usr/bin/env python

from google.appengine.ext import db

""""
class Pet(db.Model):
    name = db.StringProperty(required=True)
    type = db.StringProperty(required=True, choices=set(["cat", "dog", "bird"]))
    birth = db.DateProperty()
    weight_in_pounds = db.IntegerProperty()
    spayed_or_neutered = db.BooleanProperty()
    owner = db.UserProperty(required=True)
"""

class Person(db.Model):
    name = db.StringProperty(default = '')
    age = db.IntegerProperty(default = 1)
    msg = db.TextProperty(default = '')
