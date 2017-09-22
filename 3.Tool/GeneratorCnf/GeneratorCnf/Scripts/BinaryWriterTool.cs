using System.IO;


namespace GeneratorCnf.Scripts
{
    public class BinaryWriterTool
    {
        public static void WriteInt32(BinaryWriter bw, int data)
        {
            bw.Write(data);
        }

        public static void WriteInts32(BinaryWriter bw, int[] data)
        {
            if (data == null)
            {
                bw.Write(0);
            }
            else
            {
                bw.Write(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    bw.Write(data[i]);
                }
            }

        }

        public static void WriteString(BinaryWriter bw, string data)
        {
            bw.Write(data);
        }

        public static void WriteStringS(BinaryWriter bw, string[] data)
        {
            if (data == null)
            {
                bw.Write(0);
            }
            else
            {
                bw.Write(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    bw.Write(data[i]);
                }
            }

        }

        public static void WriteBool(BinaryWriter bw, bool data)
        {
            bw.Write(data);
        }

        public static void WriteBools(BinaryWriter bw, bool[] data)
        {
            if (data == null)
            {
                bw.Write(0);
            }
            else
            {
                bw.Write(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    bw.Write(data[i]);
                }
            }

        }

    }
}
