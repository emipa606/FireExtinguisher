using System;
using RimWorld;
using Verse;
using Verse.Sound;

namespace FireExt
{
	// Token: 0x02000004 RID: 4
	public class CompCeilingPopper : ThingComp
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000220D File Offset: 0x0000040D
		public CompProperties_CeilingPopper Props
		{
			get
			{
				return (CompProperties_CeilingPopper)this.props;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000221C File Offset: 0x0000041C
		public override void CompTick()
		{
			bool flag = this.parent.Spawned && this.parent.IsHashIntervalTick(100);
			if (flag)
			{
				bool flag2 = !this.parent.Position.Roofed(this.parent.Map);
				if (flag2)
				{
					SoundInfo SInfo = SoundInfo.InMap(this.parent, MaintenanceType.None);
					SoundDefOf.Roof_Collapse.PlayOneShot(SInfo);
					FilthMaker.TryMakeFilth(this.parent.Position, this.parent.Map, ThingDefOf.Filth_RubbleBuilding, 1, FilthSourceFlags.None);
					this.parent.Destroy(DestroyMode.Vanish);
				}
			}
		}
	}
}
