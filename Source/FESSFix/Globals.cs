using Verse;

namespace FESSFix
{
    // Token: 0x02000002 RID: 2
    public static class Globals
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        internal static bool isExtinguisher(ThingWithComps weapon)
        {
            return weapon != null &&
                   (weapon.def.defName == "Gun_Fire_Ext" || weapon.def.defName == "VWE_Gun_FireExtinguisher");
        }
    }
}