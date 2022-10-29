using HarmonyLib;
using PeteTimesSix.SimpleSidearms.Utilities;
using Verse;

namespace FESSFix;

[HarmonyPatch(typeof(StatCalculator), "RangedDPS")]
public class StatCalc_RangedDPS
{
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