using System.IO;
public class BatchMonsterInfoCnf : BaseCsv
{
	 // ID(对应地图信息和波次)
	 public int ID;

	 // 对应关卡id
	 public int ChapterID;

	 // 多版本名字id
	 public int OtherName;

	 // 对应怪物id信息
	 public int[] MonsterBornInfo;

	 // 对应怪物数量
	 public int[] MonsterCount;

	 // 对应怪物等级
	 public int[] MonsterLevel;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByBinary(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		ChapterID = reader.ReadInt32();

		OtherName = reader.ReadInt32();

		int length_MonsterBornInfo = reader.ReadInt32();
		MonsterBornInfo = new int[length_MonsterBornInfo];
		for(int i = 0; i < length_MonsterBornInfo; i++)
		{
		MonsterBornInfo[i] = reader.ReadInt32();
		}

		int length_MonsterCount = reader.ReadInt32();
		MonsterCount = new int[length_MonsterCount];
		for(int i = 0; i < length_MonsterCount; i++)
		{
		MonsterCount[i] = reader.ReadInt32();
		}

		int length_MonsterLevel = reader.ReadInt32();
		MonsterLevel = new int[length_MonsterLevel];
		for(int i = 0; i < length_MonsterLevel; i++)
		{
		MonsterLevel[i] = reader.ReadInt32();
		}

	}
}
