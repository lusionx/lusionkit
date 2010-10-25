using System;
using System.Collections.Generic;
using System.Linq;

namespace Perpor.BLL
{
    public class WidgetData
    {
        private const string _per = "widget ";

        private string _color;

        public string Color
        {
            get
            {
                if (_color.StartsWith(_per))
                {
                    return _color;
                }
                else
                {
                    return _per + _color;
                }
            }
            set { _color = value; }
        }



        public string ControlName { get; set; }
    }


}
