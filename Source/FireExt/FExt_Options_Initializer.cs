using RimWorld;
using Verse;

namespace FireExt
{
    // Token: 0x0200000B RID: 11
    [StaticConstructorOnStartup]
    internal static class FExt_Options_Initializer
    {
        // Token: 0x04000006 RID: 6
        private static bool extChecked;

        // Token: 0x04000007 RID: 7
        private static bool foamChecked;

        // Token: 0x04000008 RID: 8
        private static bool foamCheckedCE;

        // Token: 0x06000019 RID: 25 RVA: 0x0000263F File Offset: 0x0000083F
        static FExt_Options_Initializer()
        {
            LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00002670 File Offset: 0x00000870
        private static bool IsCELoaded()
        {
            return ModLister.HasActiveModWithName("Combat Extended");
        }

        // Token: 0x0600001B RID: 27 RVA: 0x0000268C File Offset: 0x0000088C
        private static void Setup()
        {
            if (!IsCELoaded())
            {
                foamCheckedCE = true;
            }

            var allDefsListForReading = DefDatabase<ThingDef>.AllDefsListForReading;
            checked
            {
                for (var i = 0; i < allDefsListForReading.Count; i++)
                {
                    if (allDefsListForReading[i].defName == "Gun_Fire_Ext")
                    {
                        allDefsListForReading[i].SetStatBaseValue(StatDefOf.RangedWeapon_Cooldown,
                            (float) Controller.Settings.CoolDownValue);
                        foreach (var verbProperties in allDefsListForReading[i].Verbs)
                        {
                            if (verbProperties.warmupTime > 0f)
                            {
                                verbProperties.warmupTime = (float) Controller.Settings.WarmUpValue;
                            }

                            if (verbProperties.range > 0f)
                            {
                                verbProperties.range = (int) Controller.Settings.RangeValue;
                            }

                            if (verbProperties.burstShotCount > 0f)
                            {
                                verbProperties.burstShotCount = (int) Controller.Settings.BurstValue;
                            }
                        }

                        extChecked = true;
                    }

                    if (allDefsListForReading[i].defName == "Bullet_Fire_Ext_Foam")
                    {
                        allDefsListForReading[i].projectile.speed = (int) Controller.Settings.SpeedValue;
                        allDefsListForReading[i].projectile.explosionRadius = (float) Controller.Settings.RadiusValue;
                        var useCleanFoam = Controller.Settings.UseCleanFoam;
                        allDefsListForReading[i].projectile.postExplosionSpawnThingDef =
                            DefDatabase<ThingDef>.GetNamed(useCleanFoam ? "Filth_FExtFireFoam" : "Filth_FireFoam");
                        foamChecked = true;
                    }

                    if (!foamCheckedCE)
                    {
                        if (allDefsListForReading[i].defName == "Bullet_FireExtFoamCE")
                        {
                            allDefsListForReading[i].projectile.speed = (int) Controller.Settings.SpeedValue;
                            allDefsListForReading[i].projectile.explosionRadius =
                                (float) Controller.Settings.RadiusValue;
                            var useCleanFoam2 = Controller.Settings.UseCleanFoam;
                            allDefsListForReading[i].projectile.postExplosionSpawnThingDef =
                                DefDatabase<ThingDef>.GetNamed(useCleanFoam2 ? "Filth_FExtFireFoam" : "Filth_FireFoam");
                            foamCheckedCE = true;
                        }
                    }

                    if (extChecked && foamChecked && foamCheckedCE)
                    {
                        i = allDefsListForReading.Count;
                    }
                }
            }
        }
    }
}