using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FireExt
{
	// Token: 0x02000002 RID: 2
	[HarmonyPatch(typeof(Building_TurretGun), "TryFindNewTarget")]
	public class BTG_TryFindNewTarget_PrePatch
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[HarmonyPrefix]
		[HarmonyPriority(800)]
		public static bool PreFix(ref Building_TurretGun __instance, ref LocalTargetInfo __result)
		{
			bool flag = __instance.def.defName == "Turret_MiniTurret_FE";
			bool result;
			if (flag)
			{
				Verb AttackVerb = __instance.AttackVerb;
				float range = AttackVerb.verbProps.range;
				__result = FETargetUtility.GetFETarget(__instance, range);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
