# -*- coding: utf-8 -*-
#!/usr/bin/python

files = [
        u'D:\91huayi\健康档案\BusinessLayer\BusinessLayer.csproj',
        u'D:\91huayi\健康档案\DataModule\DataModule.csproj',
        u'D:\91huayi\健康档案\HandlerLayer\HandlerLayer.csproj',
        u'D:\91huayi\健康档案\Substructure\Substructure.csproj',
        u'D:\91huayi\健康档案\Web\Web.csproj',
        u'D:\91huayi\健康档案\RemoteTraining\RemoteTraining.csproj',
        u'D:\91huayi\健康档案\ClassWebServesTest\ClassWebServesTest.csproj'
        ]
i=0
to = 32
for path in files:
    fr=open(path,'r')
    data=''
    for line in fr.readlines():
        if line.find('Microsoft.Practices.EnterpriseLibrary.Data.dll') > -1:
            i+=1
            if to == 64:
                data+='<HintPath>..\Microsoft.Practices.EnterpriseLibrary.Data.dll</HintPath>\n'
            elif to == 32:
                data+='<HintPath>..\References\Microsoft.Practices.EnterpriseLibrary.Data.dll</HintPath>\n'
        else:
            data+=line
    fr.close()
    fw=open(path,'w')
    fw.write(data)
    fw.close()
    
path = u'D:\91huayi\健康档案\DataModule\DataModule.csproj'
fr=open(path,'r')
data=''
for line in fr.readlines():
    if line.find('Oracle.DataAccess.dll') > -1:
        i+=1
        if to==64:
            data+='<HintPath>..\Oracle.DataAccess.dll</HintPath>\n'
        elif to==32:
            data+='<HintPath>..\References\Oracle.DataAccess.dll</HintPath>\n'
    else:
        data+=line
fr.close()
fw=open(path,'w')
fw.write(data)
fw.close()
print i
