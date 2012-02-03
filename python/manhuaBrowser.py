# coding: utf-8

from bottle import route, run, request, response, template, redirect
import os, zipfile

@route('/favicon.ico')
def favicon():
    redirect("http://bottlepy.org/docs/dev/_static/favicon.ico")


PATH = u'd:/lusionx/Desktop/tmppic'
DIRS = (u'Bleach',u'Naruto',u'极乐院女子高寮物语',u'天降之物')



@route('/')
@route('/index.html')
def index():
    return '<br/>'.join([ u'<a href="/%s/index.html" >%s</a>' % (a,a) for a in DIRS])

@route('/<dir>/index.html')
@route('/<dir>/')
def section(dir):
    dir = dir.decode('utf-8')
    htm = [  u'<a href="/%s/%s/index.html" >%s</a>' % (dir, a, a) for a in os.listdir(os.path.join(PATH,dir))]
    htm.insert(0, u'<a href="/" >首页</a>')
    return '<br/>'.join(htm)

@route('/<dir>/<file>/')
@route('/<dir>/<file>/index.html')
@route('/<dir>/<file>/index_<i:int>.html')
def browser(dir,file,i=1):
    tpl = """
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"></script>
<script>
    $(function(){
        $('select').change(function(){
            window.location.href=$(this.options[this.selectedIndex]).attr('url');
        });
        $('a.op-prev').attr('href',function(){
            var slt = $('select')[0];
            return $(slt.options[slt.selectedIndex]).prev().attr('url');
        })
        $('a.op-next').attr('href',function(){
            var slt = $('select')[0];
            return $(slt.options[slt.selectedIndex]).next().attr('url');
        })
        $(document).keydown(function (e) {
            if (e.which == 37) {
                window.location.href = $('a.op-prev').attr('href');
            }
            if (e.which == 39) {
                window.location.href = $('a.op-next').attr('href');
            }
        })
    });
</script>
<a href="/" >首页</a> <a href="/{{dir}}/index.html" >{{dir}}</a>
<a class="op-prev">prev</a>
<select>
%for a in xrange(0,len(files)):
    %if files[a] == curr:
<option selected = "selected" url="/{{dir}}/{{file}}/index_{{a+1}}.html" value="{{files[a]}}">{{a+1}}</option>
    %else :
<option url="/{{dir}}/{{file}}/index_{{a+1}}.html" value="{{files[a]}}">{{a+1}}</option>
    %end
%end
</select>
<a class="op-next">next</a>
<div><img src="/img/{{dir}}/{{file}}/{{curr}}"></div>
"""
    dir = dir.decode('utf-8')
    file = file.decode('utf-8')
    i = i-1
    zf = zipfile.ZipFile(os.path.join(PATH, dir, file),'r')
    #htm = [  u'<a href="/img/%s/%s/%s" >%s</a>' % (dir, file, a.decode('gb2312'), a.decode('gb2312')) for a in zf.namelist()]    
    #htm.insert(0, u'<a href="/%s/index.html" >%s</a>' % (dir,dir)) #章节
    #htm.insert(0, u'<a href="/" >首页</a>')
    files = [ a.decode('gb2312') for a in zf.namelist()]
    (lambda f, d : f.write(d+'\n'))(open('manhuaBrowser.log','a'), request.url)
    return template(tpl,dir = dir, files = files, file = file, curr = files[i])
    

@route('/img/<dir>/<file>/<name>')
def img(dir,file,name):
    dir = dir.decode('utf-8')
    file = file.decode('utf-8')
    name = name.decode('utf-8')
    zf = zipfile.ZipFile(os.path.join(PATH, dir, file), 'r')
    bytes = zf.read(name.encode('gb2312'))
    import mimetypes
    response.content_type = mimetypes.guess_type(name)[0]
    return bytes

run(host='localhost', port=8080)


