using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
namespace GameEngine.Tools
{
    public class ToolsFile
    {

        public static void CreateFile(string path, List<string> infos)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            _saveFile(fs, infos);
        }

        public static void _saveFile(FileStream fs, List<string> infos)
        {
            StreamWriter sw = new StreamWriter(fs);

            int length = infos.Count;
            for (int i = 0; i < length; i++)
            {
                sw.WriteLine(infos[i]);
            }
            sw.Close();
            sw.Dispose();

            sw.Close();
            sw.Dispose();
            sw = null;
        }

        public static List<string> readConfigFile(TextAsset asset)
        {
            List<string> infos = new List<string>();
            ByteReader reader = new ByteReader(asset);
            while(reader.canRead)
            {
                string line = reader.ReadLine();
                if (line == null) break;
                infos.Add(line);
            }
            return infos;
        }

    }

}
