using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TNTMan
{
    class Utils
    {
        internal static IntPtr StringToPtr(string str)
        {
            IntPtr ptr = IntPtr.Zero;
            byte[] chars = null;

            if (str.Length == 0) return ptr;

            ptr = new IntPtr();
            chars = System.Text.Encoding.ASCII.GetBytes(str + '\0');
            for (int i = 0; i < str.Length; i++)
                Marshal.Copy(chars, 0, ptr + i, chars.Length);

            return ptr;
        }

        internal static byte[] PtrToArray(IntPtr ptr, int size)
        {
            List<byte> liste = null;

            if (ptr == IntPtr.Zero) return null;

            liste = new List<byte>();
            for (int i = 0; i < size; i++)
                liste.Add(Marshal.ReadByte(ptr + i));

            return liste.ToArray();
        }

        internal static PointF ArrondiPositionBloc(PointF pos)
        {
            PointF diff = new PointF(pos.X - (int)pos.X, pos.Y - (int)pos.Y);
            if (pos.X < 0.0f)
            {
                if (diff.X > -0.24f)
                {
                    pos.X += 1.0f;
                }
                if (diff.X < -0.76f)
                {
                    pos.X -= 1.0f;
                }
            }
            if (pos.X > 0.0f)
            {
                if (diff.X < 0.24f)
                {
                    pos.X -= 1.0f;
                }
                if (diff.X > 0.76f)
                {
                    pos.X += 1.0f;
                }
            }
            if (pos.Y < 0.0f)
            {
                if (diff.Y > -0.24f)
                {
                    pos.Y += 1.0f;
                }
                if (diff.Y < -0.76f)
                {
                    pos.Y -= 1.0f;
                }
            }
            if (pos.Y > 0.0f)
            {
                if (diff.Y < 0.24f)
                {
                    pos.Y -= 1.0f;
                }
                if (diff.Y > 0.76f)
                {
                    pos.Y += 1.0f;
                }
            }
            pos.X = (int)pos.X;
            pos.Y = (int)pos.Y;
            return pos;
        }

        internal static PointF ArrondiPositionBloc(float x, float y)
        {
            return ArrondiPositionBloc(new PointF(x, y));
        }
    }
}
