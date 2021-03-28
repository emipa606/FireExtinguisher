﻿using HarmonyLib;
using Verse;

namespace FireExt
{
    [HarmonyPatch(typeof(ThingDef), "IsWeapon", MethodType.Getter)]
    public class ThingDef_IsWeapon_Postfix
    {
        [HarmonyPostfix]
        public static void Postfix(ref ThingDef __instance, ref bool __result)
        {
            Controller.LastWeaponCheckType = __instance.defName;
            if (__instance.defName != "Gun_Fire_Ext")
            {
                return;
            }

            __result = false;
        }
    }
}