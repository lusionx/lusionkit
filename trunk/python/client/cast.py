# -*- coding: utf-8 -*-
#!/usr/bin/python

def cfun(txt):
    result = []
    result.append(txt[6:8]+txt[4:6]+txt[2:4]+txt[0:2])
    result.append(txt[10:12]+txt[8:10])
    result.append(txt[14:16]+txt[12:14])
    result.append(txt[16:20])
    result.append(txt[20:])
    return result

def raw_guid(txt):
    txt = txt.lower()
    return '-'.join(cfun(txt))

def guid_raw(txt):
    txt = txt.replace('-','').upper()
    return ''.join(cfun(txt))


def main(arg):
    txt = 'E1F8BEDC831D2A4AAE4E2451A55A1AC4'
    print txt
    txt = raw_guid(txt)
    print txt
    txt = guid_raw(txt)
    print txt
if __name__ == '__main__':
    main('')
