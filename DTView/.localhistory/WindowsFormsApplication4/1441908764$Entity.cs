using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NextGen
{
    class Entity
    {
        public Vector3 Pos; //Position
        public Archive data;
        //public String Class; // Class
        //public char Type; //Symbol
        //public double Size; //Size
        //public double rotSpeed; //Rotation Speed
        //public Vector3 rotAxis; //Rotation Axis
        //public Scheme Tint; //Color Shading
        //public Texture Tex; //Texture
        //public Transparency Tran; //Transparency

        public Entity(Vector3 coor, Archive dat)
        {//CREATE NEW ENTITY
            
            Pos = coor;
            data = dat;
        }

        public Entity setPos(Vector3 coor)
        {//SET THE ENTITIES POSITION
            Pos = coor;
            return this;
        }
        public Entity Clone()
        {//DUPLICATE THE ENTITY
            return new Entity(Pos, data);

        }
        public override string ToString()
        {//OUTPUTS A STRING OF THE ENTITIES POSITION AND CHAR REPRESENTATION
            return "<" + Pos.ToStringS() + " " + data.ToString() + ">";
        }
    }

}

