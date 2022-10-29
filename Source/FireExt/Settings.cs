using System;
using UnityEngine;
using Verse;

namespace FireExt;

public class Settings : ModSettings
{
    public float BurstValue = 3f;

    public float CleanDmgResist = 0.25f;

    public double CoolDownValue = 2.0;

    public int DamTickPeriod = 240;

    public int DryOutTime = 5;

    public double RadiusValue = 2.5;

    public float RangeValue = 12f;

    public float SpeedValue = 30f;

    public bool UseCleanFoam;

    public double WarmUpValue = 1.5;

    public void DoWindowContents(Rect canvas)
    {
        var listing_Standard = new Listing_Standard { ColumnWidth = canvas.width };
        listing_Standard.Begin(canvas);
        listing_Standard.Gap();
        listing_Standard.Label("FExt.WarmUpValue".Translate() + "  " + WarmUpValue);
        WarmUpValue = Math.Round(listing_Standard.Slider((float)WarmUpValue, 1f, 3f), 2);
        listing_Standard.Gap();
        listing_Standard.Label("FExt.CoolDownValue".Translate() + "  " + CoolDownValue);
        CoolDownValue = Math.Round(listing_Standard.Slider((float)CoolDownValue, 1f, 3f), 2);
        listing_Standard.Gap();
        checked
        {
            listing_Standard.Label("FExt.RangeValue".Translate() + "  " + (int)RangeValue);
            RangeValue = listing_Standard.Slider(RangeValue, 3f, 16f);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.BurstValue".Translate() + "  " + (int)BurstValue);
            BurstValue = listing_Standard.Slider(BurstValue, 1f, 4f);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.SpeedValue".Translate() + "  " + (int)SpeedValue);
            SpeedValue = listing_Standard.Slider(SpeedValue, 25f, 35f);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.RadiusValue".Translate() + "  " + RadiusValue);
            RadiusValue = Math.Round(listing_Standard.Slider((float)RadiusValue, 1f, 4f), 2);
            listing_Standard.Gap();
            listing_Standard.CheckboxLabeled("FExt.UseCleanFoam".Translate(), ref UseCleanFoam);
            listing_Standard.Gap();
            listing_Standard.Label("FExt.DryOutTime".Translate() + "  " + DryOutTime);
            DryOutTime = (int)listing_Standard.Slider(DryOutTime, 3f, 8f);
            listing_Standard.Gap();
            Text.Font = GameFont.Tiny;
            listing_Standard.Label("          " + "FExt.GeneralValueTip".Translate());
            Text.Font = GameFont.Small;
            if (Controller.currentVersion != null)
            {
                listing_Standard.Gap();
                GUI.contentColor = Color.gray;
                listing_Standard.Label("FExt.CurrentModVersion".Translate(Controller.currentVersion));
                GUI.contentColor = Color.white;
            }

            listing_Standard.End();
        }
    }

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