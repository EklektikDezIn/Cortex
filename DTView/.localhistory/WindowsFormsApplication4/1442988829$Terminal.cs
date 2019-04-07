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
            Vector4 alpha = new Vector4();
            Vector3 beta = new Vector3();
            switch (inpt[0])
            {
                case 'H': //help
                    if (data.Length < 3)
                    {
                        otpt.Add("****HELP MENU****");
                        otpt.Add("h - Help menu");
                        otpt.Add("a - Add...");
                        otpt.Add("b - Symbols...");
                    }
                    else
                    {
                        switch (inpt[2])
                        {
                            case 'A':
                                otpt.Add("****ADD MENU****");
                                otpt.Add("b - Block |a b SYM (COOR)");
                                otpt.Add("a - Area  |a a SYM (COOR) (COOR)");
                                otpt.Add("a - Area  |a a SYM (COOR) [VEC]");
                                break;
                            case 'B':
                                otpt.Add("****SYM MENU****");
                                foreach (Scroll blocks in Main.Archives)
                                {
                                    otpt.Add(blocks.Symbol + " - " + blocks.Name);
                                }
                                break;
                            default:
                                otpt.Add("Command \"" + data + "\" is not recognized");
                                break;
                        }
                    }
                    break;
                case 'A': //add
                    switch (inpt[2])
                    {
                        case 'B':
                            alpha = getCoor(6, inpt);
                            Main.FEmpty.setObject(alpha.Xyz, inpt[4]);
                            Main.Refresh();
                            otpt.Add(Main.Archives.getScrollC(inpt[4]).Name + " block added at" + alpha.Xyz.ToStringS());
                            break;
                        case 'A':
                            alpha = getCoor(6, inpt);
                            beta = getCoor(2 + (int)alpha.W, inpt).Xyz;
                            if (inpt[2 + (int)alpha.W] == '(')
                            {
                                Main.FEmpty.addArea(alpha.Xyz, beta, inpt[4]);
                            }
                            else if (inpt[2 + (int)alpha.W] == '[')
                            {
                                Main.FEmpty.addArea(alpha.Xyz, alpha.Xyz + beta, inpt[4]);
                            }
                            Main.FEmpty.addArea(alpha.Xyz, beta, inpt[4]);
                            Main.Refresh();
                            otpt.Add("Plane of " + Main.Archives.getScrollC(inpt[4]).Name + " blocks added from" + alpha.Xyz.ToStringS() + " to "beta.ToStringS());
                            break;
                        default:
                            otpt.Add("Command \"" + data + "\" is not recognized");
                            break;
                    }
                    break;
                default:
                    otpt.Add("Command \"" + data + "\" is not recognized");
                    break;
            }
            return otpt;

        }
        public static Vector4 getCoor(int start, char[] data)
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
            return new Vector4(ex, why, zee, start);
        }
    }
}