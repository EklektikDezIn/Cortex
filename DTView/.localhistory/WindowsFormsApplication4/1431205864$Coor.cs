using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NextGen
{
    public static class Coor
    {//EXTENDS OPENTK.VECTOR3 OBJECT
        public static Vector3 setXYZ(this Vector3 vec, int x, int y, int z)
        {//SET X,Y AND Z DIMESIONS
            vec.X = x;
            vec.Y = y;
            vec.Z = z;
            return vec;
        }
        public static Vector3 setXYZ(this Vector3 vec, string xyz)
        {//SET X,Y AND Z DIMESIONS
            xyz = xyz.Substring(xyz.IndexOf('(') + 1, xyz.IndexOf(')'));
            vec.X = Int32.Parse(xyz.Substring(0, xyz.IndexOf(',')));
            xyz = xyz.Substring(xyz.IndexOf(',') + 2);
            vec.Y = Int32.Parse(xyz.Substring(0, xyz.IndexOf(',')));
            xyz = xyz.Substring(xyz.IndexOf(',') + 2);
            vec.Z = Int32.Parse(xyz.Substring(0, xyz.IndexOf(')')));
            return vec;
        }

        public static Vector3 offSet(this Vector3 vec, float x, float y, float z)
        {//GET A VECTOR3 THAT IS OFFSET FROM THE BASE + 
            return new Vector3(vec.X + x, vec.Y + y, vec.Z + z);
        }
        public static Vector3 offSet(this Vector3 vec, Vector3 tar)
        {//GET A VECTOR3 THAT IS OFFSET FROM THE BASE +
            return new Vector3(vec.X + tar.X, vec.Y + tar.Y, vec.Z + tar.Z);
        }
        public static Vector3 offSetX(this Vector3 vec, float x, float y, float z)
        {//GET A VECTOR3 THAT IS OFFSET FROM THE BASE *
            return new Vector3(vec.X * x, vec.Y * y, vec.Z * z);
        }
        public static Vector3 Lowest(Vector3 alpha, Vector3 beta)
        {
            return new Vector3(Math.Min(alpha.X, beta.X), Math.Min(alpha.Y, beta.Y), Math.Min(alpha.Z, beta.Z));
        }
        public static Vector3 offSetS(this Vector3 vec, float x, float y, float z)
        {//GET A VECTOR3 THAT IS OFFSET FROM THE BASE BY AN INTEGER REGARDLESS OF SIGN

            int sX;
            int sY;
            int sZ;
            if (vec.X / x == 0)
            {
                sX = 1;
            }
            else
            {
                sX = (int)(vec.X / Math.Abs(vec.X));
            }

            if (vec.Y == 0)
            {
                sY = 1;
            }
            else
            {
                sY = (int)(vec.Y / Math.Abs(vec.Y));
            }

            if (vec.Z == 0)
            {
                sZ = 1;
            }
            else
            {
                sZ = (int)(vec.Z / Math.Abs(vec.Z));
            }

            return new Vector3(vec.X + x * sX, vec.Y + y * sY, vec.Z + z * sZ);
        }

        public static Vector3 Round(this Vector3 vec)
        {//SWITCH Z AND Y COORDINATES
            vec.X = (float)Math.Floor(vec.X);
            vec.Y = (float)Math.Floor(vec.Y);
            vec.Z = (float)Math.Floor(vec.Z);
            return vec;
        }

        public static Vector3 ToPositive(this Vector3 vec)
        {//SWITCH Z AND Y COORDINATES
            vec.X = (float)Math.Abs(vec.X);
            vec.Y = (float)Math.Abs(vec.Y);
            vec.Z = (float)Math.Abs(vec.Z);
            return vec;
        }

        public static Vector3 distanceTo(this Vector3 vec, Vector3 target)
        {//RETURN DISTANCE TO ANOTHER COORDINATE
            return new Vector3(target.X - vec.X, target.Y - vec.Y, target.Z - vec.Z);
        }

        public static List<Vector2> orderCoor(this Vector3 vec)
        {
            List<int> order = new List<int>();
            List<Vector2> report = new List<Vector2>();
            order.Add((int)Math.Abs(vec.X));
            order.Add((int)Math.Abs(vec.Y));
            order.Add((int)Math.Abs(vec.Z));
            order.Sort();
            if (order[0] == (int)Math.Abs(vec.X))
            {
                report.Add(new Vector2(vec.X, 0));
                if (order[1] == (int)Math.Abs(vec.Y))
                {
                    report.Add(new Vector2(vec.Y, 1));
                    report.Add(new Vector2(vec.Z, 2));
                }
                else 
                {
                    report.Add(new Vector2(vec.Z, 2)); 
                    report.Add(new Vector2(vec.Y, 1));
                }
            }
            else if (order[0] == (int)Math.Abs(vec.Y))
            {
                report.Add(new Vector2(vec.Y,1));
                if (order[1] == (int)Math.Abs(vec.X))
                {
                    report.Add(new Vector2(vec.X, 0));
                    report.Add(new Vector2(vec.Z, 2));
                }
                else 
                {
                    report.Add(new Vector2(vec.Z, 2));
                    report.Add(new Vector2(vec.X, 0));
                }
            }
            else if (order[0] == (int)Math.Abs(vec.Z))
            {
                report.Add(new Vector2(vec.Z,2));
                if (order[1] == (int)Math.Abs(vec.Y))
                {
                    report.Add(new Vector2(vec.Y, 1));
                    report.Add(new Vector2(vec.X, 0));
                }
                else
                {
                    report.Add(new Vector2(vec.X, 0));
                    report.Add(new Vector2(vec.Y, 1));
                }
            }


            if (order[1] == (int)Math.Abs(vec.X))
            {
                report.Add(new Vector2(vec.X,0));
            }
            else if (order[1] == (int)Math.Abs(vec.Y))
            {
                report.Add(new Vector2(vec.Y,1));
            }
            else if (order[1] == (int)Math.Abs(vec.Z))
            {
                report.Add(new Vector2(vec.Z,2));
            }


            if (order[2] == (int)Math.Abs(vec.X))
            {
                report.Add(new Vector2(vec.X,0));
            }
            else if (order[2] == (int)Math.Abs(vec.Y))
            {
                report.Add(new Vector2(vec.Y,1));
            }
            else if (order[2] == (int)Math.Abs(vec.Z))
            {
                report.Add(new Vector2(vec.Z,2));
            }

            return report;
        }
        public static float sumD(this Vector3 vec)
        {//CONVERT TO STRING
            return vec.X + vec.Y + vec.Z;
        }

        public static String ToStringS(this Vector3 vec)
        {//CONVERT TO STRING
            return ("(" + vec.X + ", " + vec.Y + ", " + vec.Z + ")");
        }



        public static Vector3 switchZY(this Vector3 vec)
        {//SWITCH Z AND Y COORDINATES
            float temp = vec.Y;
            vec.Y = vec.Z;
            vec.Z = temp;
            return vec;
        }

        public static Vector3 switchXY(this Vector3 vec)
        {//SWITCH Z AND Y COORDINATES
            float temp = vec.X;
            vec.X = vec.Y;
            vec.Y = temp;
            return vec;
        }

        public static Vector3 switchXZ(this Vector3 vec)
        {//SWITCH Z AND Y COORDINATES
            float temp = vec.Z;
            vec.Z = vec.X;
            vec.X = temp;
            return vec;
        }

        //EXTENDS VECTOR2
        public static Vector2 offSet(this Vector2 vec, Vector2 tar)
        {//GET A VECTOR2 THAT IS OFFSET FROM THE BASE +
            return new Vector2(vec.X + tar.X, vec.Y + tar.Y);
        }
        public static Vector2 offSet(this Vector2 vec, float x, float y)
        {//GET A VECTOR2 THAT IS OFFSET FROM THE BASE +
            return new Vector2(vec.X + x, vec.Y + y);
        }
        public static Vector2 setX(this Vector2 vec, float x)
        {//GET A VECTOR2 THAT IS OFFSET FROM THE BASE +
            return new Vector2(x, vec.Y);
        }
        //EXTENDS RANDOM OBJECT
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {//CREATE A RANDOM DOUBLE BETWEEN TWO VALUES
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

}

