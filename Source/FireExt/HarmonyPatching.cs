using System.Reflection;
using HarmonyLib;
using Verse;

namespace FireExt
{
    // Token: 0x0200000D RID: 13
    [StaticConstructorOnStartup]
    public static class HarmonyPatching
    {
        // Token: 0x06000028 RID: 40 RVA: 0x00002EFC File Offset: 0x000010FC
        static HarmonyPatching()
        {
            var harmony = new Harmony("com.Pelador.RW.ENSFE");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            if (ModLister.GetActiveModWithIdentifier("PeteTimesSix.SimpleSidearms") == null)
            {
                return;
            }

            var thingDef_IsWeaponGet = AccessTools.PropertyGetter(typeof(ThingDef), "IsWeapon");
            var thingDef_IsWeapon_Postfix =
                AccessTools.Method(typeof(HarmonyPatching), "ThingDef_IsWeapon_Postfix");
            harmony.Patch(thingDef_IsWeaponGet, null, new HarmonyMethod(thingDef_IsWeapon_Postfix));

            var workTagIsDisabledMeth = AccessTools.Method(typeof(Pawn), "WorkTagIsDisabled");
            var workTagIsDisabledPostfix = AccessTools.Method(typeof(HarmonyPatching), "WorkTagIsDisabledPostfix");
            harmony.Patch(workTagIsDisabledMeth, null, new HarmonyMethod(workTagIsDisabledPostfix));
        }

        public static void ThingDef_IsWeapon_Postfix(ref ThingDef __instance, ref bool __result)
        {
            Controller.LastWeaponCheckType = __instance;
            if (__instance != FireExtDefOf.Gun_Fire_Ext)
            {
                return;
            }

            __result = false;
        }

        public static void WorkTagIsDisabledPostfix(WorkTags w, ref Pawn __instance, ref bool __result)
        {
            if (w != WorkTags.Violent)
            {
                return;
            }

            if (__result == false)
            {
                return;
            }

            if (Controller.LastWeaponCheckType == FireExtDefOf.Gun_Fire_Ext &&
                __instance.equipment?.PrimaryEq?.parent?.def == FireExtDefOf.Gun_Fire_Ext)
            {
                __result = false;
            }
        }
    }
}