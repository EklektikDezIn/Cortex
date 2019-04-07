using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using ShadowEngine;
using System.Drawing;
namespace Cortex
{
    class Scheme
    {
        Color[] Palate = new Color[6];

        public Scheme(Color alpha, Color beta, Color gamma, Color delta, Color epsilon, Color zeta)
        {
            Palate[0] = alpha;
            Palate[1] = beta;
            Palate[2] = gamma;
            Palate[3] = delta;
            Palate[4] = epsilon;
            Palate[5] = zeta;
        }
        public Scheme(Color alpha)
        {
            Palate[0] = alpha;
            Palate[1] = alpha;
            Palate[2] = alpha;
            Palate[3] = alpha;
            Palate[4] = alpha;
            Palate[5] = alpha;
        }
        public Color getTop()
        {
            return Palate[0];
        }

        public Color getBottom()
        {
            return Palate[1];
        }

        public Color getFront()
        {
            return Palate[2];
        }

        public Color getBack()
        {
            return Palate[3];
        }

        public Color getLeft()
        {
            return Palate[4];
        }

        public Color getRight()
        {
            return Palate[5];
        }
        public override String ToString()
        {
            return "("+Palate[0].ToString()+","+Palate[1].ToString()+","+Palate[2].ToString()+","+Palate[3].ToString()+","+Palate[4].ToString()+","+Palate[5].ToString()+")";
        }
    }
}
