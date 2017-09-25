public class StaticCnfLoader
{
	public static void LoadAllCsvFile()
	{
StaticCnf.Add(CsvLoader.LoadFile<areaCnf>("areaCnf"));
StaticCnf.Add(CsvLoader.LoadFile<attribute_type_configCnf>("attribute_type_configCnf"));
StaticCnf.Add(CsvLoader.LoadFile<avgCnf>("avgCnf"));
StaticCnf.Add(CsvLoader.LoadFile<BatchMonsterInfoCnf>("BatchMonsterInfoCnf"));
StaticCnf.Add(CsvLoader.LoadFile<BuffCnf>("BuffCnf"));
StaticCnf.Add(CsvLoader.LoadFile<HeroInfoCnf>("HeroInfoCnf"));
StaticCnf.Add(CsvLoader.LoadFile<MonsterInfoCnf>("MonsterInfoCnf"));
StaticCnf.Add(CsvLoader.LoadFile<SkillTypeCDListCnf>("SkillTypeCDListCnf"));
StaticCnf.Add(CsvLoader.LoadFile<SpaceInfoCnf>("SpaceInfoCnf"));
StaticCnf.Add(CsvLoader.LoadFile<SpellInfoCnf>("SpellInfoCnf"));
	}

	public static void LoadAllCsvBinary()
	{
StaticCnf.Add(CsvLoader.LoadBinary<areaCnf>("areaCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<attribute_type_configCnf>("attribute_type_configCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<avgCnf>("avgCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<BatchMonsterInfoCnf>("BatchMonsterInfoCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<BuffCnf>("BuffCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<HeroInfoCnf>("HeroInfoCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<MonsterInfoCnf>("MonsterInfoCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<SkillTypeCDListCnf>("SkillTypeCDListCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<SpaceInfoCnf>("SpaceInfoCnf"));
StaticCnf.Add(CsvLoader.LoadBinary<SpellInfoCnf>("SpellInfoCnf"));
	}
}
