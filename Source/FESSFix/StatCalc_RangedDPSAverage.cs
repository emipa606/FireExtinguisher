using HarmonyLib;
using PeteTimesSix.SimpleSidearms.Utilities;
using Verse;

namespace FESSFix;

[HarmonyPatch(typeof(StatCalculator), "RangedDPSAverage")]
public class StatCalc_RangedDPSAverage
{
    [HarmonyPostfix]
    public static void PostFix(ref float __result, ThingWithComps weapon, float speedBias, float averageSpeed)
    {
        if (Globals.isExtinguisher(weapon))
        {
            __result = 0f;
        }
    }
}