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
        public Vector3 last;
        public int[] stat = new int[8];

        public Intelligence(Vector3 loc, int smell, int sight, int sound, int dist, int health, int energy, int hunger, int thirst)
        {
            me = loc;
            last = loc;
            Inpt = new Room(Main.FEmpty.getRoomSize());
            Inpt.setEmpty();
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
            last = loc;
            Inpt = new Room(Main.FEmpty.getRoomSize());
            Inpt.setEmpty();
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

        public void updateInpt()
        {
            Room temp = Main.FEmpty.Senses(me);

            Inpt.addToScene(temp, temp.offset);
            foreach (Intelligence Char in Main.IA)
            {
                if (!Char.last.Equals(Char.me))
                {
                    Inpt.removeObject(Char.last);
                }
            }
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

        public List<Vector4> paths(Vector3 coor, List<Vector3> Targets)
        {
            int i = 0;
            List<Vector3> possible = new List<Vector3>();
            List<Vector4> data = new List<Vector4>();
            possible.Add(coor);
            List<Vector3> temp = new List<Vector3>();

            while (possible.Count > 0)
            {
                foreach (Vector3 pos in possible)
                {
                    data.Add(new Vector4(pos.X, pos.Y, pos.Z, i));
                    temp.AddRange(oneStep(pos, data, Targets));
                }
                possible.Clear();
                temp = temp.Distinct().ToList<Vector3>();
                possible.AddRange(temp);
                temp.Clear();
                i++;
            }
            return data;
        }

        public List<Vector3> oneStep(Vector3 start, List<Vector4> beenThere, List<Vector3> Targets)
        {
            List<Vector3> data = new List<Vector3>();
            List<Vector2> comp = new List<Vector2>();
            foreach (Vector4 temp in beenThere)
            {
                comp.Add(temp.Xz);
            }
            foreach (Vector3 pos in Targets)
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
            updateInpt();
            List<Vector3> Targets = location(Inpt);
            List<Vector3> Choices = location(Inpt.getObjectsInRadius(me, stat[3]));
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
                            val[j] += Logos(Inpt.getObjectAt(alpha), alpha, option);

                        }
                    }
                }
                val[j] += Logos(option, paths(option, Targets));
                j++;
            }
            List<int> temp = new List<int>();
            temp.AddRange(val);
            temp.Sort();
            temp.Reverse();
            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] == temp[0])
                {
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                    thoughts.Add(Choices[i]);
                }
                try
                {
                    if (val[i] == temp[1])
                    {
                        thoughts.Add(Choices[i]);
                        thoughts.Add(Choices[i]);
                        thoughts.Add(Choices[i]);
                    }
                }
                catch { }
                try
                {
                    if (val[i] == temp[2])
                    {
                        thoughts.Add(Choices[i]);
                    }
                }
                catch { }
            }
            return thoughts[Rand.Next(0, thoughts.Count)];
        }

        public int Logos(Vector3 home, List<Vector4> Paths)
        {
            double Value = 0;
            double energy = 0;
            double points;
            Archive data = Main.Archives[0];
            foreach (Vector4 path in Paths)
            {

                int y = (int)path.Y - (int)home.Y;
                energy = path.W + Math.Cos(Math.Atan(y / path.W));
                if (energy == 0 || energy.Equals(Double.NaN))
                {
                    energy = 1;
                }

                List<Vector3> contact = new List<Vector3>();
                contact.Add(path.Xyz.offSet(0, 0, 1));
                contact.Add(path.Xyz.offSet(0, 0, -1));
                contact.Add(path.Xyz.offSet(0, 1, 0));
                contact.Add(path.Xyz.offSet(0, -1, 0));
                contact.Add(path.Xyz.offSet(1, 0, 0));
                contact.Add(path.Xyz.offSet(-1, 0, 0));

                points = 0;
                contact.Remove(me);
                foreach (Vector3 place in contact)
                {
                    char car = Inpt.getObjectAt(place);
                    foreach (Archive archie in Main.Archives)
                    {
                        if (archie.Symbol.Equals(car))
                        {
                            data = archie.Clone();
                            break;
                        }
                    }

                    if (car.Equals('~') || car.Equals('L'))
                    {

                    }
                    points += (500 - stat[4]) * data.Benefits[2];//health
                    points += stat[6] * data.Benefits[0];//food
                    points += stat[7] * data.Benefits[1];//water


                }

                Value += Math.Ceiling(points / energy);
            }
            if (Value > 0)
            {

            }
            return (int)Value;
        }

        private int Logos(char car, Vector3 alpha, Vector3 beta)
        {
            int a, b, c;
            a = (int)beta.X - (int)alpha.X;
            b = (int)beta.Z - (int)alpha.Z;
            c = (int)beta.Y - (int)alpha.Y;
            double flat = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            double energy = flat + c * Math.Cos(Math.Atan(c / flat));
            Archive data = Main.Archives[0];
            foreach (Archive archie in Main.Archives)
            {
                if (archie.Symbol.Equals(car))
                {
                    data = archie.Clone();
                    break;
                }
            }
            double points = 0;

            points += (500 - stat[4]) * data.Benefits[2];//health
            points += stat[6] * data.Benefits[0];//food
            points += stat[7] * data.Benefits[1];//water
            if (points > 0)
            {

            }
            if (energy == 0)
            {
                energy = 1;
            }
            return (int)Math.Ceiling(points / energy);
        }
    }
}
