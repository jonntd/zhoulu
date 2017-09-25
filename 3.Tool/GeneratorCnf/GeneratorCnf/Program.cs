using System;
using System.Collections.Generic;
using System.IO;
using GeneratorCnf.Scripts;
namespace GeneratorCnf
{
    /// <summary>
    /// 慢慢分解这些内容
    /// 
    /// 这代码惨不忍睹呀！这代码惨不忍睹呀！这代码惨不忍睹呀！这代码惨不忍睹呀！这代码惨不忍睹呀！这代码惨不忍睹呀！这代码惨不忍睹呀！
    /// 
    /// 这就是一开始代码没有规划好 各个模块之间的功能导致的问题
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("开启麻少为你准备的自动代码生成之路");

                string current_directory = System.Environment.CurrentDirectory;

                //current_directory.LastIndexOf();
                Console.WriteLine("当前程序目录:" + current_directory);
                Console.WriteLine("读取配置文件:" + "msm_config.txt");

                string config_path = "D:\\github\\GeneratorCnf.txt";

                // 读取配置文件
                string content = ReadTxt(config_path);
                ResConfig config = new ResConfig();
                config.ParseText(content);
                EdNode node = config.GetRoot();
                CodeTool.SetData(node);

                // 读取tables
                ReadTables();
            }
            catch (Exception e)
            {
                Console.WriteLine("错误信息:" + e.Message);
            }
            finally
            {
                Console.WriteLine("按下任意键关闭程序");
                Console.ReadKey();
            }

        }


        public static string ReadTxt(string path)
        {
            using (
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        public static void ReadTables()
        {
            DirectoryInfo csv_folder = new DirectoryInfo(CodeTool.root_tables_path);
            FileInfo[] csv_files = csv_folder.GetFiles("*.csv", SearchOption.AllDirectories);

            List<string> out_files = new List<string>();
            foreach (FileInfo csv_file in csv_files)
            {
                string file_name = csv_file.Name;
                file_name = csv_file.Name.Replace(csv_file.Extension, string.Empty);
                if (CodeTool.IsIgnoreFile(file_name))
                    continue;
                //默认添加Cnf后缀
                file_name += "Cnf";
                out_files.Add(file_name);
                List<string> content_str_list = new List<string>();
                using (StreamReader sr = csv_file.OpenText())
                {
                    while (sr.Peek() > 0)
                    {
                        content_str_list.Add(sr.ReadLine());
                    }
                }


                EClassTableDefine class_define = TableData.ParseTable(content_str_list, file_name);
                CodeTool.SaveToCs(class_define);
            }

            string content = StaticCnfFactory.Create(out_files.ToArray());
            File.WriteAllText(CodeTool.root_out_path + "StaticCnfLoader.cs", content);
        }



        #region Log
        public static
        void Error(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("啦啦啦,啦啦啦,我是卖报的小行家  出现错误/好机会速怼傻逼程序员/走起");
            Console.ReadKey();
        }

        public static void Error(string message, params object[] arg)
        {
            Console.WriteLine(message, arg);
            Console.WriteLine("啦啦啦,啦啦啦,我是卖报的小行家  出现错误/好机会速怼傻逼程序员/走起");
            Console.ReadKey();
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Log(string message, params object[] arg)
        {
            Console.WriteLine(message, arg);
        }
        #endregion
    }
}
