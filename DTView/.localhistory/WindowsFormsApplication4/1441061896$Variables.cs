using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen
{
    class Variables
    {
        public static Boolean[] booleans = {true,false}; //Output list
        private static bool loaded = false; //Checks if OpenGL Window is active

        public static void setLoaded(bool inpt)
        {
            loaded = inpt;
        }
        public static bool getLoaded()
        {
            return loaded;
        }

       
    }
}
