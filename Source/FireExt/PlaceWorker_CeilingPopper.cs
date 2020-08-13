using System;
using System.Collections.Generic;
using Verse;

namespace FireExt
{
	// Token: 0x0200000F RID: 15
	public class PlaceWorker_CeilingPopper : PlaceWorker
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002FBC File Offset: 0x000011BC
		public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
		{
			bool flag = !loc.InBounds(map);
			AcceptanceReport result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !map.roofGrid.Roofed(loc);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = loc.Filled(map);
					if (flag3)
					{
						result = false;
					}
					else
					{
						List<Thing> list = loc.GetThingList(map);
						bool flag4 = list.Count > 0;
						if (flag4)
						{
							foreach (Thing thingy in list)
							{
								bool flag5 = thingy is Building;
								if (flag5)
								{
									ThingDef def = thingy.def;
									bool flag6 = ((def != null) ? def.entityDefToBuild : null) != null;
									if (flag6)
									{
										bool flag7 = thingy.def.entityDefToBuild == checkingDef;
										if (flag7)
										{
											return false;
										}
									}
									bool isDoor = thingy.def.IsDoor;
									if (isDoor)
									{
										return false;
									}
								}
							}
						}
						result = true;
					}
				}
			}
			return result;
		}
	}
}
