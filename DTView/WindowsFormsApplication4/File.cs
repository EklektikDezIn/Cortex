using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Cortex
{
    class File
    {
        public static List<String> Read(String file)
        {//READS A FILE
            List<String> Paragraph = new List<String>();
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(file);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //Add the line to the list
                    Paragraph.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //Close the file
                sr.Close();
            }
            catch (Exception e)
            {
                if (Main.Error)
                {
                    Console.WriteLine("Exception at readFile: " + e.Message);
                }
            }
            return Paragraph;
        }
        public static void Write(String file, List<String> inpt)
        {//WRITES TO A FILE
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter(file, true, Encoding.ASCII);

                //Write the text
                foreach (string i in inpt)
                {
                    sw.Write(i);
                }
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                if (Main.Error)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }

        public static void Write(String file, String inpt)
        {//WRITES TO A FILE
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter(file, true, Encoding.ASCII);

                //Write the text
                sw.Write(inpt);
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                if (Main.Error)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }

        public static void Clear(String file)
        {//CLEARS A FILE
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter(file, false, Encoding.ASCII);

                //Write the text
                sw.Write("");
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                if (Main.Error)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
    }
}
