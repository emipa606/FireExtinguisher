using System;
using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace FireExt
{
	// Token: 0x0200000E RID: 14
	[StaticConstructorOnStartup]
	internal static class MultiplayerSupport
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002F24 File Offset: 0x00001124
		static MultiplayerSupport()
		{
			bool flag = !MP.enabled;
			if (!flag)
			{
				MP.RegisterSyncMethod(typeof(FireFoamBelt), "DoFFoam", null);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F65 File Offset: 0x00001165
		private static void FixRNG(MethodInfo method)
		{
			MultiplayerSupport.harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPre", null), new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPos", null), null, null);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002FA0 File Offset: 0x000011A0
		private static void FixRNGPre()
		{
			Rand.PushState(Find.TickManager.TicksAbs);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002FB2 File Offset: 0x000011B2
		private static void FixRNGPos()
		{
			Rand.PopState();
		}

		// Token: 0x0400000B RID: 11
		private static Harmony harmony = new Harmony("rimworld.pelador.ffoambelt.multiplayersupport");
	}
}
