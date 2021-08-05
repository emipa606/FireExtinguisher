using HarmonyLib;
using PeteTimesSix.SimpleSidearms.Utilities;
using Verse;

namespace FESSFix
{
    // Token: 0x02000005 RID: 5
    [HarmonyPatch(typeof(StatCalculator), "RangedDPSAverage")]
    public class StatCalc_RangedDPSAverage
    {
        // Token: 0x06000005 RID: 5 RVA: 0x000020B7 File Offset: 0x000002B7
        [HarmonyPostfix]
        public static void PostFix(ref float __result, ThingWithComps weapon, float speedBias, float averageSpeed)
        {
            if (Globals.isExtinguisher(weapon))
            {
                __result = 0f;
            }
        }
    }
}