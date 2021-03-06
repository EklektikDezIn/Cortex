﻿using OpenTK;
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
            switch (inpt[0])
            {
                case 'h': //help
                    if (data.Length < 3)
                    {
                        otpt.Add("****HELP MENU****");
                        otpt.Add("h - Help menu");
                        otpt.Add("a - Add...");
                        otpt.Add("b - Blocks...");
                    }
                    else
                    {
                        switch (inpt[2])
                        {
                            case 'A':
                                otpt.Add("****ADD MENU****");
                                otpt.Add("b - Block");
                                otpt.Add("p - Plane");
                                break;

                        }
                    }
                    break;
                case 'A': //add
                    switch (inpt[2])
                    {
                        case 'B':
                            alpha = getCoor(6, inpt);
                            Main.FEmpty.setObject(alpha, inpt[4]);
                            Main.Refresh();
                            otpt.Add(Main.Archives.getScrollC(inpt[4]) + " block added at" + alpha.ToStringS());
                            break;
                        case 'P':

                            break;
                    }
                    break;
            }
            return otpt;

        }
        public static Vector3 getCoor(int start, char[] data)
        {
            String inpt = "";
            start++;
            while (data[start] != ',')
            {
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
            return new Vector3(ex, why, zee);
        }
    }
}