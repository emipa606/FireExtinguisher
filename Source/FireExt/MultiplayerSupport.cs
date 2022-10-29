using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace FireExt;

[StaticConstructorOnStartup]
internal static class MultiplayerSupport
{
    private static readonly Harmony harmony = new Harmony("rimworld.pelador.ffoambelt.multiplayersupport");

    static MultiplayerSupport()
    {
        if (MP.enabled)
        {
            MP.RegisterSyncMethod(typeof(FireFoamBelt), "DoFFoam");
        }
    }

    private static void FixRNG(MethodInfo method)
    {
        harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPre"),
            new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPos"));
    }

    private static void FixRNGPre()
    {
        Rand.PushState(Find.TickManager.TicksAbs);
    }

    private static void FixRNGPos()
    {
        Rand.PopState();
    }
}