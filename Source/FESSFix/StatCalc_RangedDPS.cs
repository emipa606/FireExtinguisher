using HarmonyLib;
using SimpleSidearms.utilities;
using Verse;

namespace FESSFix
{
    // Token: 0x02000004 RID: 4
    [HarmonyPatch(typeof(StatCalculator), "RangedDPS")]
    public class StatCalc_RangedDPS
    {
        // Token: 0x06000003 RID: 3 RVA: 0x0000209E File Offset: 0x0000029E
        [HarmonyPostfix]
        public static void PostFix(ref float __result, ThingWithComps weapon, float speedBias, float averageSpeed,
            float range)
        {
            if (Globals.isExtinguisher(weapon))
            {
                __result = 0f;
            }
        }
    }
}