using HarmonyLib;
using RimWorld;
using Verse;

namespace FireExt;

[HarmonyPatch(typeof(Building_TurretGun), "TryFindNewTarget")]
public class BTG_TryFindNewTarget_PrePatch
{
    [HarmonyPrefix]
    [HarmonyPriority(800)]
    public static bool PreFix(ref Building_TurretGun __instance, ref LocalTargetInfo __result)
    {
        bool result;
        if (__instance.def.defName == "Turret_MiniTurret_FE")
        {
            var AttackVerb = __instance.AttackVerb;
            var range = AttackVerb.verbProps.range;
            __result = FETargetUtility.GetFETarget(__instance, range);
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }
}