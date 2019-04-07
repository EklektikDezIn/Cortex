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
                            case 'S':
                                otpt.Add("****SET MENU****");
                                otpt.Add("b - Block |a b SYM (COOR)");
                                otpt.Add("a - Area  |a a SYM (COOR) (COOR)");
                                otpt.Add("a - Area  |a a SYM (COOR) [VEC]");
                                break;
                            case 'A':
                                otpt.Add("****ADD MENU****");
                                otpt.Add("a - To Area  |a a SYM (COOR) (COOR)");
                                otpt.Add("a - To Area  |a a SYM (COOR) [VEC]");
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
                        case 'A'://Area
                            alpha = getCoor(6, inpt);

                            if (inpt[2 + (int)alpha.W] == '(')
                            {
                                beta = getCoor(2 + (int)alpha.W, inpt).Xyz;
                            }
                            else if (inpt[2 + (int)alpha.W] == '[')
                            {
                                beta = getCoor(2 + (int)alpha.W, inpt).Xyz;
                            }

                            Main.FEmpty.addToArea(alpha.Xyz, beta, inpt[4]);
                            Main.Refresh();
                            otpt.Add("Area of " + Main.Archives.getScrollC(inpt[4]).Name + " blocks set from" + alpha.Xyz.ToStringS() + " to " + beta.ToStringS());
                            break;
                        default:
                            otpt.Add("Command \"" + data + "\" is not recognized");
                            break;
                    }
                    break;







                case 'S': //add
                    switch (inpt[2])
                    {
                        case 'A'://Area
                            alpha = getCoor(6, inpt);

                            if (inpt[2 + (int)alpha.W] == '(')
                            {
                                beta = getCoor(2 + (int)alpha.W, inpt).Xyz;
                            }
                            else if (inpt[2 + (int)alpha.W] == '[')
                            {
                                beta = getCoor(2 + (int)alpha.W, inpt).Xyz;
                            }

                            Main.FEmpty.addArea(alpha.Xyz, beta, inpt[4]);
                            Main.Refresh();
                            otpt.Add("Area of " + Main.Archives.getScrollC(inpt[4]).Name + " blocks set from" + alpha.Xyz.ToStringS() + " to " + beta.ToStringS());
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