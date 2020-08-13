using System;
using UnityEngine;
using Verse;

namespace FireExt
{
	// Token: 0x02000010 RID: 16
	public class Settings : ModSettings
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003108 File Offset: 0x00001308
		public void DoWindowContents(Rect canvas)
		{
			Listing_Standard listing_Standard = new Listing_Standard();
			listing_Standard.ColumnWidth = canvas.width;
			listing_Standard.Begin(canvas);
			listing_Standard.Gap(12f);
			listing_Standard.Label("FExt.WarmUpValue".Translate() + "  " + this.WarmUpValue, -1f, null);
			this.WarmUpValue = Math.Round((double)listing_Standard.Slider((float)this.WarmUpValue, 1f, 3f), 2);
			listing_Standard.Gap(12f);
			listing_Standard.Label("FExt.CoolDownValue".Translate() + "  " + this.CoolDownValue, -1f, null);
			this.CoolDownValue = Math.Round((double)listing_Standard.Slider((float)this.CoolDownValue, 1f, 3f), 2);
			listing_Standard.Gap(12f);
			checked
			{
				listing_Standard.Label("FExt.RangeValue".Translate() + "  " + (int)this.RangeValue, -1f, null);
				this.RangeValue = listing_Standard.Slider(this.RangeValue, 3f, 16f);
				listing_Standard.Gap(12f);
				listing_Standard.Label("FExt.BurstValue".Translate() + "  " + (int)this.BurstValue, -1f, null);
				this.BurstValue = listing_Standard.Slider(this.BurstValue, 1f, 4f);
				listing_Standard.Gap(12f);
				listing_Standard.Label("FExt.SpeedValue".Translate() + "  " + (int)this.SpeedValue, -1f, null);
				this.SpeedValue = listing_Standard.Slider(this.SpeedValue, 25f, 35f);
				listing_Standard.Gap(12f);
				listing_Standard.Label("FExt.RadiusValue".Translate() + "  " + this.RadiusValue, -1f, null);
				this.RadiusValue = Math.Round((double)listing_Standard.Slider((float)this.RadiusValue, 1f, 4f), 2);
				listing_Standard.Gap(12f);
				listing_Standard.CheckboxLabeled("FExt.UseCleanFoam".Translate(), ref this.UseCleanFoam, null);
				listing_Standard.Gap(12f);
				listing_Standard.Label("FExt.DryOutTime".Translate() + "  " + this.DryOutTime, -1f, null);
				this.DryOutTime = (int)listing_Standard.Slider((float)this.DryOutTime, 3f, 8f);
				listing_Standard.Gap(12f);
				Text.Font = GameFont.Tiny;
				listing_Standard.Label("          " + "FExt.GeneralValueTip".Translate(), -1f, null);
				Text.Font = GameFont.Small;
				listing_Standard.Gap(12f);
				listing_Standard.End();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003460 File Offset: 0x00001660
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<double>(ref this.CoolDownValue, "CoolDownValue", 2.0, false);
			Scribe_Values.Look<double>(ref this.WarmUpValue, "WarmUpValue", 1.5, false);
			Scribe_Values.Look<float>(ref this.RangeValue, "RangeValue", 12f, false);
			Scribe_Values.Look<float>(ref this.BurstValue, "BurstValue", 3f, false);
			Scribe_Values.Look<float>(ref this.SpeedValue, "SpeedValue", 30f, false);
			Scribe_Values.Look<double>(ref this.RadiusValue, "RadiusValue", 2.5, false);
			Scribe_Values.Look<bool>(ref this.UseCleanFoam, "UseCleanFoam", false, false);
			Scribe_Values.Look<int>(ref this.DryOutTime, "DryOutTime", 5, false);
			Scribe_Values.Look<int>(ref this.DamTickPeriod, "DamTickPeriod", 240, false);
			Scribe_Values.Look<float>(ref this.CleanDmgResist, "CleanDmgResist", 0.5f, false);
		}

		// Token: 0x0400000C RID: 12
		public double WarmUpValue = 1.5;

		// Token: 0x0400000D RID: 13
		public double CoolDownValue = 2.0;

		// Token: 0x0400000E RID: 14
		public float RangeValue = 12f;

		// Token: 0x0400000F RID: 15
		public float BurstValue = 3f;

		// Token: 0x04000010 RID: 16
		public float SpeedValue = 30f;

		// Token: 0x04000011 RID: 17
		public double RadiusValue = 2.5;

		// Token: 0x04000012 RID: 18
		public bool UseCleanFoam = false;

		// Token: 0x04000013 RID: 19
		public int DryOutTime = 5;

		// Token: 0x04000014 RID: 20
		public int DamTickPeriod = 240;

		// Token: 0x04000015 RID: 21
		public float CleanDmgResist = 0.25f;
	}
}
