using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perpor.BLL
{
    public class ColorClasses
    {//'color-yellow', 'color-red', 'color-blue', 'color-white', 'color-orange', 'color-green']
        public const string Yellow = "color-yellow";
        public const string Red = "color-red";
        public const string Blue = "color-blue";
        public const string White = "color-white";
        public const string Orange = "color-orange";
        public const string Green = "color-green";
        private static readonly string[] AllColors = { Yellow, Red, Blue, White, Orange, Green };
        public static string Rand
        {
            get
            {
                var rand = new Random();
                return AllColors[rand.Next(6)];
            }
        }
    }
    
}
