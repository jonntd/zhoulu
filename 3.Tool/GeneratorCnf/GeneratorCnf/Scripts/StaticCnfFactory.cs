using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCnf.Scripts
{
    public class StaticCnfFactory
    {
        public const string TAB = "\t";
        public static string Create(string[] infos)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.IO;");

            sb.AppendLine("public class StaticCnfLoader");
            sb.AppendLine("{");

            // LoadAllCsvFile 
            int length = infos.Length;
            sb.AppendLine(TAB + "public static void LoadAllCsvFile()");
            sb.AppendLine(TAB + "{");

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(TAB + TAB + string.Format("StaticCnf.Add(CsvLoader.LoadFile<{0}>(\"{0}\"));", infos[i]));
            }
            sb.AppendLine(TAB + "}");

            sb.AppendLine(string.Empty);

            // LoadAllCsvBinary function
            sb.AppendLine(TAB + "public static void LoadAllCsvBinary()");
            sb.AppendLine(TAB + "{");

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(TAB + TAB + string.Format("StaticCnf.Add(CsvLoader.LoadBinary<{0}>(\"{0}\"));", infos[i]));
            }
            sb.AppendLine(TAB + "}");

            // WriteAllCsvBinary
            sb.AppendLine(TAB + "public static void WriteAllCsvBinary(string root_path)");
            sb.AppendLine(TAB + "{");

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(TAB + TAB + string.Format("SaveBinaryFile<{0}>(root_path);", infos[i]));
            }
            sb.AppendLine(TAB + "}");

            // SaveBinaryFile
            sb.AppendLine(TAB + "public static void SaveBinaryFile<T>(string path) where T : BaseCsv");
            sb.AppendLine(TAB + "{");
            sb.AppendLine(TAB + TAB + "Type t = typeof(T);");
            sb.AppendLine(TAB + TAB + "string name = t.Name;");
            sb.AppendLine(TAB + TAB + "FileStream fs = new FileStream(path + name+\".bytes\", FileMode.Create);");
            sb.AppendLine(TAB + TAB + "BinaryWriter bw = new BinaryWriter(fs);");
            sb.AppendLine(TAB + TAB + "CsvLoader.WriteBinary<T>(StaticCnf.FindMap<T>(), bw);");
            sb.AppendLine(TAB + TAB + "bw.Flush();");
            sb.AppendLine(TAB + TAB + "bw.Close();");
            sb.AppendLine(TAB + TAB + "fs.Close();");
            sb.AppendLine(TAB + "}");
            sb.AppendLine("}");
            return sb.ToString();
        }

    }
}
