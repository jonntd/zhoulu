using System.IO;
public class MonsterInfoCnf : BaseCsv
{
	 // ID(怪物模板id)
	 public int ID;

	 // 怪物名字
	 public string Name;

	 // 多版本名字id
	 public int OtherName;

	 // 小图标名字
	 public string IconName;

	 // PrefabName
	 public string PrefabName;

	 // 模型缩放
	 public string scale;

	 // 特效缩放
	 public int effectScale;

	 // 攻击类型
	 public int AtkType;

	 // 安全攻击距离
	 public int safeRange;

	 // 人物模型半径
	 public int radius;

	 // 死亡技能id
	 public float deadSkill;

	 // 移动速度
	 public int moveSpeed;

	 // 备战移动速度
	 public int atk_movespeed;

	 // 后撤速度
	 public int escape_movespeed;

	 // 技能id(普攻只记录第一下id)
	 public int[] skillidList;

	 // 是否允许被推挤
	 public int pushValid;

	 // 基础血量
	 public int MaxHp;

	 // 基础攻击伤害
	 public int damage;

	 // 防御
	 public int defense;

	 // 警戒圈R1
	 public int warning1;

	 // 警戒圈R2
	 public int warning2;

	 // 警戒圈R3
	 public int warning3;

	 // 警戒圈R4
	 public int warning4;

	 // 攻击完等待时间
	 public int waitleaveTime;

	 // 怪物类型
	 public int enemy_type;

	 // 霸体值上限
	 public int super_armor;

	 // 霸体值恢复速度
	 public int super_armor_recovery;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByBinary(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		Name = reader.ReadString();

		OtherName = reader.ReadInt32();

		IconName = reader.ReadString();

		PrefabName = reader.ReadString();

		scale = reader.ReadString();

		effectScale = reader.ReadInt32();

		AtkType = reader.ReadInt32();

		safeRange = reader.ReadInt32();

		radius = reader.ReadInt32();

		deadSkill = reader.ReadSingle();

		moveSpeed = reader.ReadInt32();

		atk_movespeed = reader.ReadInt32();

		escape_movespeed = reader.ReadInt32();

		int length_skillidList = reader.ReadInt32();
		skillidList = new int[length_skillidList];
		for(int i = 0; i < length_skillidList; i++)
		{
		skillidList[i] = reader.ReadInt32();
		}

		pushValid = reader.ReadInt32();

		MaxHp = reader.ReadInt32();

		damage = reader.ReadInt32();

		defense = reader.ReadInt32();

		warning1 = reader.ReadInt32();

		warning2 = reader.ReadInt32();

		warning3 = reader.ReadInt32();

		warning4 = reader.ReadInt32();

		waitleaveTime = reader.ReadInt32();

		enemy_type = reader.ReadInt32();

		super_armor = reader.ReadInt32();

		super_armor_recovery = reader.ReadInt32();

	}
}
