using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NextGen
{
    class Room
    {
        public Vector3 offset = new Vector3(0, 0, 0);//Offset Vector
        public string Name = "Map1";

        static Random Rand = new Random(); //Random Number Generator
        static double[,] height; //Corner Heights

        private char[, ,] Cube; //3D array of characters for Room

        Vector3 alpha = new Vector3(0, 0, 0); //Class Vector


        public Room(int x, int y, int z)
        {//CREATE NEW ROOM
            if (x > 0 && y > 0 && z > 0)
            {
                Cube = new char[x, y, z];
                height = new double[x, z];
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("Room: Nonexistent Room: " + x + ", " + y + ", " + z);
                }
                Cube = new char[0, 0, 0];

            }
        }

        public Room(Vector3 vec)
        {//CREATE NEW ROOM
            if (vec.X > 0 && vec.Y > 0 && vec.Z > 0)
            {
                Cube = new char[(int)vec.X, (int)vec.Y, (int)vec.Z];
                height = new double[(int)vec.X, (int)vec.Z];
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("Room: Nonexistent Room: " + vec.X + ", " + vec.Y + ", " + vec.Z);
                }
                Cube = new char[0, 0, 0];

            }
        }

        public Room(char[, ,] inpt)
        {//CREATE NEW ROOM
            Cube = inpt;
        }
        public Room changeSize(int x, int y, int z)
        {//ALLOWS USER TO ADJUST THE SIZE OF THE ROOM
            Room CS = new Room(x, y, z);
            CS.Name = Name;
            CS.setEmpty();
            for (int xi = 0; xi < getXDim(); xi++)
            {
                for (int yi = 0; yi < getYDim(); yi++)
                {
                    for (int zi = 0; zi < getZDim(); zi++)
                    {
                        alpha = alpha.setXYZ(xi, yi, zi);
                        CS.setObject(alpha, getObjectAt(alpha));
                    }
                }
            }
            return CS;
        }
        public int getXDim()
        {//GET X DIMENSION OF ROOM
            return Cube.GetLength(0);
        }

        public int getYDim()
        {//GET Y DIMENSION OF ROOM
            return Cube.GetLength(1);
        }

        public int getZDim()
        {//GET Z DIMENSION OF ROOM
            return Cube.GetLength(2);
        }

        public char[, ,] getRoom()
        {//RETURN ENTIRE ROOM
            return Cube;
        }

        public Room Clone()
        {//RETURNS A DUPLICATE OF THE ROOM
            Room clone = new Room(getRoomSize());

            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        clone.setObject(alpha, getObjectAt(alpha));
                    }
                }
            }
            return clone;
        }
        public Vector3 getRoomSize()
        {//RETURN ALL DIMENSIONS
            return new Vector3(getXDim(), getYDim(), getZDim());
        }

        public void addScene(Room obj, Vector3 target)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        setObject(alpha.offSet(target), obj.getObjectAt(alpha));
                    }
                }
            }
        }

        public void addToScene(Room obj, Vector3 target)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (getObjectAt(alpha.offSet(target)).Equals('#'))
                        {
                            setObject(alpha.offSet(target), obj.getObjectAt(alpha));
                        }
                    }
                }
            }
        }

        public void addSceneB(Room obj, Vector3 target, char ent)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (!obj.getObjectAt(alpha).Equals('#'))
                        {
                            setObject(alpha.offSet(target), ent);
                        }
                        else
                        {
                            setObject(alpha.offSet(target), '#');
                        }
                    }
                }
            }
        }

        public void addToSceneB(Room obj, Vector3 target, char ent)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (getObjectAt(target.offSet(alpha)).Equals('#') && !obj.getObjectAt(alpha).Equals('#'))
                        {
                            setObject(alpha.offSet(target), ent);
                        }
                    }
                }
            }
        }



        public void addSceneI(Room obj, Vector3 target, char ent)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (obj.getObjectAt(alpha).Equals('#'))
                        {
                            setObject(alpha.offSet(target), ent);
                        }
                    }
                }
            }
        }

        public void addToSceneI(Room obj, Vector3 target, char ent)
        {//ADD A SCENE ITEM TO THE MAP
            for (int y = 0; y < obj.getYDim(); y++)
            {
                for (int x = 0; x < obj.getXDim(); x++)
                {
                    for (int z = 0; z < obj.getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (obj.getObjectAt(alpha).Equals('#') && getObjectAt(target.offSet(alpha)).Equals('#'))
                        {
                            setObject(alpha.offSet(target), ent);
                        }
                    }
                }
            }
        }


        public char getObjectAt(Vector3 coor)
        {//GET THE OBJECT AT A CERTAIN POSITION
            if (Contains(coor))
            {
                return Cube[(int)coor.X, (int)coor.Y, (int)coor.Z];
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("getObjectAt:  " + coor.ToString() + " does not exist.");
                }
                return '#';
            }
        }

        public Room getObjectsInRadius(Vector3 coor, int Rad)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 target = new Vector3(0, 0, 0);
            Vector3 obtar = new Vector3(0, 0, 0);
            Room area = new Room(2 * Rad + 1, 2 * Rad + 1, 2 * Rad + 1);
            area.offset = coor.offSet(-Rad, -Rad, -Rad);
            area.setEmpty();
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) <= Rad)
                        {
                            target = target.setXYZ(xdim + Rad, ydim + Rad, zdim + Rad);
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(target) && Contains(obtar))
                            {
                                char temp = getObjectAt(obtar);
                                area.setObject(target, getObjectAt(obtar));
                            }
                        }
                    }
                }
            }
            return area;
        }

        public Room Senses(Vector3 coor)
        {
            int SightRadius = 20;
            int minScent = 1;
            int minAudio = 1;

            Room World = new Room(getXDim(), getYDim(), getZDim());
            World.setEmpty();
            World.offset = offset;

            Room Eye = getObjectsInRadius(coor, SightRadius).Vision(alpha.setXYZ(SightRadius, SightRadius, SightRadius));
            Room Skin = getObjectsInRadius(coor, 1);
            Room Nose;
            Room Ear;
            if (getXDim() > Main.MaxScent && getXDim() > Main.MaxSound)
            {
                Nose = getObjectsInRadius(coor, Main.MaxScent).Scent(alpha.setXYZ(Main.MaxScent, Main.MaxScent, Main.MaxScent), minScent);
                Ear = getObjectsInRadius(coor, Main.MaxSound).Sound(alpha.setXYZ(Main.MaxSound, Main.MaxSound, Main.MaxSound), minAudio);
            }
            else
            {
                Nose = Scent(coor, minScent);
                Ear = Sound(coor, minAudio);
            }

            World.addToScene(Eye,Eye.offset);
            World.addToScene(Nose, Nose.offset);
            World.addToScene(Skin, Skin.offset);
            World.addToScene(Ear, Ear.offset);

            return World;
        }

        public Room Scent(Vector3 coor, int minScent)
        {
            Room area = new Room(getXDim(), getYDim(), getZDim());
            area.setEmpty();
            area.offset = offset;
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        Vector3 target = new Vector3(x, y, z);
                        char temp = getObjectAt(target);
                        if (Type(target, 'N') - target.distanceTo(coor).ToPositive().sumD() >= minScent)
                        {
                            area.setObject(target, getObjectAt(target));
                        }
                    }
                }
            }
            return area;
        }

        public Room Sound(Vector3 coor, int minAudio)
        {
            Room area = new Room(getXDim(), getYDim(), getZDim());
            area.setEmpty();
            area.offset = offset;
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        Vector3 target = new Vector3(x, y, z);
                        char temp = getObjectAt(target);
                        if (Type(target, 'A') - target.distanceTo(coor).ToPositive().sumD() >= minAudio)
                        {
                            area.setObject(target, getObjectAt(target));
                        }
                    }
                }
            }
            return area;
        }

        public Room Vision(Vector3 coor)
        {//REMOVES OBJECTS NOT VISIBLE DUE TO BLOCKAGE
            Room area = new Room(getXDim(), getYDim(), getZDim());
            area.offset = offset;
            area.setEmpty();
            foreach (Vector3 point in area.getEdges())
            {
                List<Vector3>[] Paths = pathFinder(coor, point);
                for (int i = 0; i < 6; i++)
                {
                    foreach (Vector3 target in Paths[i])
                    {
                        area.setObject(target, getObjectAt(target));
                        if ((Type(target, 'T') <= 3)&& !target.Equals(coor))
                        {
                            break;
                        }
                    }
                }
            }
            return area;
        }

        public List<Vector3> getEdges()
        {
            List<Vector3> Edges = new List<Vector3>();
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        if (x == 0 || x == getXDim() - 1 || y == 0 || y == getYDim() - 1 || z == 0 || z == getZDim() - 1)
                        {
                            Edges.Add(new Vector3(x, y, z));
                        }
                    }
                }
            }
            return Edges;
        }

        public List<Vector3>[] pathFinder(Vector3 alpha, Vector3 omega)
        {
            List<Vector3>[] Paths = new List<Vector3>[6];
            Paths[0] = new List<Vector3>();
            Paths[1] = new List<Vector3>();
            Paths[2] = new List<Vector3>();
            Paths[3] = new List<Vector3>();
            Paths[4] = new List<Vector3>();
            Paths[5] = new List<Vector3>();
            int xcount;
            int ycount;
            int zcount;
            for (int i = 0; i < 6; i++)
            {
                Paths[i].Add(alpha);
            }
            Vector3 home = alpha.Clone();
            Vector3 distance = home.distanceTo(omega);
            List<Vector2> data = distance.orderCoor();
            Vector3 jump = new Vector3((float)Math.Floor(distance.X / data[0].X), (float)Math.Floor(distance.Y / data[0].X), (float)Math.Floor(distance.Z / data[0].X));
            

            //"Don't do it wrong"-Prim
            Vector3 voids = new Vector3(0, 0, 0);
            while (!alpha.distanceTo(home.offSet(jump.offSetX(i,i,i))).Equals(voids))
            {
                if (distance.X == 0)
                {
                    xcount = 0;
                }
                else
                {
                    xcount = (int)(distance.X / Math.Abs(distance.X));
                }

                if (distance.Y == 0)
                {
                    ycount = 0;
                }
                else
                {
                    ycount = (int)(distance.Y / Math.Abs(distance.Y));
                }

                if (distance.Z == 0)
                {
                    zcount = 0;
                }
                else
                {
                    zcount = (int)(distance.Z / Math.Abs(distance.Z));
                }
                Paths[0].Add(alpha.offSet(xcount, 0, 0));
                Paths[0].Add(alpha.offSet(xcount, ycount, 0));
                Paths[0].Add(alpha.offSet(xcount, ycount, zcount));

                Paths[1].Add(alpha.offSet(xcount, 0, 0));
                Paths[1].Add(alpha.offSet(xcount, 0, zcount));
                Paths[1].Add(alpha.offSet(xcount, ycount, zcount));

                Paths[2].Add(alpha.offSet(0, ycount, 0));
                Paths[2].Add(alpha.offSet(xcount, ycount, 0));
                Paths[2].Add(alpha.offSet(xcount, ycount, zcount));

                Paths[3].Add(alpha.offSet(0, ycount, 0));
                Paths[3].Add(alpha.offSet(0, ycount, zcount));
                Paths[3].Add(alpha.offSet(xcount, ycount, zcount));

                Paths[4].Add(alpha.offSet(0, 0, zcount));
                Paths[4].Add(alpha.offSet(xcount, 0, zcount));
                Paths[4].Add(alpha.offSet(xcount, ycount, zcount));

                Paths[5].Add(alpha.offSet(0, 0, zcount));
                Paths[5].Add(alpha.offSet(0, ycount, zcount));
                Paths[5].Add(alpha.offSet(xcount, ycount, zcount));


                alpha = alpha.offSet(xcount, ycount, zcount);
            }
            return Paths;
        }

        public List<Entity> toDisplay()
        {//TRANSLATE 3D MATRIX OF CHAR TO LIST OF ENTITY
            List<Entity> temp = new List<Entity>();
            char car;
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        car = getObjectAt(alpha);
                        if (!car.Equals('#'))
                        {
                            temp.Add(charToEnt(car, alpha.offSet(offset)));
                        }
                    }
                }
            }
            return temp;
        }
        public static Entity charToEnt(char inpt, Vector3 target)
        {//CONVERTS CHAR TO ENTITY
            foreach (Archive data in Main.Archives)
            {
                if (data.Symbol.Equals(inpt)){
                    return new Entity(target, data.Clone());
                }
            }
            return null;
        }
        public List<Vector3> findObjects(char ent)
        {//RETURN POSITIONS OF ALL SPECIFIED ITEM TYPE
            List<Vector3> datapoints = new List<Vector3>();
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        obtar = obtar.setXYZ(x, y, z);
                        if (getObjectAt(obtar) == ent)
                        {
                            datapoints.Add(obtar);
                        }
                    }
                }
            }
            return datapoints;
        }

        public List<Vector3> findObjects(int ent)
        {//RETURN POSITIONS OF ALL SPECIFIED ITEM TYPE
            List<Vector3> datapoints = new List<Vector3>();
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        obtar = obtar.setXYZ(x, y, z);
                        if (Type(obtar, 'D') == ent)
                        {
                            datapoints.Add(obtar);
                        }
                    }
                }
            }
            return datapoints;
        }


        public Room roundAbout(Room place)
        {//CLONE A ROOM
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        obtar = obtar.setXYZ(x, y, z);
                        if (!getObjectAt(obtar).Equals('#'))
                        {
                            setObject(obtar, place.getObjectAt(obtar));
                        }
                    }
                }

            }
            return this;
        }

        public List<Vector3> line(Vector3 Home, Vector3 walls, int range)
        {//RETURNS AN AREA OF SPACE WITHIN A SPECIFIED RADIUS !!
            //if (Contains(Home))
            //{
            List<Vector3> points = new List<Vector3>();
            Vector3 distance = new Vector3(walls.X - Home.X, walls.Y - Home.Y, walls.Z - Home.Z);
            int xVal = (int)distance.X;
            int yVal = (int)distance.Y;
            int zVal = (int)distance.Z;

            int wallX = (int)walls.X;
            int wallY = (int)walls.Y;
            int wallZ = (int)walls.Z;

            int xmov;
            if (xVal != 0)
            {
                xmov = (xVal / Math.Abs(xVal));
            }
            else
            {
                xmov = 0;
            }
            int ymov;
            if (yVal != 0)
            {
                ymov = (yVal / Math.Abs(yVal));
            }
            else
            {
                ymov = 0;
            }
            int zmov;
            if (zVal != 0)
            {
                zmov = (zVal / Math.Abs(zVal));
            }
            else
            {
                zmov = 0;
            }

            int xLoc = wallX, yLoc = wallY, zLoc = wallZ;
            Vector3 target = new Vector3(wallX + xmov, wallY + ymov, wallZ + zmov);
            while (Math.Abs(distance.X) + Math.Abs(distance.Y) + Math.Abs(distance.Z) < range)
            {
                xVal = (int)distance.X;
                yVal = (int)distance.Y;
                zVal = (int)distance.Z;
                while (xVal != 0 || yVal != 0 || zVal != 0)
                {
                    if (xVal != 0)
                    {
                        xLoc += xmov;
                        xVal -= xmov;
                    }
                    if (yVal != 0)
                    {
                        yLoc += ymov;
                        yVal -= ymov;
                    }
                    if (zVal != 0)
                    {
                        zLoc += zmov;
                        zVal -= zmov;
                    }

                    target = target.setXYZ(xLoc, yLoc, zLoc);
                    points.Add(target);
                    distance = distance.setXYZ((int)(walls.X - Home.X), (int)(walls.Y - Home.Y), (int)(walls.Z - Home.Z));
                }
            }
            return points;
        }
        //}
        //else
        //{
        //    if (Main.Error)
        //    {
        //        System.Console.WriteLine("Vision: " + Home.ToString() + "does not exist");
        //    }
        //    return null;
        //}



        public static int GL(int alpha, int beta)
        {
            if (alpha < beta)
            {
                return alpha;
            }
            else
            {
                return beta;
            }
        }

        public Room getObjectsAt(Vector3 coor1, Vector3 coor2)
        {//RETURNS THE OBJECTS AT A SPECIFIED COORDINATES


            Vector3 distance = coor1.distanceTo(coor2);
            Vector3 target = new Vector3(0, 0, 0);
            Vector3 obtar = new Vector3(0, 0, 0);
            Room area = new Room(distance.ToPositive().offSetS(1, 1, 1));
            area.setEmpty();

            int xcount;
            int ycount;
            int zcount;
            if (distance.X == 0)
            {
                xcount = 1;
            }
            else
            {
                xcount = (int)(distance.X / Math.Abs(distance.X));
            }

            if (distance.Y == 0)
            {
                ycount = 1;
            }
            else
            {
                ycount = (int)(distance.Y / Math.Abs(distance.Y));
            }

            if (distance.Z == 0)
            {
                zcount = 1;
            }
            else
            {
                zcount = (int)(distance.Z / Math.Abs(distance.Z));
            }

            for (int x = (int)coor1.X; x != coor2.X + xcount; x += xcount)
            {
                for (int y = (int)coor1.Y; y != coor2.Y + ycount; y += ycount)
                {
                    for (int z = (int)coor1.Z; z != coor2.Z + zcount; z += zcount)
                    {
                        area.setObject(target.setXYZ(x - GL((int)coor1.X, (int)coor2.X), y - GL((int)coor1.Y, (int)coor2.Y), z - GL((int)coor1.Z, (int)coor2.Z)), getObjectAt(obtar.setXYZ(x, y, z)));
                    }
                }
            }
            return area;
        }


        public void setEmpty()
        {//FILLS THE ROOM WITH THE EMPTY CHARACTER
            Vector3 target = new Vector3(0, 0, 0);
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {

                        setObject(target.setXYZ(x, y, z), '#');
                    }
                }
            }
        }
        public void addLevel(char sym, int height)
        {//ADDS WATER FROM Y 0 TO Y HEIGHT
            for (int r = 0; r < getXDim(); r++)
            {
                for (int c = 0; c < getZDim(); c++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        alpha = alpha.setXYZ(r, h, c);
                        setObject(alpha, sym);
                    }
                }
            }
        }

        public void addLava(int height)
        {//ADDS LAVA FROM Y 0 TO Y HEIGHT
            for (int r = 0; r < getXDim(); r++)
            {
                for (int c = 0; c < getZDim(); c++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        alpha = alpha.setXYZ(r, h, c);
                        if (getObjectAt(alpha).Equals('#'))
                        {
                            setObject(alpha, '!');
                            if (getObjectAt(alpha.offSet(1, 0, 0)).Equals('S')) { setObject(alpha.offSet(1, 0, 0), 'D'); }
                            if (getObjectAt(alpha.offSet(-1, 0, 0)).Equals('S')) { setObject(alpha.offSet(-1, 0, 0), 'D'); }
                            if (getObjectAt(alpha.offSet(0, 1, 0)).Equals('S')) { setObject(alpha.offSet(0, 1, 0), 'D'); }
                            if (getObjectAt(alpha.offSet(0, -1, 0)).Equals('S')) { setObject(alpha.offSet(0, -1, 0), 'D'); }
                            if (getObjectAt(alpha.offSet(0, 0, 1)).Equals('S')) { setObject(alpha.offSet(0, 0, 1), 'D'); }
                            if (getObjectAt(alpha.offSet(0, 0, -1)).Equals('S')) { setObject(alpha.offSet(0, 0, -1), 'D'); }
                        }
                    }
                }
            }
        }


        public void setObject(Vector3 coor, char ent)
        {
            /*
             Adds an Object to the Map.  
             List How far left, high and deep from origin.
             Followed by the Object to be added
             (x,y,z,object)
             */
            if (Contains(coor))
            {
                Cube[(int)coor.X, (int)coor.Y, (int)coor.Z] = ent;
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("setObject: " + coor.ToString() + " does not exist");
                }
            }

        }

        public void moveObject(Vector3 from, Vector3 to)
        {
            char temp = getObjectAt(from);
            setObject(from, '#'); 
            setObject(to, temp);
        }

        public void addArea(Vector3 coor1, Vector3 coor2, char ent)
        {//SETS AN AREA OF THE MAP TO A PARTICULAR OBJECT
            if (Contains(coor1) && Contains(coor2))
            {
                Vector3 distance = coor1.distanceTo(coor2);
                Vector3 target = new Vector3(0, 0, 0);
                int xcount;
                int ycount;
                int zcount;
                if (distance.X == 0)
                {
                    xcount = 1;
                }
                else
                {
                    xcount = (int)(distance.X / Math.Abs(distance.X));
                }

                if (distance.Y == 0)
                {
                    ycount = 1;
                }
                else
                {
                    ycount = (int)(distance.Y / Math.Abs(distance.Y));
                }

                if (distance.Z == 0)
                {
                    zcount = 1;
                }
                else
                {
                    zcount = (int)(distance.Z / Math.Abs(distance.Z));
                }

                for (int x = (int)coor1.X; x != coor2.X + xcount; x += xcount)
                {
                    for (int y = (int)coor1.Y; y != coor2.Y + ycount; y += ycount)
                    {
                        for (int z = (int)coor1.Z; z != coor2.Z + zcount; z += zcount)
                        {
                            setObject(target.setXYZ(x, y, z), ent);
                        }
                    }
                }
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("addArea: Either " + coor1.ToString() + " or " + coor2.ToString() + "does not exist");
                }
            }
        }

        public void swapArea(Vector3 coor1, Vector3 coor2, char ent, char ent2)
        {//SETS AN AREA OF THE MAP TO A PARTICULAR OBJECT
            if (Contains(coor1) && Contains(coor2))
            {
                Vector3 distance = coor1.distanceTo(coor2);
                Vector3 target = new Vector3(0, 0, 0);
                int xcount;
                int ycount;
                int zcount;
                if (distance.X == 0)
                {
                    xcount = 1;
                }
                else
                {
                    xcount = (int)(distance.X / Math.Abs(distance.X));
                }

                if (distance.Y == 0)
                {
                    ycount = 1;
                }
                else
                {
                    ycount = (int)(distance.Y / Math.Abs(distance.Y));
                }

                if (distance.Z == 0)
                {
                    zcount = 1;
                }
                else
                {
                    zcount = (int)(distance.Z / Math.Abs(distance.Z));
                }

                for (int x = (int)coor1.X; x != coor2.X + xcount; x += xcount)
                {
                    for (int y = (int)coor1.Y; y != coor2.Y + ycount; y += ycount)
                    {
                        for (int z = (int)coor1.Z; z != coor2.Z + zcount; z += zcount)
                        {
                            target = target.setXYZ(x, y, z);
                            if (getObjectAt(target).Equals(ent))
                                setObject(target, ent2);
                        }
                    }
                }
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("addArea: Either " + coor1.ToString() + " or " + coor2.ToString() + "does not exist");
                }
            }
        }

        public void addToArea(Vector3 coor1, Vector3 coor2, char ent)
        {//SETS AN AREA OF THE MAP TO A PARTICULAR OBJECT
            if (Contains(coor1) && Contains(coor2))
            {
                Vector3 distance = coor1.distanceTo(coor2);
                Vector3 target = new Vector3(0, 0, 0);
                int xcount;
                int ycount;
                int zcount;
                if (distance.X == 0)
                {
                    xcount = 1;
                }
                else
                {
                    xcount = (int)(distance.X / Math.Abs(distance.X));
                }

                if (distance.Y == 0)
                {
                    ycount = 1;
                }
                else
                {
                    ycount = (int)(distance.Y / Math.Abs(distance.Y));
                }

                if (distance.Z == 0)
                {
                    zcount = 1;
                }
                else
                {
                    zcount = (int)(distance.Z / Math.Abs(distance.Z));
                }

                for (int x = (int)coor1.X; x != coor2.X + xcount; x += xcount)
                {
                    for (int y = (int)coor1.Y; y != coor2.Y + ycount; y += ycount)
                    {
                        for (int z = (int)coor1.Z; z != coor2.Z + zcount; z += zcount)
                        {
                            target = target.setXYZ(x, y, z);
                            if (getObjectAt(target).Equals('#'))
                            {
                                setObject(target, ent);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("addArea: Either " + coor1.ToString() + " or " + coor2.ToString() + "does not exist");
                }
            }
        }

        public void replaceArea(Vector3 coor1, Vector3 coor2, char ent)
        {//SETS AN AREA OF THE MAP TO A PARTICULAR OBJECT
            if (Contains(coor1) && Contains(coor2))
            {
                Vector3 distance = coor1.distanceTo(coor2);
                Vector3 target = new Vector3(0, 0, 0);
                int xcount;
                int ycount;
                int zcount;
                if (distance.X == 0)
                {
                    xcount = 1;
                }
                else
                {
                    xcount = (int)(distance.X / Math.Abs(distance.X));
                }

                if (distance.Y == 0)
                {
                    ycount = 1;
                }
                else
                {
                    ycount = (int)(distance.Y / Math.Abs(distance.Y));
                }

                if (distance.Z == 0)
                {
                    zcount = 1;
                }
                else
                {
                    zcount = (int)(distance.Z / Math.Abs(distance.Z));
                }

                for (int x = (int)coor1.X; x != coor2.X + xcount; x += xcount)
                {
                    for (int y = (int)coor1.Y; y != coor2.Y + ycount; y += ycount)
                    {
                        for (int z = (int)coor1.Z; z != coor2.Z + zcount; z += zcount)
                        {
                            target = target.setXYZ(x, y, z);
                            if (!getObjectAt(target).Equals('#'))
                            {
                                setObject(target, ent);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("addArea: Either " + coor1.ToString() + " or " + coor2.ToString() + "does not exist");
                }
            }
        }

        public void addRadius(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) <= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public void addToRadius(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) <= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar) && getObjectAt(obtar).Equals('#'))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public void replaceRadius(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) <= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar) && !getObjectAt(obtar).Equals('#'))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public void addRadiusI(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) >= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public void addToRadiusI(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) >= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar) && getObjectAt(obtar).Equals('#'))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public void replaceRadiusI(Vector3 coor, int Rad, char ent)
        {//GET THE OBJECTS WITHIN A CERTAIN RADIUS
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int ydim = -Rad; ydim <= Rad; ydim++)
            {
                for (int xdim = -Rad; xdim <= Rad; xdim++)
                {
                    for (int zdim = -Rad; zdim <= Rad; zdim++)
                    {
                        if (Math.Abs(xdim) + Math.Abs(zdim) + Math.Abs(ydim) >= Rad)
                        {
                            obtar = obtar.setXYZ((int)coor.X + xdim, (int)coor.Y + ydim, (int)coor.Z + zdim);
                            if (Contains(obtar) && !getObjectAt(obtar).Equals('#'))
                            {
                                setObject(obtar, ent);
                            }
                        }
                    }
                }
            }
        }

        public Room RotSI(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchXZ().offSetX(1, 1, -1).offSet(0, 0, dis - 1);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room RotS(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchXZ().offSetX(-1, 1, 1).offSet(dis - 1, 0, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room RotR(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchXY().offSetX(-1, 1, 1).offSet(dis - 1, 0, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room RotL(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchXY().offSetX(1, -1, 1).offSet(0, dis - 1, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room RotU(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchZY().offSetX(1, -1, 1).offSet(0, dis - 1, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room RotD(Vector3 coor1, int dis)
        {//ROTATES A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.switchZY().offSetX(1, 1, -1).offSet(0, 0, dis - 1);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }

        public Room InvertUD(Vector3 coor1, int dis)
        {//INVERTS A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.offSetX(1, 1, -1).offSet(0, 0, dis - 1);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }
        public Room InvertFB(Vector3 coor1, int dis)
        {//INVERTS A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.offSetX(1, -1, 1).offSet(0, dis - 1, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }
        public Room InvertLR(Vector3 coor1, int dis)
        {//INVERTS A SELECTED AREA 
            Room rot = new Room(dis, dis, dis);
            rot.offset = coor1;
            rot.offset = rot.offset.offSet(offset);
            rot.setEmpty();
            Vector3 temp = new Vector3(0, 0, 0);
            for (int x = 0; x < dis; x++)
            {
                for (int y = 0; y < dis; y++)
                {
                    for (int z = 0; z < dis; z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        temp = alpha.offSetX(-1, 1, 1).offSet(dis - 1, 0, 0);
                        rot.setObject(temp, getObjectAt(alpha.offSet(coor1)));
                    }
                }

            }
            return rot;
        }
        public void removeObject(Vector3 coor)
        {

            /*
             Removes an Object to the Map.  
             List How far left, high and deep from origin.
             */
            if (Contains(coor))
            {
                Cube[(int)coor.X, (int)coor.Y, (int)coor.Z] = '#';
            }
            else
            {
                if (Main.Error)
                {
                    System.Console.WriteLine("removeObject: " + coor.ToString() + "does not exist");
                }
            }

        }


        public void TerrainGen(int size, int[] seed)
        {//RANDOMLY GENREATES THE TERRAIN FOR THE MAP
            size = (int)Math.Pow(2, size);
            height[0, 0] = seed[0];
            height[0, height.GetLength(1) - 1] = seed[1];
            height[height.GetLength(0) - 1, 0] = seed[2];
            height[height.GetLength(0) - 1, height.GetLength(1) - 1] = seed[3];
            divide(0, 0, size, size);
            addLand();

        }
        private void addLand()
        {//ADDS THE LAND ACCORDING THE HEIGHT MATRIX
            for (int r = 0; r < height.GetLength(0); r++)
            {
                for (int c = 0; c < height.GetLength(1); c++)
                {
                    for (int h = 0; h <= height[r, c]; h++)
                    {
                        alpha = alpha.setXYZ(r, h, c);
                        setObject(alpha, ']');

                    }
                }
            }
        }
        public void loadBlanks()
        {//SETS THE HEIGHT MATRIX VALUES TO 0
            for (int r = 0; r < height.GetLength(0); r++)
            {
                for (int c = 0; c < height.GetLength(1); c++)
                {
                    height[r, c] = 0;
                }
            } if (Main.Error)
            {
                System.Console.WriteLine(height);
            }
        }
        public void divide(int xbeg, int zbeg, int xfin, int zfin)
        {//SETS THE EDGE CENTER AND MIDDLE VALUES OF CONSECUTIVELY SMALLER SQUARES TO GENERATE THE HEIGHT MATRIX
            int halfx = xbeg + ((xfin - xbeg) / 2);
            int halfz = zbeg + ((zfin - zbeg) / 2);
            double scale = (Main.roughness / 10) * (xfin - xbeg);
            height[xbeg, halfz] = (height[xbeg, zbeg] + height[xbeg, zfin]) / 2;
            height[xfin, halfz] = (height[xfin, zbeg] + height[xfin, zfin]) / 2;
            height[halfx, zbeg] = (height[xbeg, zbeg] + height[xfin, zbeg]) / 2;
            height[halfx, zfin] = (height[xbeg, zfin] + height[xfin, zfin]) / 2;
            height[halfx, halfz] = ((height[xbeg, zbeg] + height[xfin, zbeg] + height[xbeg, zfin] + height[xfin, zfin]) / 4) + (Rand.NextDouble(-1, 1) * scale);

            if (xfin - xbeg <= 3)
            {
                return;
            }
            else
            {
                divide(xbeg, zbeg, halfx, halfz);
                divide(halfx, halfz, xfin, zfin);
                divide(xbeg, halfz, halfx, zfin);
                divide(halfx, zbeg, xfin, halfz);
            }
        }


        public void printHeight()
        {//PRINTS OUT THE HEIGHT MATRIX
            for (int r = 0; r < height.GetLength(0); r++)
            {
                for (int c = 0; c < height.GetLength(1); c++)
                {
                    System.Console.Write(height[r, c] + ",");
                }
            }

        }
        public Room hollow()
        {//REMOVES BLOCKS THAT ARE SURROUNED BY THEIR KIND
            Room comp = new Room(getXDim(), getYDim(), getZDim());
            comp.offset = offset;
            comp.Name = Name;
            bool Norm = true;
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        for (int i = 0; i <= 9; i++)
                        {
                            if ((Type(alpha, 'D') == i) &&
                                (Type(alpha.offSet(1, 0, 0), 'D') == i) &&
                                (Type(alpha.offSet(-1, 0, 0), 'D') == i) &&
                                (Type(alpha.offSet(0, 1, 0), 'D') == i) &&
                                (Type(alpha.offSet(0, -1, 0), 'D') == i) &&
                                (Type(alpha.offSet(0, 0, 1), 'D') == i) &&
                                (Type(alpha.offSet(0, 0, -1), 'D') == i))
                            {
                                comp.setObject(alpha, '#');
                                Norm = false;
                            }
                        }
                        if (Norm)
                        {
                            comp.setObject(alpha, getObjectAt(alpha));
                        }
                        Norm = true;
                    }
                }
            }
            return comp;
        }

        public int Type(Vector3 coor, char sense)
        {//GROUPS THE BLOCKS BY TYPE
            char sym = getObjectAt(coor);

            foreach (Archive data in Main.Archives)
            {
                if (data.Symbol.Equals(sym))
                {
                    switch (sense)
                    {
                        case 'D'://Density
                            return data.CharSees[0];
                        case 'T'://Density
                            return data.CharSees[1];
                        case 'N'://Scent
                            return data.CharSees[2];
                        case 'A'://Audio
                            return data.CharSees[3];
                    }
                }
            }
            return -1;
        }

        public String symToText(Vector3 coor)
        {
            char sym = getObjectAt(coor);
            Archive comp = Main.Archives[1];

            foreach (Archive data in Main.Archives)
            {
                if (data.Symbol.Equals(sym))
                {
                    return data.Name;
                }
            }
            return "null";
        }
        public bool Contains(Vector3 coor)
        {//CHECKS IF A COORDINATE IS WITHIN A ROOM
            return (coor.X >= 0 && coor.Y >= 0 && coor.Z >= 0 && coor.X < getXDim() && coor.Y < getYDim() && coor.Z < getZDim());
        }
        public bool Contains(char ent)
        {//CHECKS IF A ROOM CONTAINS A SPECIFIC ENTITY 
            Vector3 obtar = new Vector3(0, 0, 0);
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        obtar = obtar.setXYZ(x, y, z);
                        if (getObjectAt(obtar) == ent)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void cave(int depth, Vector3 coor, double chance, char ent)
        {//CREATES A CAVE OF SIZE VARYING BY CHANCE INPUT

            alpha = alpha.setXYZ((int)coor.X, (int)coor.Y, (int)coor.Z);
            if (Contains(alpha)) { setObject(alpha, ent); }
            if (Contains(alpha.offSet(1, 0, 0)) && getObjectAt(alpha.offSet(1, 0, 0)).Equals(']') && getObjectAt(alpha.offSet(1, 0, 0)).Equals('#')) { setObject(alpha.offSet(1, 0, 0), ent); }
            if (Contains(alpha.offSet(-1, 0, 0)) && getObjectAt(alpha.offSet(-1, 0, 0)).Equals(']') && !getObjectAt(alpha.offSet(-1, 0, 0)).Equals('#')) { setObject(alpha.offSet(-1, 0, 0), ent); }
            if (Contains(alpha.offSet(0, 1, 0)) && getObjectAt(alpha.offSet(0, 1, 0)).Equals(']') && !getObjectAt(alpha.offSet(0, 1, 0)).Equals('#')) { setObject(alpha.offSet(0, 1, 0), ent); }
            if (Contains(alpha.offSet(0, -1, 0)) && getObjectAt(alpha.offSet(0, -1, 0)).Equals(']') && !getObjectAt(alpha.offSet(0, -1, 0)).Equals('#')) { setObject(alpha.offSet(0, -1, 0), ent); }
            if (Contains(alpha.offSet(0, 0, 1)) && getObjectAt(alpha.offSet(0, 0, 1)).Equals(']') && !getObjectAt(alpha.offSet(0, 0, 1)).Equals('#')) { setObject(alpha.offSet(0, 0, 1), ent); }
            if (Contains(alpha.offSet(0, 0, -1)) && getObjectAt(alpha.offSet(0, 0, -1)).Equals(']') && !getObjectAt(alpha.offSet(0, 0, -1)).Equals('#')) { setObject(alpha.offSet(0, 0, -1), ent); }
            double prob = Math.Pow(chance, 2);
            alpha = alpha.setXYZ((int)coor.X + 1, (int)coor.Y, (int)coor.Z);
            if
            (Contains(alpha) /*&& inpt[coor.X + 1, coor.Y, coor.Z].Equals(']'*/ && Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
            alpha = alpha.setXYZ((int)coor.X - 1, (int)coor.Y, (int)coor.Z);
            if
            (Contains(alpha) /*&& inpt[(int)coor.X - 1, (int)coor.Y, (int)coor.Z].Equals(']')*/&& Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
            alpha = alpha.setXYZ((int)coor.X, (int)coor.Y + 1, (int)coor.Z);
            if
            (Contains(alpha) /*&& inpt[(int)coor.X , (int)coor.Y+1, (int)coor.Z].Equals(']'*/ && Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
            alpha = alpha.setXYZ((int)coor.X, (int)coor.Y - 1, (int)coor.Z);
            if
            (Contains(alpha) /*&& inpt[(int)coor.X , (int)coor.Y-1, (int)coor.Z].Equals(']'*/ && Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
            alpha = alpha.setXYZ((int)coor.X, (int)coor.Y, (int)coor.Z + 1);
            if
            (Contains(alpha) /*&& inpt[(int)coor.X + 1, (int)coor.Y, (int)coor.Z+1].Equals(']'*/ && Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
            alpha = alpha.setXYZ((int)coor.X, (int)coor.Y, (int)coor.Z - 1);
            if
            (Contains(alpha) /*&& inpt[(int)coor.X , (int)coor.Y, (int)coor.Z-1].Equals(']'*/ && Rand.NextDouble() < prob)
            {
                cave(depth + 1, alpha, prob, ent);
            }
        }


        public Room modTerrain()
        {//ADJUSTS THE TERRAIN FOR MORE ASTHETIC APPEARANCE
            Room comp = new Room(getXDim(), getYDim(), getZDim());
            comp.Name = Name;
            //comp.setEmpty();
            //for (int x = 0; x < getXDim(); x++)
            //{
            //    for (int y = 0; y < getYDim(); y++)
            //    {
            //        for (int z = 0; z < getZDim(); z++)
            //        {
            //            alpha = alpha.setXYZ(x, y, z);
            //            comp.setObject(alpha.offSet(1, 1, 1), getObjectAt(alpha));
            //            setObject(alpha, getObjectAt(alpha));
            //        }
            //    }
            //}
            //for (int x = 0; x < comp.getXDim(); x++)
            //{
            //    for (int y = 0; y < comp.getYDim(); y++)
            //    {
            //        for (int z = 0; z < comp.getZDim(); z++)
            //        {
            //            alpha = alpha.setXYZ(x, y, z);
            //            if (comp.getObjectAt(alpha).Equals('#'))
            //            {
            //                comp.setObject(alpha, '`');
            //            }
            //        }
            //    }
            //}
            bool Norm = true;
            for (int x = 0; x < getXDim(); x++)
            {
                for (int y = 0; y < getYDim(); y++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        alpha = alpha.setXYZ(x, y, z);
                        if (y + 3 < getYDim())
                        {
                            if (Rand.NextDouble() > .5)
                            {
                                if
                               (getObjectAt(alpha.offSet(0, 2, 0)).Equals(']') && getObjectAt(alpha.offSet(0, 1, 0)).Equals(']'))
                                {
                                    comp.setObject(alpha, 'S');
                                    Norm = false;
                                }
                            }
                            else if
                              (getObjectAt(alpha.offSet(0, 3, 0)).Equals(']') && getObjectAt(alpha.offSet(0, 2, 0)).Equals(']') && getObjectAt(alpha.offSet(0, 1, 0)).Equals(']'))
                            {
                                comp.setObject(alpha, 'S');
                                Norm = false;
                            }

                        }


                        if
                          (getObjectAt(alpha).Equals(']') &&
                          getObjectAt(alpha.offSet(0, 1, 0)).Equals('#'))
                        {
                            comp.setObject(alpha, '[');
                            Norm = false;

                            if (Rand.NextDouble() < Main.TreeProb / 1000)
                            {
                                comp.addToScene(Main.convertToRoom(File.Read(Main.path[0] + "\\parts\\Tree.ngs")), alpha.offSet(-5, 0, -5));
                            }
                            else if (Rand.NextDouble() < Main.BushProb / 1000)
                            {
                                setObject(alpha.offSet(0, 1, 0), 'L');
                            }
                        }


                        if (alpha.Y > Main.corners[1] + 10 && alpha.Y < Main.corners[1] + 50 && Rand.NextDouble() < Main.CloudProb / 10000)
                        {

                            comp.cave(1, alpha.setXYZ(x, y, z), Rand.NextDouble(.5, .999), 'C');
                        }

                        if (alpha.Y > Main.corners[1] - 50 && alpha.Y < Main.corners[1] - 10 && Rand.NextDouble() < Main.CaveProb / 10000)
                        {
                            comp.cave(1, alpha.setXYZ(x, y, z), Rand.NextDouble(.5, .999), '#');
                        }


                        if
                         ((Type(alpha, 'D') == 9) &&
                         (getObjectsInRadius(alpha, Main.BeachWidth).Contains('~')))
                        {
                            comp.setObject(alpha, ':');
                            Norm = false;
                        }

                        if (Norm)
                        {
                            comp.setObject(alpha, getObjectAt(alpha));
                        }
                        Norm = true;
                    }
                }
            }
            return comp;
        }
        public override String ToString()
        {
            Vector3 obtar = new Vector3(0, 0, 0);
            String map = "";
            for (int y = 0; y < getYDim(); y++)
            {
                for (int x = 0; x < getXDim(); x++)
                {
                    for (int z = 0; z < getZDim(); z++)
                    {
                        map += getObjectAt(obtar.setXYZ(x, y, z));
                    }
                    map += "|";
                }
                map += " \n";
            }
            return map;
        }
    }
}



