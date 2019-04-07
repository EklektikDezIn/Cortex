using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;   // For BitmMapData type
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using ShadowEngine;

namespace Cortex
{
    class Draw
    {

        public static bool loaded = false; //Checks if OpenGL Window is active

        public static void setLoaded(bool inpt)
        {
            loaded = inpt;
        }
        public static void Pyramid(int it, float x, float y, float z, float size, float rotAngle, OpenTK.Vector3 rotAxis, Texture tRef, Scheme Tint, Transparency transparancy)
        {//Creates Pyramid Object
            //Adjusts object position for centering
            x += size / 2;
            x *= -1;
            y += size / 2;
            z += size / 2;

            rotAngle *= it;//sets rotation angle

            GL.PushMatrix();
            GL.Translate(x, y, z);//Moves object

            GL.Rotate(-rotAngle, rotAxis.X, rotAxis.Y, rotAxis.Z);//Rotates object
            //Bottom
            GL.BindTexture(TextureTarget.Texture2D, tRef.getBottom()); //Get texture
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getBottom().R, Tint.getBottom().G, Tint.getBottom().B, transparancy.getBottom()); //Set color and transparancy
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, size / 2);//Create Plane
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, -size / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(size / 2, -size / 2, -size / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(size / 2, -size / 2, size / 2);
            GL.End();

            //Front
            GL.BindTexture(TextureTarget.Texture2D, tRef.getFront());
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(Tint.getFront().R, Tint.getFront().G, Tint.getFront().B, transparancy.getFront());
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, size / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(size / 2, -size / 2, size / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(0, size / 2, 0);
            GL.End();

            //Back
            GL.BindTexture(TextureTarget.Texture2D, tRef.getBack());
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(Tint.getBack().R, Tint.getBack().G, Tint.getBack().B, transparancy.getBack());
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(size / 2, -size / 2, -size / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, -size / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(0, size / 2, 0);
            GL.End();

            //Left
            GL.BindTexture(TextureTarget.Texture2D, tRef.getLeft());
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(Tint.getLeft().R, Tint.getLeft().G, Tint.getLeft().B, transparancy.getLeft());
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, -size / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-size / 2, -size / 2, size / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(0, size / 2, 0);
            GL.End();

            //Right
            GL.BindTexture(TextureTarget.Texture2D, tRef.getRight());
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(Tint.getRight().R, Tint.getRight().G, Tint.getRight().B, transparancy.getRight());
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(size / 2, -size / 2, size / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(size / 2, -size / 2, -size / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(0, size / 2, 0);
            GL.End();






            GL.PopMatrix();
        }

        public static void Cube(int it, Entity ent)
        {
            Scroll data = ent.data;
            //Adjusts object position for centering
            double x = ent.Pos.X + data.UserSees[1] / -2;
            double y = ent.Pos.X + data.UserSees[1] / 2;
            double z = ent.Pos.X + data.UserSees[1] / 2;

            double rotA = (data.UserSees[0] * it) % 360;//sets rotation angle

            GL.PushMatrix();
            GL.Translate(ent.Pos);//Moves object
            GL.Rotate(rotA, data.RotAxis.X, data.RotAxis.Y, data.RotAxis.Z);//Rotates object

            //Top
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getTop()); //Get texture
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getTop().R, Main.Colors[(int)data.UserSees[3]].getTop().G, Main.Colors[(int)data.UserSees[3]].getTop().B, data.UserSees[2]);//Set color and transparancy
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);//Create Plane
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.End();

            //Bottom
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getBottom());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getBottom().R, Main.Colors[(int)data.UserSees[3]].getBottom().G, Main.Colors[(int)data.UserSees[3]].getBottom().B, data.UserSees[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.End();

            //Front
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getFront());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getFront().R, Main.Colors[(int)data.UserSees[3]].getFront().G, Main.Colors[(int)data.UserSees[3]].getFront().B, data.UserSees[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.End();

            //Back
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getBack());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getBack().R, Main.Colors[(int)data.UserSees[3]].getBack().G, Main.Colors[(int)data.UserSees[3]].getBack().B, data.UserSees[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.End();

            //Left
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getLeft());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getLeft().R, Main.Colors[(int)data.UserSees[3]].getLeft().G, Main.Colors[(int)data.UserSees[3]].getLeft().B, data.UserSees[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.End();

            //Right
            GL.BindTexture(TextureTarget.Texture2D, data.Textr.getRight());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Main.Colors[(int)data.UserSees[3]].getRight().R, Main.Colors[(int)data.UserSees[3]].getRight().G, Main.Colors[(int)data.UserSees[3]].getRight().B, data.UserSees[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(data.UserSees[1] / 2, -data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, -data.UserSees[1] / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(data.UserSees[1] / 2, data.UserSees[1] / 2, data.UserSees[1] / 2);
            GL.End();


            GL.PopMatrix();
        }

        public static void DrawRect(int it, float x, float y, float z, float sizex, float sizey, float sizez, float rotAngle, OpenTK.Vector3 rotAxis, Texture tRef, Scheme Tint, float[] transparancy)
        {//Creates Rectangular Prism Object
            //Adjusts object position for centering
            x += sizex / 2;
            x *= -1;
            y += sizey / 2;
            z += sizez / 2;

            rotAngle *= it;//sets rotation angle

            GL.PushMatrix();
            GL.Translate(x, y, z);//Moves object
            GL.Rotate(-rotAngle, rotAxis.X, rotAxis.Y, rotAxis.Z);//Rotates object

            //Top
            GL.BindTexture(TextureTarget.Texture2D, tRef.getTop()); //Get texture
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getTop().R, Tint.getTop().G, Tint.getTop().B, transparancy[0]);//Set color and transparancy
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-sizex / 2, sizey / 2, sizez / 2);//Create Plane
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(sizex / 2, sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(sizex / 2, sizey / 2, -sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-sizex / 2, sizey / 2, -sizez / 2);
            GL.End();

            //Bottom
            GL.BindTexture(TextureTarget.Texture2D, tRef.getBottom());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getBottom().R, Tint.getBottom().G, Tint.getBottom().B, transparancy[1]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(sizex / 2, -sizey / 2, sizez / 2);
            GL.End();

            //Front
            GL.BindTexture(TextureTarget.Texture2D, tRef.getFront());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getFront().R, Tint.getFront().G, Tint.getFront().B, transparancy[2]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(sizex / 2, -sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(sizex / 2, sizey / 2, sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-sizex / 2, sizey / 2, sizez / 2);
            GL.End();

            //Back
            GL.BindTexture(TextureTarget.Texture2D, tRef.getBack());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getBack().R, Tint.getBack().G, Tint.getBack().B, transparancy[3]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-sizex / 2, sizey / 2, -sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(sizex / 2, sizey / 2, -sizez / 2);
            GL.End();

            //Left
            GL.BindTexture(TextureTarget.Texture2D, tRef.getLeft());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getLeft().R, Tint.getLeft().G, Tint.getLeft().B, transparancy[4]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-sizex / 2, -sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-sizex / 2, sizey / 2, sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-sizex / 2, sizey / 2, -sizez / 2);
            GL.End();

            //Right
            GL.BindTexture(TextureTarget.Texture2D, tRef.getRight());
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Tint.getRight().R, Tint.getRight().G, Tint.getRight().B, transparancy[5]);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(sizex / 2, -sizey / 2, sizez / 2);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(sizex / 2, -sizey / 2, -sizez / 2);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(sizex / 2, sizey / 2, -sizez / 2);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(sizex / 2, sizey / 2, sizez / 2);
            GL.End();

            GL.PopMatrix();
        }

        public static void BoxScene(int it, OpenTK.Vector3 coor1, OpenTK.Vector3 coor2, int scheme)
        {//Draws Frame and Floor for a specified area
            float rotSpeed = 0; //sets the angle of Rotation
            OpenTK.Vector3 rotAxis = Main.rotAxis;
            float[] TP = new float[6] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
            Texture texture = new Texture("Wood");
            Scheme Basic = Main.Colors[scheme];
            OpenTK.Vector3 distance = coor1.distanceTo(coor2);
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



            //Base
            DrawRect(it,
              -(coor1.X - .6f * xcount), (coor1.Y - .6f * ycount), (coor1.Z - .6f * zcount),
                -distance.X - xcount * 1.2f, 0.1f, 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
                -(coor1.X - .6f * xcount), (coor1.Y - .6f * ycount), (coor1.Z - .6f * zcount),
                0.1f, 0.1f, distance.Z + zcount * 1.2f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
               -(coor1.X + distance.X + .6f * xcount), (coor1.Y - .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
                distance.X + xcount * 1.2f, 0.1f, 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
                 -(coor1.X + distance.X + .6f * xcount), (coor1.Y - .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
                0.1f, 0.1f, -distance.Z - zcount * 1.2f,
                rotSpeed, rotAxis,
                texture, Basic, TP);

            //Walls
            DrawRect(it,
                -(coor1.X - .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z - .6f * zcount),
                0.1f, -(distance.Y + ycount * 1.2f), 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
               -(coor1.X + distance.X + .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
                0.1f, -(distance.Y + ycount * 1.2f), 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
               -(coor1.X - .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
               0.1f, -(distance.Y + ycount * 1.2f), 0.1f,
               rotSpeed, rotAxis,
               texture, Basic, TP);
            DrawRect(it,
               -(coor1.X + distance.X + .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z - .6f * zcount),
                0.1f, -(distance.Y + ycount * 1.2f), 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            //Top
            DrawRect(it,
              -(coor1.X - .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z - .6f * zcount),
                -distance.X - xcount * 1.2f, 0.1f, 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
                -(coor1.X - .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z - .6f * zcount),
                0.1f, 0.1f, distance.Z + zcount * 1.2f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
               -(coor1.X + distance.X + .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
                distance.X + xcount * 1.2f, 0.1f, 0.1f,
                rotSpeed, rotAxis,
                texture, Basic, TP);
            DrawRect(it,
                 -(coor1.X + distance.X + .6f * xcount), (coor1.Y + distance.Y + .6f * ycount), (coor1.Z + distance.Z + .6f * zcount),
                0.1f, 0.1f, -distance.Z - zcount * 1.2f,
                rotSpeed, rotAxis,
                texture, Basic, TP);

            //BasePlane
            //DrawRect(it, 0, 0, 0, coor.X, 0, coor.Z, rotSpeed, rotAxis, floor, Basic, TP);

        }
        public static void Render()
        {//Renders the scene
            // Clear the screen
            if (!loaded) { return; }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Initialise the model view matrix
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            // Draw the scene
            Main.DrawScene();

            GL.Disable(EnableCap.Blend);
            // Display the new frame


        }
        public static void Load()
        {//Loads OpenGL Properties
            // Setup OpenGL capabilities
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            // Setup background color
            GL.ClearColor(Color.Black);
            Main.loadVar();
        }
        public static void UpdateImage(int w, int h)
        {//Updates the current scene image

            if (!loaded) { return; }

            float aspect = 1;




            // Calculate aspect ratio, checking for divide by zero
            if (h > 0)
            {
                aspect = (float)w / (float)h;
            }

            // Initialise the projection view matrix
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Setup a perspective view

            float FOVradians = MathHelper.DegreesToRadians(Main.cam.GetZoom());
            Matrix4 perspective = Main.cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(FOVradians, aspect, 1.0f, 4000.0f);
            GL.MultMatrix(ref perspective);

            // Set the viewport to the whole window
            GL.Viewport(0, 0, w, h);


        }

    }

}
