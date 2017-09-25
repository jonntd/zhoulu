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

            sb.AppendLine("public class StaticCnfLoader");
            sb.AppendLine("{");

            // LoadAllCsvFile function
            int length = infos.Length;
            sb.AppendLine(TAB + "public static void LoadAllCsvFile()");
            sb.AppendLine(TAB + "{");

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(string.Format("StaticCnf.Add(CsvLoader.LoadFile<{0}>(\"{0}\"));", infos[i]));
            }
            sb.AppendLine(TAB + "}");

            sb.AppendLine(string.Empty);

            // LoadAllCsvBinary function
            sb.AppendLine(TAB + "public static void LoadAllCsvBinary()");
            sb.AppendLine(TAB + "{");

            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(string.Format("StaticCnf.Add(CsvLoader.LoadBinary<{0}>(\"{0}\"));", infos[i]));
            }
            sb.AppendLine(TAB + "}");

            sb.AppendLine("}");
            return sb.ToString();
        }

    }
}
