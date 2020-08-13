using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace FireExt
{
	// Token: 0x02000003 RID: 3
	public class FETargetUtility
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		internal static Thing GetFETarget(Building_TurretGun BTG, float range)
		{
			List<Thing> candidates = new List<Thing>();
			List<Thing> list = GenRadial.RadialDistinctThingsAround(BTG.Position, BTG.Map, range, false).ToList<Thing>();
			bool flag = list.Count > 0;
			if (flag)
			{
				foreach (Thing thing in list)
				{
					bool flag2 = (thing.def == ThingDefOf.Fire || thing.IsBurning()) && GenSight.LineOfSight(BTG.Position, thing.Position, BTG.Map, true, null, 0, 0);
					if (flag2)
					{
						bool flag3 = thing.def != ThingDefOf.Fire;
						if (flag3)
						{
							bool flag4 = thing.Faction != null;
							if (flag4)
							{
								bool flag5 = !thing.Faction.HostileTo(BTG.Faction);
								if (flag5)
								{
									candidates.AddDistinct(thing);
								}
							}
							else
							{
								candidates.AddDistinct(thing);
							}
						}
						else
						{
							candidates.AddDistinct(thing);
						}
					}
				}
				bool flag6 = candidates.Count > 0;
				if (flag6)
				{
					return candidates.RandomElement<Thing>();
				}
			}
			return null;
		}
	}
}
