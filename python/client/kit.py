
import sqlite3, os

_connstr = os.path.join(os.path.dirname(__file__),'info.sqlite3')

def exec_rows(sql,par=()):
    conn = sqlite3.connect(_connstr)
    cur = conn.cursor()
    cur.execute(sql,par)
    rows = cur.fetchall()
    cur.close()
    conn.close()
    return rows

def exec_non(sql,par=()):
    conn = sqlite3.connect(_connstr)
    conn.execute(sql,par)
    conn.commit()
    conn.close()
    return None

def exec_top_one(sql,par=()):
    conn = sqlite3.connect(_connstr)
    cur = conn.cursor()
    cur.execute(sql,par)
    obj = cur.fetchone()
    cur.close()
    conn.close()
    return obj
