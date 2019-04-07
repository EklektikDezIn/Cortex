using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NextGen
{
    class Intelligence
    {
        static Random Rand = new Random(); //Random Number Generator
        Vector3 alpha = new Vector3(0, 0, 0);
        public Room Inpt;
        public Vector3 me;
        public int[] stat = new int[8];

        public Intelligence(Vector3 loc, int smell, int sight, int sound, int dist, int health, int energy, int hunger, int thirst)
        {
            me = loc;
            Inpt = new Room(0, 0, 0);
            stat[0] = smell;//min
            stat[1] = sight;//max
            stat[2] = sound;//min
            stat[3] = dist;//max
            stat[4] = health;//decreases
            stat[5] = energy;//decreases
            stat[6] = hunger;//increases
            stat[7] = thirst;//increases
        }

        public Intelligence(Vector3 loc)
        {
            me = loc;
            Inpt = new Room(0, 0, 0);
            stat[0] = Rand.Next(1, 2);
            stat[1] = Rand.Next(7, 12);
            stat[2] = Rand.Next(1, 3);
            stat[3] = Rand.Next(1, 3);
            stat[4] = Rand.Next(400, 500);//500
            stat[5] = Rand.Next(400, 500);//500
            stat[6] = Rand.Next(0, 30);//100
            stat[7] = Rand.Next(0, 20);//100
        }

        public void setMap(Room map)
        {
            Inpt = map;
        }

        public static Intelligence findIntel(Vector3 coor)
        {
            Intelligence Curr = Main.potato;
            foreach (Intelligence AI in Main.IA)
            {
                if (AI.me.Equals(coor))
                {
                    Curr = AI;
                }
            }
            return Curr;
        }

        public List<Vector3> location(Room options)
        {
            List<Vector3> Places = new List<Vector3>();
            for (int x = 0; x < options.getXDim(); x++)
            {
                for (int y = 0; y < options.getYDim(); y++)
                {
                    for (int z = 0; z < options.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z).offSet(options.offset);

                        char temp = Inpt.getObjectAt(alpha);
                        if (Inpt.Type(alpha, 'D') >= 8 && Inpt.Type(alpha.offSet(0, 1, 0), 'D') <= 1)
                        {
                            Places.Add(alpha.offSet(0, 1, 0));
                        }
                    }
                }
            }
            return Places;
        }

        public List<Vector4> paths(Vector3 coor)
        {
            int i = 0;
            List<Vector3> possible = new List<Vector3>();
            List<Vector4> data = new List<Vector4>();
            possible.Add(me);
            List<Vector3> temp = new List<Vector3>();
            while (possible.Count > 0)
            {
                foreach (Vector3 pos in possible)
                {
                    data.Add(new Vector4(pos.X, pos.Y, pos.Z, i));
                    temp.AddRange(oneStep(pos, data));
                }
                possible.Clear();
                temp = temp.Distinct().ToList<Vector3>();
                possible.AddRange(temp);
                temp.Clear();
                i++;
            }
            return data;
        }

        public List<Vector3> oneStep(Vector3 start, List<Vector4> beenThere)
        {
            List<Vector3> data = new List<Vector3>();
            List<Vector2> comp = new List<Vector2>();
            foreach (Vector4 temp in beenThere)
            {
                comp.Add(temp.Xz);
            }
            List<Vector3> Choices = location(Main.FEmpty);
            foreach (Vector3 pos in Choices)
            {
                if (start.X == pos.X && start.Z + 1 == pos.Z && pos.Y - start.Y <= 1 && !comp.Contains(start.Xz.offSet(0, 1))) { data.Add(pos); }
                if (start.X == pos.X && start.Z - 1 == pos.Z && pos.Y - start.Y <= 1 && !comp.Contains(start.Xz.offSet(0, -1))) { data.Add(pos); }
                if (start.X + 1 == pos.X && start.Z == pos.Z && pos.Y - start.Y <= 1 && !comp.Contains(start.Xz.offSet(1, 0))) { data.Add(pos); }
                if (start.X - 1 == pos.X && start.Z == pos.Z && pos.Y - start.Y <= 1 && !comp.Contains(start.Xz.offSet(-1, 0))) { data.Add(pos); }
            }
            return data;
        }

        public Vector3 Psyche()
        {
            Inpt = Main.FEmpty;//.Senses(me);
            paths(me);
            List<Vector3> Choices = location(Inpt.getObjectsInRadius(me, stat[3]));
            //Choices.Add(me);
            List<Vector3> thoughts = new List<Vector3>();
            int[] val = new int[Choices.Count];
            int j = 0;
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = 0;
            }
            foreach (Vector3 option in Choices)
            {
                for (int x = 0; x < Inpt.getXDim(); x++)
                {
                    for (int y = 0; y < Inpt.getYDim(); y++)
                    {
                        for (int z = 0; z < Inpt.getZDim(); z++)
                        {
                            alpha = alpha.setXYZ(x, y, z);
                            val[j] += Logos(Inpt.getObjectAt(alpha), alpha, option, stat);
                        }
                    }
                }
                j++;
            }
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] == val.Max())
                {
                    thoughts.Add(Choices[i]);
                }
            }
            return thoughts[Rand.Next(0, thoughts.Count)];
        }


        public int Logos(char Object, Vector3 alpha, Vector3 beta, int[] Stats)
        {
            int a, b, c;
            a = (int)beta.X - (int)alpha.X;
            b = (int)beta.Z - (int)alpha.Z;
            c = (int)beta.Y - (int)alpha.Y;
            double flat = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            double energy = flat + c * Math.Cos(Math.Atan(c / flat));
            //Archive data = Main.SearchArchives(Object);
            double Value = 0;

            //          Value += (500 - stat[4]) * data.stat[2];//health
            //        Value += stat[6] * data.stat[0];//food
            //      Value += stat[7] * data.stat[1];//water
            if (Value > 0)
            {

            }
            if (energy == 0)
            {
                energy = 1;
            }
            return (int)Math.Ceiling(Value / energy);
        }
    }
}
