using System.IO;
public class HeroInfoCnf : BaseCsv
{
	 // ID(英雄模板id)
	 public int id;

	 // 英雄名字
	 public string hero_name;

	 // 多版本名字id
	 public int other_name;

	 // 战斗头像图片
	 public string icon;

	 // 全身像图片
	 public string icon_1;

	 // 名字图片
	 public string icon_2;

	 // 非战斗头像图片
	 public string icon_3;

	 // PrefabName
	 public string prefab_name;

	 // 模型缩放
	 public string scale;

	 // 特效缩放
	 public int effect_scale;

	 // 攻击类型
	 public int atk_type;

	 // 安全攻击距离
	 public int safe_range;

	 // 人物模型半径
	 public int radius;

	 // 死亡技能id
	 public float dead_skill;

	 // 移动速度
	 public int move_speed;

	 // 技能id(普攻只记录第一下id)
	 public int[] skillid_list;

	 // 是否允许被推挤
	 public int push_valid;

	 // 性别
	 public int sex;

	 // 职业
	 public int profession;

	 // 属性
	 public int property ;

	 // 国家
	 public int country;

	 // 攻击力
	 public int damage;

	 // 攻击资质
	 public int dmg_add;

	 // 防御
	 public int defense;

	 // 防御资质
	 public int def_add;

	 // 血量
	 public int max_hp;

	 // 血量资质
	 public int hp_add;

	 // 暴击率
	 public int cri_rate;

	 // 抗暴率
	 public int anti_cri;

	 // 破击率
	 public int wreck;

	 // 格挡率
	 public int block;

	 // 攻速
	 public int atk_speed;

	 // 暴伤
	 public int cri_dmg;

	 // 抗暴伤
	 public int anti_cri_dmg;

	 // 格挡免伤率
	 public int block_intensity;

	 // 伤害率
	 public int atk_intensity;

	 // 免伤率
	 public int def_intensity;

	 // 吸血率
	 public int suck_blood;

	 // 吸血抵抗率
	 public int anti_suck_blood;

	 // 知识
	 public int knowledge;

	 // 名望
	 public int fame;

	 // 统率
	 public int lead;

	 // 反伤率
	 public int reflect_dmg_per;

	 // 无双值
	 public int sp;

	 // 天赋
	 public int gift;

	 // 羁绊人物
	 public int link_hero;

	 // 羁绊技能
	 public int link_skill;

	 // 升星消耗
	 public int[] upgrade_star_cost;

	public override int GetId()
	{
		return id;
	}
	public override void InitByBinary(BinaryReader reader)
	{
		id = reader.ReadInt32();

		hero_name = reader.ReadString();

		other_name = reader.ReadInt32();

		icon = reader.ReadString();

		icon_1 = reader.ReadString();

		icon_2 = reader.ReadString();

		icon_3 = reader.ReadString();

		prefab_name = reader.ReadString();

		scale = reader.ReadString();

		effect_scale = reader.ReadInt32();

		atk_type = reader.ReadInt32();

		safe_range = reader.ReadInt32();

		radius = reader.ReadInt32();

		dead_skill = reader.ReadSingle();

		move_speed = reader.ReadInt32();

		int length_skillid_list = reader.ReadInt32();
		skillid_list = new int[length_skillid_list];
		for(int i = 0; i < length_skillid_list; i++)
		{
		skillid_list[i] = reader.ReadInt32();
		}

		push_valid = reader.ReadInt32();

		sex = reader.ReadInt32();

		profession = reader.ReadInt32();

		property  = reader.ReadInt32();

		country = reader.ReadInt32();

		damage = reader.ReadInt32();

		dmg_add = reader.ReadInt32();

		defense = reader.ReadInt32();

		def_add = reader.ReadInt32();

		max_hp = reader.ReadInt32();

		hp_add = reader.ReadInt32();

		cri_rate = reader.ReadInt32();

		anti_cri = reader.ReadInt32();

		wreck = reader.ReadInt32();

		block = reader.ReadInt32();

		atk_speed = reader.ReadInt32();

		cri_dmg = reader.ReadInt32();

		anti_cri_dmg = reader.ReadInt32();

		block_intensity = reader.ReadInt32();

		atk_intensity = reader.ReadInt32();

		def_intensity = reader.ReadInt32();

		suck_blood = reader.ReadInt32();

		anti_suck_blood = reader.ReadInt32();

		knowledge = reader.ReadInt32();

		fame = reader.ReadInt32();

		lead = reader.ReadInt32();

		reflect_dmg_per = reader.ReadInt32();

		sp = reader.ReadInt32();

		gift = reader.ReadInt32();

		link_hero = reader.ReadInt32();

		link_skill = reader.ReadInt32();

		int length_upgrade_star_cost = reader.ReadInt32();
		upgrade_star_cost = new int[length_upgrade_star_cost];
		for(int i = 0; i < length_upgrade_star_cost; i++)
		{
		upgrade_star_cost[i] = reader.ReadInt32();
		}

	}
}
