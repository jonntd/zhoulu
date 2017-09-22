using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCnf.Scripts
{
    public class CodeTool
    {
        public static char separator = ',';
        public static string root_tables_path = "";                          //表格根目录
        public static string root_out_path = "";
        public static List<string> ignore_files = new List<string>();         //忽略文件




        public static void SetData(EdNode root_node)
        {
            //EdNode root_node = node.GetNode("RootTop");
            root_tables_path = root_node.GetAttribute("root_tables_path").ToStr();
            root_out_path = root_node.GetAttribute("root_out_path").ToStr();


            ignore_files.Clear();
            string ignore_file = root_node.GetAttribute("ignore_files").ToStr();
            string[] files = ignore_file.Split('|');
            for (int i = 0; i < files.Length; i++)
            {
                ignore_files.Add(files[i]);
            }

        }

        public static bool IsIgnoreFile(string file_name)
        {
            if (ignore_files.Contains(file_name))
                return true;
            return false;
        }

        public static void SaveToCs(EClassDefine class_define)
        {
            File.WriteAllText(CodeTool.root_out_path + class_define.name + ".cs", class_define.ToDes(""));
        }

    }
}
