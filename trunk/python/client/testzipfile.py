# -*- coding: utf-8 -*-
#!/usr/bin/python
import zipfile, os
#http://effbot.org/librarybook/zipfile.htm
print dir(zipfile)

file = zipfile.ZipFile("ckeditor_3.4.2.zip", "r")
print dir(file)

print os.path.split(__file__)
print os.path.splitdrive(__file__)
print os.path.splitext(__file__)
print os.path.splitunc(__file__)
# list filenames
#for name in file.namelist():
#    print name
#print '---------------------------'

# list file information
#for info in file.infolist():
#    print info.filename, info.date_time, info.file_size
#for name in file.namelist():
#    data = file.read(name)
#    print name, len(data), repr(data[:10])
