using System.Reflection;
using HarmonyLib;
using Verse;

namespace FESSFix;

[StaticConstructorOnStartup]
internal static class HarmonyPatching
{
    static HarmonyPatching()
    {
        new Harmony("com.Pelador.Rimworld.FESSFix").PatchAll(Assembly.GetExecutingAssembly());
    }
}