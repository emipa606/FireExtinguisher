using System.Reflection;
using HarmonyLib;
using Verse;

namespace FESSFix
{
    // Token: 0x02000003 RID: 3
    [StaticConstructorOnStartup]
    internal static class HarmonyPatching
    {
        // Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
        static HarmonyPatching()
        {
            new Harmony("com.Pelador.Rimworld.FESSFix").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}