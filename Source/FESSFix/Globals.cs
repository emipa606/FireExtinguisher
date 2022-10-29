using Verse;

namespace FESSFix;

public static class Globals
{
    internal static bool isExtinguisher(ThingWithComps weapon)
    {
        return weapon != null &&
               weapon.def.defName is "Gun_Fire_Ext" or "VWE_Gun_FireExtinguisher";
    }
}