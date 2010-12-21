# -*- coding: utf-8 -*-
#!/usr/bin/python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import settings, sys
from admin import context
import models, kit
from google.appengine.ext.db import djangoforms




url_map = {}




class PostForm(djangoforms.ModelForm):
    class Meta:
        model = models.Post
        #exclude = ['modified']




class lst(webapp.RequestHandler):

    def get(self):
        html = u'文章管理'
        self.response.out.write(sys.path)


url_map['/admin/post/lst'] = lst




class edit(webapp.RequestHandler):

    def get(self,pid):
        pid = int(pid)
        entity = models.Post.get_by_id(pid)
        ctx = context.default
        ctx['title'] = 'Post Modify'
        ctx['form'] = PostForm(instance=entity).as_p()
        html = kit.render(ctx,'admin','postform.html')
        self.response.out.write(html)


    def post(self,pid):
        entity = models.Post.get_by_id(int(pid))
        form = PostForm(data=self.request.POST, instance=entity)
        if form.is_valid():
            entity = form.save(commit=False)
            entity.put()
            self.redirect('/admin/post/edit/'+pid)
        else:
            ctx = context.default
            ctx['title'] = 'Post Modify'
            ctx['form'] = form.as_p()
            html = kit.render(ctx,'admin','postform.html')
            self.response.out.write(html)


url_map['/admin/post/edit/(\d+)'] = edit




class new(webapp.RequestHandler):

    def get(self):
        ctx = context.default
        ctx['title'] = 'Post Modify'
        ctx['form'] = PostForm().as_p()
        html = kit.render(ctx,'admin','postform.html')
        self.response.out.write(html)

    def post(self):
        data = PostForm(data=self.request.POST)
        if data.is_valid():
            # Save the data, and redirect to the view page
            entity = data.save(commit=False)
            entity.put()
            pid = entity.key().id()
            self.redirect('/admin/post/edit/'+str(pid))
        else:
            ctx = context.default
            ctx['title'] = 'Post Modify'
            ctx['form'] = data.as_p()
            html = kit.render(ctx,'admin','postform.html')
            self.response.out.write(data)



url_map['/admin/post/new'] = new

url_map['/admin/post/.*'] = kit.error

def main():
    application = webapp.WSGIApplication([(k,v) for (k, v) in url_map.items()], debug=settings.debug)
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
