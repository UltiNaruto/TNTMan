using System;
using System.Collections.Generic;
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
    }
}
