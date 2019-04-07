using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortex
{
    class Variables
    {
        public static Boolean[] booleans = {false //Checks if OpenGL Window is active
                                           };

        public static void setboolean(int index, Boolean inpt)
        {
            booleans[index] = inpt;
        }
        public static bool getboolean(int index)
        {
            return booleans[index];
        }

       
    }
}
