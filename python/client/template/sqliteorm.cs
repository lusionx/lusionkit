{% autoescape off %}using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;
using System.Data;

namespace {{namespace }}
{
{% for k,v in tables.items %}
    [Tabel(Name = "{{k}}")]
    public class {{k|capfirst}} : TableBase
    {   {% for c in v.columns %}
        private System.{{c.systype}} _{{c.name}};
        [Column(Name = "{{c.name}}", DbType = DbType.{{c.systype}}, Nullable = {{c.nullable|lower}}, IsPrimary = {{c.primary_key|lower}})]
        public System.{{c.systype}} {{c.name|capfirst}} 
        { 
            get 
            { 
                return _{{c.name}};
            } 
            set 
            { 
                _{{c.name}} = value;
            } 
        }
        {% endfor %}
    }
{% endfor %}
}{% endautoescape %}