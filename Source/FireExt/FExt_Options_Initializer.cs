using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt
{
	// Token: 0x0200000B RID: 11
	[StaticConstructorOnStartup]
	internal static class FExt_Options_Initializer
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000263F File Offset: 0x0000083F
		static FExt_Options_Initializer()
		{
			LongEventHandler.QueueLongEvent(new Action(FExt_Options_Initializer.Setup), "LibraryStartup", false, null, true);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002670 File Offset: 0x00000870
		private static bool IsCELoaded()
		{
			return ModLister.HasActiveModWithName("Combat Extended");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000268C File Offset: 0x0000088C
		public static void Setup()
		{
			bool flag = !FExt_Options_Initializer.IsCELoaded();
			if (flag)
			{
				FExt_Options_Initializer.foamCheckedCE = true;
			}
			List<ThingDef> allDefsListForReading = DefDatabase<ThingDef>.AllDefsListForReading;
			checked
			{
				for (int i = 0; i < allDefsListForReading.Count; i++)
				{
					bool flag2 = allDefsListForReading[i].defName == "Gun_Fire_Ext";
					if (flag2)
					{
						allDefsListForReading[i].SetStatBaseValue(StatDefOf.RangedWeapon_Cooldown, (float)Controller.Settings.CoolDownValue);
						foreach (VerbProperties verbProperties in allDefsListForReading[i].Verbs)
						{
							bool flag3 = verbProperties.warmupTime > 0f;
							if (flag3)
							{
								verbProperties.warmupTime = (float)Controller.Settings.WarmUpValue;
							}
							bool flag4 = verbProperties.range > 0f;
							if (flag4)
							{
								verbProperties.range = (float)((int)Controller.Settings.RangeValue);
							}
							bool flag5 = (float)verbProperties.burstShotCount > 0f;
							if (flag5)
							{
								verbProperties.burstShotCount = (int)Controller.Settings.BurstValue;
							}
						}
						FExt_Options_Initializer.extChecked = true;
					}
					bool flag6 = allDefsListForReading[i].defName == "Bullet_Fire_Ext_Foam";
					if (flag6)
					{
						allDefsListForReading[i].projectile.speed = (float)((int)Controller.Settings.SpeedValue);
						allDefsListForReading[i].projectile.explosionRadius = (float)Controller.Settings.RadiusValue;
						bool useCleanFoam = Controller.Settings.UseCleanFoam;
						if (useCleanFoam)
						{
							allDefsListForReading[i].projectile.postExplosionSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FExtFireFoam", true);
						}
						else
						{
							allDefsListForReading[i].projectile.postExplosionSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FireFoam", true);
						}
						FExt_Options_Initializer.foamChecked = true;
					}
					bool flag7 = !FExt_Options_Initializer.foamCheckedCE;
					if (flag7)
					{
						bool flag8 = allDefsListForReading[i].defName == "Bullet_FireExtFoamCE";
						if (flag8)
						{
							allDefsListForReading[i].projectile.speed = (float)((int)Controller.Settings.SpeedValue);
							allDefsListForReading[i].projectile.explosionRadius = (float)Controller.Settings.RadiusValue;
							bool useCleanFoam2 = Controller.Settings.UseCleanFoam;
							if (useCleanFoam2)
							{
								allDefsListForReading[i].projectile.postExplosionSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FExtFireFoam", true);
							}
							else
							{
								allDefsListForReading[i].projectile.postExplosionSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FireFoam", true);
							}
							FExt_Options_Initializer.foamCheckedCE = true;
						}
					}
					bool flag9 = FExt_Options_Initializer.extChecked && FExt_Options_Initializer.foamChecked && FExt_Options_Initializer.foamCheckedCE;
					if (flag9)
					{
						i = allDefsListForReading.Count;
					}
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private static bool extChecked = false;

		// Token: 0x04000007 RID: 7
		private static bool foamChecked = false;

		// Token: 0x04000008 RID: 8
		private static bool foamCheckedCE = false;
	}
}
