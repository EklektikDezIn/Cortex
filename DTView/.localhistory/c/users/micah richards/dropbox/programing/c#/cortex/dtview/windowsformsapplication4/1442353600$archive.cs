using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Cortex
{
    class Archive{

        public List<Scroll> Library = new List<Scroll>();
        
        public void addScroll(String nam, String sym, String tex, String rot, String user, String car, String bene, String map){
            Library.Add(new Scroll(nam,sym,tex,rot,user,car,bene,map));
        }

        public Scroll findScrollN(String text){
            foreach (Scroll paper in Library){
                if (paper.Name.Equals(text){
                    return paper;
                }
            }
            return null;
        }

         public Scroll findScrollC(char text){
            foreach (Scroll paper in Library){
                if (paper.Symbol.Equals(text){
                    return paper;
                }
            }
            return null;
        }
         public Scroll getScroll(int i)
         {
             return Library[i];
         }

    private class Scroll
    {
        public string Name;
        public char Symbol;
        public Texture Textr;
        public Vector3 RotAxis;
        public float[] UserSees = new float[4];
        public int[] CharSees = new int[4];
        public int[] Benefits = new int[3];
        public char MapRender;
        public int Probability;

        public Scroll(String nam, String sym, String tex, String rot, String user, String car, String bene, String map)
        {
            Name = nam;
            Symbol = sym[0];
            if (tex.Contains("ALL"))
            {
                Textr = new Texture(tex.Substring(0, tex.IndexOf(" ")));
            }
            else
            {
                String[] text = new String[6];
                for (int i = 0; i < 6; i++)
                {
                    text[i] = tex.Substring(0, tex.IndexOf(" "));
                    tex = tex.Substring(tex.IndexOf(" ") + 1);
                }

                Textr = new Texture(text[0], text[1], text[2], text[3], text[4], text[5]);
            }


            float[] rots = new float[3];
            for (int i = 0; i < 3; i++)
            {
                rots[i] = Convert.ToSingle(rot.Substring(0, rot.IndexOf(" ")));
                rot = rot.Substring(rot.IndexOf(" ") + 1);
            }
            RotAxis = new Vector3(rots[0], rots[1], rots[2]);


            for (int i = 0; i < 4; i++)
            {
                UserSees[i] = Convert.ToSingle(user.Substring(0, user.IndexOf(" ")));
                user = user.Substring(user.IndexOf(" ") + 1);
            }
            UserSees[2] = (10 - UserSees[2]) / 10;

            for (int i = 0; i < 4; i++)
            {
                CharSees[i] = Convert.ToInt32(car.Substring(0, car.IndexOf(" ")));
                car = car.Substring(car.IndexOf(" ") + 1);
            }


            for (int i = 0; i < 3; i++)
            {
                Benefits[i] = Convert.ToInt32(bene.Substring(0, bene.IndexOf(" ")));
                bene = bene.Substring(bene.IndexOf(" ") + 1);
            }


            MapRender = map.Substring(0, map.IndexOf(" "))[0];
            map = map.Substring(map.IndexOf(" "));
            Probability = Convert.ToInt32(map);
        }

        public Scroll(string name, char symbol, Texture textr, Vector3 rotaxis, float[] user, int[] car, int[] bene, char map, int prob)
        {
            Name = name;
            Symbol = symbol;
            Textr = textr;
            UserSees = user;
            CharSees = car;
            Benefits = bene;
            MapRender = map;
            Probability = prob;
        }

        public Scroll Clone()
        {
            float[] temp = new float[] { UserSees[0], UserSees[1], UserSees[2], UserSees[3] }; 

            int[] temp2 = new int[] { CharSees[0], CharSees[1], CharSees[2], CharSees[3] };

            int[] temp3 = new int[] { Benefits[0], Benefits[1], Benefits[2] };

            return new Scroll(Name, Symbol, Textr, RotAxis, temp, temp2, temp3, MapRender, Probability);
        }

        public Scroll (){

        }
       
        public void setTint(int ti)
        {
            UserSees[3] = ti;
        }
    }
    }
}
