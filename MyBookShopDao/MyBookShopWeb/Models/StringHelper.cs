using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookShopWeb.Models
{
    public class StringHelper
    {
        public static string CutString(string str, int count)
        {
            if (str.Length <= count)
            {
                return str;
            }
            else
            {
                return str.Substring(0, count) + "...";
            }
        }
    }
}