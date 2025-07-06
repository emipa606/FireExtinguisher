using System.Reflection;
using HarmonyLib;
using Verse;

namespace FireExt;

[StaticConstructorOnStartup]
public static class HarmonyPatching
{
    static HarmonyPatching()
    {
        var harmony = new Harmony("com.Pelador.RW.ENSFE");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        if (ModLister.GetActiveModWithIdentifier("PeteTimesSix.SimpleSidearms", true) != null)
        {
            return;
        }

        var thingDefIsWeaponGet = AccessTools.PropertyGetter(typeof(ThingDef), nameof(ThingDef.IsWeapon));
        var thingDefIsWeaponPostfix =
            AccessTools.Method(typeof(HarmonyPatching), nameof(ThingDef_IsWeapon_Postfix));
        harmony.Patch(thingDefIsWeaponGet, null, new HarmonyMethod(thingDefIsWeaponPostfix));

        var workTagIsDisabledMeth = AccessTools.Method(typeof(Pawn), nameof(Pawn.WorkTagIsDisabled));
        var workTagIsDisabledPostfix = AccessTools.Method(typeof(HarmonyPatching), nameof(WorkTagIsDisabledPostfix));
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

        if (!__result)
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