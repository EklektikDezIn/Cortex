using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Cortex
{
    class Camera
    {
        public Vector3 Position;
        public Vector3 Orientation;
        public float Speed;
        public float Sensitivity;
        public float Zoom;


        public Camera(float xPos, float yPos, float zPos, float xAng, float yAng, float zAng, float zoom, float MoveSpeed, float MouseSpeed)
        {//CREATE NEW CAMERA
            Position = new Vector3(xPos, yPos, zPos);
            Orientation = new Vector3(xAng, yAng, zAng);
            Speed = MoveSpeed;
            Sensitivity = MouseSpeed;
            Zoom = zoom;
        }

        public float GetZoom()
        {
            return Zoom;
        }

        public float GetXPos()
        {
            return Position.X;
        }

        public float GetYPos()
        {
            return Position.Y;
        }

        public float GetZPos()
        {
            return Position.Z;
        }

        public float GetXAng()
        {
            return Orientation.X;
        }

        public float GetYAng()
        {
            return Orientation.Y;
        }

        public float GetZAng()
        {
            return Orientation.Z;
        }

        public Matrix4 GetViewMatrix()
        {//Returns target for camera view
            OpenTK.Vector3 lookat = new OpenTK.Vector3();

            lookat.X = (float)(Math.Sin((float)Orientation.X) * Math.Cos((float)Orientation.Y));
            lookat.Y = (float)Math.Sin((float)Orientation.Y);
            lookat.Z = (float)(Math.Cos((float)Orientation.X) * Math.Cos((float)Orientation.Y));

            return Matrix4.LookAt(Position, Position + lookat, OpenTK.Vector3.UnitY);
        }

        public void Move(float x, float y, float z)
        {//Adjusts camera position
            OpenTK.Vector3 offset = new OpenTK.Vector3();

            OpenTK.Vector3 forward = new OpenTK.Vector3((float)Math.Sin((float)Orientation.X), 0, (float)Math.Cos((float)Orientation.X));
            OpenTK.Vector3 right = new OpenTK.Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = OpenTK.Vector3.Multiply(offset, Speed);

            Position += offset;
        }

        public void AddRotation(float x, float y)
        {//Rotates Camera
            x = x * Sensitivity;
            y = y * Sensitivity;

            Orientation.X = (Orientation.X + x) % ((float)Math.PI * 2.0f);
            Orientation.Y = Math.Max(Math.Min(Orientation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
