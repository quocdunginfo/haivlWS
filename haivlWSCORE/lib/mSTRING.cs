using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haivlWSCORE
{
    public static class mSTRING
    {
        public static string fromHTML(String input = "")
        {
            string tmp = System.Net.WebUtility.HtmlDecode(input);
            tmp = tmp.Replace("\r\n", "");
            tmp = tmp.Replace("\n\r", "");
            return tmp;
        }
    }
}
