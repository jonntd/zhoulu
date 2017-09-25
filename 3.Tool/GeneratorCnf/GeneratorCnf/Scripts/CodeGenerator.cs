﻿
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CSharp;

namespace GeneratorCnf.Scripts
{
    //访问权限定义
    public enum EAccessLimit
    {
        Public,
        Private,
        Internal,
    }
    //编译类型
    public enum ECompileType
    {
        Const,
        Static,
        member,
    }
    //类型
    public enum ETypeDefine
    {
        String,
        Int,
        Short,
        Char,
        Float,
        Double,
    }
    //变量定义
    public class EVariable
    {
        public string comment;                                      // 描述
        public EAccessLimit access_limit;                           // 访问权限
        public string name;                                         // 变量名
        public string attribute;                                    // 变量的类型

        //public CodeTypeReference type;                              // 类型
        //public ECompileType compile_type = ECompileType.member;     // 编译类型
        //public string value;                                        // 值

        public EVariable()
        {
        }
        public bool Vaild()
        {
            switch (attribute)
            {
                case "int":
                    break;
                case "int[]":
                    break;
                case "float":
                    break;
                case "float[]":
                    break;
                case "string":
                    break;
                case "string[]":
                    break;
                case "bool":
                    break;
                case "bool[]":
                    break;
                default:
                    Program.Error("非法属性|字段[{0}]", name);
                    return false;
            }
            return true;
        }

        public string ToDes(string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} // {1}", tab, comment));
            sb.AppendLine(string.Format("{0} public {1} {2};", tab, attribute, name));

            return sb.ToString();
        }

        public string ToWrite(string tab)
        {
            string length_name = string.Empty;
            StringBuilder sb = new StringBuilder();
            switch (attribute)
            {
                case "int":
                    sb.AppendLine(tab + string.Format("{0} = reader.ReadInt32();", name));
                    break;
                case "int[]":
                    length_name = string.Format("length_{0}", name);
                    sb.AppendLine(tab + string.Format("int {0} = reader.ReadInt32();", length_name));
                    sb.AppendLine(tab + string.Format("{0} = new int[{1}];", name, length_name));

                    sb.AppendLine(tab + string.Format("for(int i = 0; i < {0}; i++)", length_name));
                    sb.AppendLine(tab + "{");
                    sb.AppendLine(tab + string.Format("{0}[i] = reader.ReadInt32();", name));
                    sb.AppendLine(tab + "}");

                    break;
                case "float":
                    sb.AppendLine(tab + string.Format("{0} = reader.ReadSingle();", name));

                    break;
                case "float[]":

                    length_name = string.Format("length_{0}", name);
                    sb.AppendLine(tab + string.Format("int {0} = reader.ReadInt32();", length_name));
                    sb.AppendLine(tab + string.Format("{0} = new string[{1}];", name, length_name));

                    sb.AppendLine(string.Format("for(int i = 0; i < {0}; i++)", length_name));
                    sb.AppendLine(tab + "{");
                    sb.AppendLine(tab + tab + string.Format("{0}[i] = reader.ReadSingle();", name));
                    sb.AppendLine(tab + "}");
                    break;
                case "string":
                    sb.AppendLine(tab + string.Format("{0} = reader.ReadString();", name));
                    break;
                case "string[]":
                    length_name = string.Format("length_{0}", name);
                    sb.AppendLine(tab + string.Format("int {0} = reader.ReadInt32();", length_name));
                    sb.AppendLine(tab + string.Format("{0} = new string[{1}];", name, length_name));

                    sb.AppendLine(tab + string.Format("for(int i = 0; i < {0}; i++)", length_name));
                    sb.AppendLine(tab + "{");
                    sb.AppendLine(tab + string.Format("{0}[i] = reader.ReadString();", name));
                    sb.AppendLine(tab + "}");
                    break;
                case "bool":
                    sb.AppendLine(string.Format("{0} = reader.ReadBoolean();", name));
                    break;
                case "bool[]":
                    length_name = string.Format("length_{0}", name);
                    sb.AppendLine(tab + string.Format("int {0} = reader.ReadInt32();", length_name));
                    sb.AppendLine(tab + string.Format("{0} = new bool[{1}];", name, length_name));

                    sb.AppendLine(tab + string.Format("for(int i = 0; i < {0}; i++)", length_name));

                    sb.AppendLine(tab + "for(int i = 0; i < length; i++)");
                    sb.AppendLine(tab + "{");
                    sb.AppendLine(tab + tab + string.Format("{0}[i] = reader.ReadBoolean();", name));
                    sb.AppendLine(tab + "}");
                    break;
                default:
                    Program.Error("--------非法属性|字段[{0}]---------", name);
                    break;
            }
            return sb.ToString();
        }


    }

    public class ETypeUtil
    {
        public static CodeTypeReference GetCodeType(ETypeDefine type)
        {
            CodeTypeReference ret_type = null;
            switch (type)
            {
                case ETypeDefine.Char:
                    ret_type = new CodeTypeReference(typeof(System.Char));
                    break;
                case ETypeDefine.Double:
                    ret_type = new CodeTypeReference(typeof(System.Double));
                    break;
                case ETypeDefine.Float:
                    ret_type = new CodeTypeReference(typeof(System.Decimal));
                    break;
                case ETypeDefine.Int:
                    ret_type = new CodeTypeReference(typeof(System.Int32));
                    break;
                case ETypeDefine.Short:
                    ret_type = new CodeTypeReference(typeof(System.Int16));
                    break;
                case ETypeDefine.String:
                    ret_type = new CodeTypeReference(typeof(System.String));
                    break;
            }
            return ret_type;
        }
    }

    #region namespace
    /// <summary>
    /// 命名空间
    /// </summary>
    public class ENamespaceDefine
    {
        public string comment;                      //注释
        public string name;                         //名字
        public List<EClassDefine> classes           //包含的类
            = new List<EClassDefine>();
        public string file_name;                    //生成的文件名
        public string generate_dir = "";            //生成的路径
    }
    #endregion

    #region Class
    /// <summary>
    /// 类定义
    /// </summary>
    public class EClassDefine
    {
        public List<string> using_str = new List<string>();
        public string extend;                       //继承
        public string comment;                      //注释
        public string name;                         //类名字
        public List<EVariable> variables            //变量
            = new List<EVariable>();
        public List<EProperty> properties           //属性定义 
            = new List<EProperty>();

        public const string TAB = "\t";
        public virtual string ToDes(string tab)
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
            sb.AppendLine("}");
            return sb.ToString();
        }

    }

    #endregion

    #region Property
    /// <summary>
    /// 属性定义只支持Get
    /// </summary>
    public class EProperty
    {
        public string comment;                                      //描述
        public EAccessLimit access_limit;                           //访问权限
        public CodeTypeReference type;                              //类型
        public ECompileType compile_type = ECompileType.member;     //编译类型
        public string name;                                         //变量名
        public string get_return_code;                              //代码

        public EProperty() { }

        public EProperty(EAccessLimit taccess_limit, ECompileType tcompile_type, ETypeDefine type, string name, string tget_return_code, string comment = null)
        {
            access_limit = taccess_limit;
            this.type = ETypeUtil.GetCodeType(type);
            compile_type = tcompile_type;
            this.name = name;
            get_return_code = tget_return_code;
            this.comment = comment;
        }

    }

    #endregion


    /// <summary>
    /// 代码生成工具
    /// </summary>
    public class CodeGenerator
    {
        public static void Generate(ENamespaceDefine name_space)
        {
            CreateDirIfNotExists(name_space.generate_dir);

            CodeCompileUnit compile_unit = new CodeCompileUnit();
            CodeNamespace code_name_space = new CodeNamespace(name_space.name);
            compile_unit.Namespaces.Add(code_name_space);

            foreach (var class_define in name_space.classes)
            {
                CodeTypeDeclaration code_type = new CodeTypeDeclaration(class_define.name);
                code_name_space.Types.Add(code_type);

                AddDocumentComment(code_type.Comments, class_define.comment);

                foreach (var variable in class_define.variables)
                {
                    AddVariable(code_type, variable);
                }

                foreach (var property in class_define.properties)
                {
                    AddProperty(code_type, property);
                }

            }
            var provider = new CSharpCodeProvider();
            var options = new CodeGeneratorOptions();
            options.BlankLinesBetweenMembers = false;

            StreamWriter writer = new StreamWriter(File.Open(Path.GetFullPath(name_space.generate_dir + Path.DirectorySeparatorChar + name_space.file_name), FileMode.Create));

            provider.GenerateCodeFromCompileUnit(compile_unit, writer, options);
            writer.Close();
        }

        //添加注释
        static void AddDocumentComment(CodeCommentStatementCollection comments, string comment_content)
        {
            if (!string.IsNullOrEmpty(comment_content))
            {
                comments.Add(new CodeCommentStatement(new CodeComment("<summary>", true)));
                comments.Add(new CodeCommentStatement(new CodeComment(comment_content, true)));
                comments.Add(new CodeCommentStatement(new CodeComment("</summary>", true)));
            }
        }

        //添加变量
        static void AddVariable(CodeTypeDeclaration code_type, EVariable variable)
        {
            CodeMemberField name_field = new CodeMemberField();

            AddDocumentComment(name_field.Comments, variable.comment);

            switch (variable.access_limit)
            {
                case EAccessLimit.Public:
                    name_field.Attributes = MemberAttributes.Public;
                    break;
                case EAccessLimit.Private:
                    name_field.Attributes = MemberAttributes.Private;
                    break;
            }

            /* switch (variable.compile_type)
             {
                 case ECompileType.Const:
                     name_field.Attributes |= MemberAttributes.Const;
                     break;
                 case ECompileType.Static:
                     name_field.Attributes |= MemberAttributes.Static;
                     break;
             }*/

            /* name_field.Name = variable.name;
             name_field.Type = variable.type;
             name_field.InitExpression = new CodePrimitiveExpression(variable.value);*/

            code_type.Members.Add(name_field);
        }

        static void AddProperty(CodeTypeDeclaration code_type, EProperty property)
        {
            CodeMemberProperty get_property = new CodeMemberProperty();

            AddDocumentComment(get_property.Comments, property.comment);

            switch (property.access_limit)
            {
                case EAccessLimit.Public:
                    get_property.Attributes = MemberAttributes.Public;
                    break;
                case EAccessLimit.Private:
                    get_property.Attributes = MemberAttributes.Private;
                    break;
            }

            switch (property.compile_type)
            {
                case ECompileType.Const:
                    get_property.Attributes |= MemberAttributes.Const;
                    break;
                case ECompileType.Static:
                    get_property.Attributes |= MemberAttributes.Static;
                    break;
            }

            get_property.Name = property.name;
            get_property.Type = property.type;
            get_property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, property.get_return_code)));

            code_type.Members.Add(get_property);
        }

        static string CreateDirIfNotExists(string dir_full_path)
        {
            if (!Directory.Exists(dir_full_path))
            {
                Directory.CreateDirectory(dir_full_path);
            }
            return dir_full_path;
        }
    }
}

