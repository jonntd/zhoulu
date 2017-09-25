using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorCnf.Scripts
{
    public class TableData
    {
        public const string EXTENDS = "BaseCsv";        //强制继承BaseCsv
        public static EClassTableDefine ParseTable(List<string> contents, string file_name)
        {
            Program.Log("file_name:" + file_name);
            EClassTableDefine cd = new EClassTableDefine();
            cd.using_str.Add("using System.IO;");
            cd.comment = file_name;
            cd.name = file_name;
            cd.extend = EXTENDS;
            string[] dess = contents[0].Split(CodeTool.separator);
            string[] var_attrs = contents[1].Split(CodeTool.separator);
            string[] var_names = contents[2].Split(CodeTool.separator);

            for (int i = 0; i < dess.Length; i++)
            {
                EVariable e_var = CreateVar(dess[i], var_attrs[i], var_names[i]);
                cd.variables.Add(e_var);
            }
            return cd;
        }

        public static EVariable CreateVar(string comment, string attr, string name)
        {
            EVariable info = new EVariable();
            info.comment = comment;
            info.name = name;
            info.attribute = attr;
            info.access_limit = EAccessLimit.Public;

            info.Vaild();
            return info;
        }
    }
}
