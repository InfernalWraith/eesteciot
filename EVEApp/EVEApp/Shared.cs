using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEApp
{
    public static class Shared
    {
        public static char HUMIDITY = '1';
        public static char TEMP = '2';
        public static char LIGHT = '3';


        public static float[] ConvertByteArrayToFloat(byte[] bytes)
        {
            if (bytes == null)
            {
                return new float[3] { 0.0f, 0.0f, 0.0f };
            }

            if (bytes.Length % 4 != 0)
                throw new ArgumentException
                      ("bytes does not represent a sequence of floats");

            return Enumerable.Range(0, bytes.Length / 4)
                             .Select(i => BitConverter.ToSingle(bytes, i * 4))
                             .ToArray();
        }
    }
}
