

using System.Text;

namespace GeneratorCnf.Scripts
{
    public class EClassTableDefine : EClassDefine
    {
        public override string ToDes(string tab)
        {
            string ttab = tab + TAB;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < using_str.Count; i++)
            {
                sb.AppendLine(using_str[i]);
            }
            //TODO 这里的Cnf和外部的cnf要统一化 实际是有问题的
            if (string.IsNullOrEmpty(extend))
                sb.AppendLine(string.Format("public class {0}", comment));
            else
                sb.AppendLine(string.Format("public class {0} : {1}", comment, extend));
            sb.AppendLine("{");
            for (int i = 0; i < variables.Count; i++)
            {
                sb.AppendLine(variables[i].ToDes(ttab));
            }

            sb.AppendLine(ttab + "public override int GetId()");
            sb.AppendLine(ttab + "{");
            sb.AppendLine(ttab + ttab + string.Format("return {0};", variables[0].name));
            sb.AppendLine(ttab + "}");

            // public virtual void InitByBinary(BinaryReader reader)
            sb.AppendLine(ttab + "public override void InitByBinary(BinaryReader reader)");
            sb.AppendLine(ttab + "{");
            for (int i = 0; i < variables.Count; i++)
            {
                sb.AppendLine(variables[i].ToWrite(ttab + TAB));
            }
            sb.AppendLine(ttab + "}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
