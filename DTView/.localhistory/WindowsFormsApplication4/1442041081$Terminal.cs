using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex
{
    static class Terminal
    {
        public static List<String> prompt(String data)
        {
            char[] inpt = data.ToCharArray();
           List<String> otpt = new List<String>();
           switch(inpt[0]){
               case 'h':
                   otpt.Add("h - Help menu");
                   otpt.Add("");
               break;
               case 'a':
                   if (intp)
                       break;
           }
               return otpt;
        
    }
}
}