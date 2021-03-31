using System.Reflection;
using HarmonyLib;
using Verse;

namespace FireExt
{
    // Token: 0x0200000D RID: 13
    [StaticConstructorOnStartup]
    public static class HarmonyPatching
    {
        public static bool SSLoaded;

        // Token: 0x06000028 RID: 40 RVA: 0x00002EFC File Offset: 0x000010FC
        static HarmonyPatching()
        {
            SSLoaded = ModLister.GetActiveModWithIdentifier("PeteTimesSix.SimpleSidearms") != null;
            var harmony = new Harmony("com.Pelador.RW.ENSFE");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}