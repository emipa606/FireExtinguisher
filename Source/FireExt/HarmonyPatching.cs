using System.Reflection;
using HarmonyLib;
using Verse;

namespace FireExt
{
    // Token: 0x0200000D RID: 13
    [StaticConstructorOnStartup]
    internal static class HarmonyPatching
    {
        // Token: 0x06000028 RID: 40 RVA: 0x00002EFC File Offset: 0x000010FC
        static HarmonyPatching()
        {
            var harmony = new Harmony("com.Pelador.RW.ENSFE");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}