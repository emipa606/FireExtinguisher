using HarmonyLib;
using Verse;

namespace FireExt
{
    [HarmonyPatch(typeof(Pawn), "WorkTagIsDisabled", typeof(WorkTags))]
    public class Pawn_WorkTagIsDisabled_Postfix
    {
        [HarmonyPostfix]
        public static void Postfix(WorkTags w, ref Pawn __instance, ref bool __result)
        {
            if (HarmonyPatching.SSLoaded)
            {
                return;
            }

            if (w != WorkTags.Violent)
            {
                return;
            }

            if (__result == false)
            {
                return;
            }


            if (Controller.LastWeaponCheckType == "Gun_Fire_Ext" &&
                __instance.equipment?.PrimaryEq?.parent?.def?.defName == "Gun_Fire_Ext")
            {
                __result = false;
            }
        }
    }
}