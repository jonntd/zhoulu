using System.IO;

public class SkillTypeCDListCnf
{
    // ID(技能类型)
    public int ID;

    // cd长度
    public int cdLenth;

    // 权重系数
    public int weight;


    public void Init(BinaryReader br)
    {
        ID = br.ReadInt32();
    }
}
