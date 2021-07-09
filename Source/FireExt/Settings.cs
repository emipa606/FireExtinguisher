using System;
using UnityEngine;
using Verse;

namespace FireExt
{
    // Token: 0x02000010 RID: 16
    public class Settings : ModSettings
    {
        // Token: 0x0400000F RID: 15
        public float BurstValue = 3f;

        // Token: 0x04000015 RID: 21
        public float CleanDmgResist = 0.25f;

        // Token: 0x0400000D RID: 13
        public double CoolDownValue = 2.0;

        // Token: 0x04000014 RID: 20
        public int DamTickPeriod = 240;

        // Token: 0x04000013 RID: 19
        public int DryOutTime = 5;

        // Token: 0x04000011 RID: 17
        public double RadiusValue = 2.5;

        // Token: 0x0400000E RID: 14
        public float RangeValue = 12f;

        // Token: 0x04000010 RID: 16
        public float SpeedValue = 30f;

        // Token: 0x04000012 RID: 18
        public bool UseCleanFoam;

        // Token: 0x0400000C RID: 12
        public double WarmUpValue = 1.5;

        // Token: 0x0600002F RID: 47 RVA: 0x00003108 File Offset: 0x00001308
        public void DoWindowContents(Rect canvas)
        {
            var listing_Standard = new Listing_Standard {ColumnWidth = canvas.width};
            listing_Standard.Begin(canvas);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.WarmUpValue".Translate() + "  " + WarmUpValue);
            WarmUpValue = Math.Round(listing_Standard.Slider((float) WarmUpValue, 1f, 3f), 2);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.CoolDownValue".Translate() + "  " + CoolDownValue);
            CoolDownValue = Math.Round(listing_Standard.Slider((float) CoolDownValue, 1f, 3f), 2);
            listing_Standard.Gap();
            checked
            {
                listing_Standard.Label("FExt.RangeValue".Translate() + "  " + (int) RangeValue);
                RangeValue = listing_Standard.Slider(RangeValue, 3f, 16f);
                listing_Standard.Gap();
                listing_Standard.Label("FExt.BurstValue".Translate() + "  " + (int) BurstValue);
                BurstValue = listing_Standard.Slider(BurstValue, 1f, 4f);
                listing_Standard.Gap();
                listing_Standard.Label("FExt.SpeedValue".Translate() + "  " + (int) SpeedValue);
                SpeedValue = listing_Standard.Slider(SpeedValue, 25f, 35f);
                listing_Standard.Gap();
                listing_Standard.Label("FExt.RadiusValue".Translate() + "  " + RadiusValue);
                RadiusValue = Math.Round(listing_Standard.Slider((float) RadiusValue, 1f, 4f), 2);
                listing_Standard.Gap();
                listing_Standard.CheckboxLabeled("FExt.UseCleanFoam".Translate(), ref UseCleanFoam);
                listing_Standard.Gap();
                listing_Standard.Label("FExt.DryOutTime".Translate() + "  " + DryOutTime);
                DryOutTime = (int) listing_Standard.Slider(DryOutTime, 3f, 8f);
                listing_Standard.Gap();
                Text.Font = GameFont.Tiny;
                listing_Standard.Label("          " + "FExt.GeneralValueTip".Translate());
                Text.Font = GameFont.Small;
                listing_Standard.Gap();
                listing_Standard.End();
            }
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00003460 File Offset: 0x00001660
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref CoolDownValue, "CoolDownValue", 2.0);
            Scribe_Values.Look(ref WarmUpValue, "WarmUpValue", 1.5);
            Scribe_Values.Look(ref RangeValue, "RangeValue", 12f);
            Scribe_Values.Look(ref BurstValue, "BurstValue", 3f);
            Scribe_Values.Look(ref SpeedValue, "SpeedValue", 30f);
            Scribe_Values.Look(ref RadiusValue, "RadiusValue", 2.5);
            Scribe_Values.Look(ref UseCleanFoam, "UseCleanFoam");
            Scribe_Values.Look(ref DryOutTime, "DryOutTime", 5);
            Scribe_Values.Look(ref DamTickPeriod, "DamTickPeriod", 240);
            Scribe_Values.Look(ref CleanDmgResist, "CleanDmgResist", 0.5f);
        }
    }
}