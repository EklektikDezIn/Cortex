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
            char[] inpt = data.ToUpper().ToCharArray();
           List<String> otpt = new List<String>();
           Vector3 alpha = new Vector3();
           switch(inpt[0]){
               case 'h': //help
                   otpt.Add("h - Help menu");
                   otpt.Add("");
                   break;
               case 'A': //add
               switch (inpt[3])
               {
                   case 'B':
                       switch (inpt[5])
                       {
                           case 'S':
                               alpha = getCoor(7,inpt);
                               Main.FEmpty.setObject(alpha, inpt[5]);
                               otpt.Add("Stone block added at" + alpha.ToStringS());
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