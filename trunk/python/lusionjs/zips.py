#coding: utf-8
#!/usr/bin/env python
#
# Copyright 2007 Google Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
import webapp2

import email.Utils
import logging
import mimetypes
import time
import zipfile

class MainHandler(webapp2.RequestHandler):
    zipfile_cache = {}
    def get(self,name,path):
        self.ServeFromZipFile(name + '.zip', path)

    zipfile_cache = {}
    def ServeFromZipFile(self, zipfilename, name):
        zipfile_object = self.zipfile_cache.get(zipfilename)
        if zipfile_object is None:
            try:
                zipfile_object = zipfile.ZipFile(zipfilename)
            except (IOError, RuntimeError, zipfile.BadZipfile), err:
                logging.error('Can\'t open zipfile %s: %s', zipfilename, err)
                zipfile_object = None
            self.zipfile_cache[zipfilename] = zipfile_object
        try:
            data = zipfile_object.read(name)
        except (KeyError, RuntimeError), err:
            try:
                data = zipfile_object.read(zipfilename[:-4] + '/'+ name)
            except (KeyError, RuntimeError), err:
                self.error(404)
                self.response.out.write('Not found')
                return
        content_type, encoding = mimetypes.guess_type(name)
        self.response.headers['Content-Type'] = content_type
        self.response.out.write(data)

app = webapp2.WSGIApplication([('/(.*)/zip/(.*)', MainHandler)],
                              debug=True)
