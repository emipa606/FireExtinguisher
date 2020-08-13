using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt
{
	// Token: 0x0200000A RID: 10
	public class FExtFoam : Filth
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002433 File Offset: 0x00000633
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.FFspawnTick, "FFspawnTick", 0, false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002450 File Offset: 0x00000650
		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			this.FFspawnTick = Find.TickManager.TicksGame;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000246C File Offset: 0x0000066C
		public void Refill()
		{
			this.FFspawnTick = Find.TickManager.TicksGame;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002480 File Offset: 0x00000680
		public override void Tick()
		{
			bool flag = this.FFspawnTick + Controller.Settings.DryOutTime * 60 * 60 < Find.TickManager.TicksGame;
			if (flag)
			{
				this.Destroy(DestroyMode.Vanish);
			}
			else
			{
				bool flag2 = Find.TickManager.TicksGame % Controller.Settings.DamTickPeriod == 0;
				if (flag2)
				{
					Map TargetMap = base.Map;
					IntVec3 TargetCell = base.Position;
					List<Thing> Filthlist = TargetCell.GetThingList(TargetMap);
					bool flag3 = Filthlist.Count > 0;
					if (flag3)
					{
						for (int i = 0; i < Filthlist.Count; i++)
						{
							bool flag4 = Filthlist[i] is Filth;
							if (flag4)
							{
								IntVec3 filthCell = Filthlist[i].Position;
								bool flag5 = filthCell == TargetCell;
								if (flag5)
								{
									bool flag6 = Filthlist[i].def.defName != "Filth_FExtFireFoam";
									if (flag6)
									{
										float CleaningAmount = Filthlist[i].def.filth.cleaningWorkToReduceThickness * (Controller.Settings.CleanDmgResist * (float)Controller.Settings.DamTickPeriod);
										bool flag7 = (float)(Find.TickManager.TicksGame - this.FFspawnTick) > CleaningAmount;
										if (flag7)
										{
											this.DoFFCleaning(Filthlist[i]);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025FD File Offset: 0x000007FD
		public override void ThickenFilth()
		{
			base.ThickenFilth();
			this.Refill();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002610 File Offset: 0x00000810
		private void DoFFCleaning(Thing targ)
		{
			Filth filth = targ as Filth;
			bool flag = filth != null;
			if (flag)
			{
				filth.ThinFilth();
			}
		}

		// Token: 0x04000005 RID: 5
		private int FFspawnTick;
	}
}
