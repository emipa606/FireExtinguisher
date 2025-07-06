using HarmonyLib;
using RimWorld;
using Verse;

namespace FireExt;

[HarmonyPatch(typeof(Building_TurretGun), nameof(Building_TurretGun.TryFindNewTarget))]
[HarmonyPriority(800)]
public class Building_TurretGun_TryFindNewTarget
{
    public static bool Prefix(ref Building_TurretGun __instance, ref LocalTargetInfo __result)
    {
        bool result;
        if (__instance.def.defName == "Turret_MiniTurret_FE")
        {
            var attackVerb = __instance.AttackVerb;
            var range = attackVerb.verbProps.range;
            __result = FETargetUtility.GetFeTarget(__instance, range);
            result = false;
        }
        else
        {
            result = true;
        }

        return result;
    }
}