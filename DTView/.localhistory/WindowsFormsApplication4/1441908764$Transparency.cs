using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen
{
    class Transparency
    {
        float[] SWrap = new float[6]; //List of transparency values

        public Transparency (float alpha, float beta, float gamma, float delta, float epsilon, float zeta)
        {//CREATE NEW TRANSPARENCY
            SWrap[0] = alpha;
            SWrap[1] = beta;
            SWrap[2] = gamma;
            SWrap[3] = delta;
            SWrap[4] = epsilon;
            SWrap[5] = zeta;
        }
        public Transparency(float alpha)
        {//CREATE NEW TRANSPARENCY
            SWrap[0] = alpha;
            SWrap[1] = alpha;
            SWrap[2] = alpha;
            SWrap[3] = alpha;
            SWrap[4] = alpha;
            SWrap[5] = alpha;
        }
        public float getTop()
        {//RETURN TOP VALUE
            return SWrap[0];
        }

        public float getBottom()
        {//RETURN BOTTOM VALUE
            return SWrap[1];
        }

        public float getFront()
        {//RETURN FRONT VALUE
            return SWrap[2];
        }

        public float getBack()
        {//RETURN BACK VALUE
            return SWrap[3];
        }

        public float getLeft()
        {//RETURN LEFT VALUE
            return SWrap[4];
        }

        public float getRight()
        {//RETURN RIGHT VALUE
            return SWrap[5];
        }
        public override string ToString()
        {
            return "("+SWrap[0] + "," + SWrap[1] + "," + SWrap[2] + "," + SWrap[3] + "," + SWrap[4] + "," + SWrap[5]+")";
        }
    }
}
