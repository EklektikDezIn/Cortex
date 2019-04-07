using OpenTK;
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
               case 'h': //help
                   otpt.Add("h - Help menu");
                   otpt.Add("");
                   break;
               case 'a': //add
               switch (inpt[3])
               {
                   case 'b':
                       switch (inpt[5])
                       {
                           case 's':
                               Main.FEmpty.setObject(getCoor(7,inpt), inpt[5]);
                               break;
                       }
                       break;

               }
                   break;
           }
               return otpt;
        
    }
       public static Vector3 getCoor(int start, char[] data){
            String inpt = "";
            start++;
            while(data[start] != ','){
                inpt += data[start];
                start++;
            }
           int ex = Int32.Parse(inpt);
           start++;
            inpt = "";
           while (data[start] != ',')
           {
               inpt += data[start];
               start++;
           }
           int why = Int32.Parse(inpt);

            inpt = "";
            start++;
            while (data[start] != ')' && data[start] != ']' && data[start] != '}')
           {
               inpt += data[start];
               start++;
           }
           int zee = Int32.Parse(inpt);
            return new Vector3(ex,why,zee);
        }
}
}