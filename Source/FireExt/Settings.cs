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
        var listingStandard = new Listing_Standard { ColumnWidth = canvas.width };
        listingStandard.Begin(canvas);
        listingStandard.Gap();
        listingStandard.Label("FExt.WarmUpValue".Translate() + "  " + WarmUpValue);
        WarmUpValue = Math.Round(listingStandard.Slider((float)WarmUpValue, 1f, 3f), 2);
        listingStandard.Gap();
        listingStandard.Label("FExt.CoolDownValue".Translate() + "  " + CoolDownValue);
        CoolDownValue = Math.Round(listingStandard.Slider((float)CoolDownValue, 1f, 3f), 2);
        listingStandard.Gap();
        checked
        {
            listingStandard.Label("FExt.RangeValue".Translate() + "  " + (int)RangeValue);
            RangeValue = listingStandard.Slider(RangeValue, 3f, 16f);
            listingStandard.Gap();
            listingStandard.Label("FExt.BurstValue".Translate() + "  " + (int)BurstValue);
            BurstValue = listingStandard.Slider(BurstValue, 1f, 4f);
            listingStandard.Gap();
            listingStandard.Label("FExt.SpeedValue".Translate() + "  " + (int)SpeedValue);
            SpeedValue = listingStandard.Slider(SpeedValue, 25f, 35f);
            listingStandard.Gap();
            listingStandard.Label("FExt.RadiusValue".Translate() + "  " + RadiusValue);
            RadiusValue = Math.Round(listingStandard.Slider((float)RadiusValue, 1f, 4f), 2);
            listingStandard.Gap();
            listingStandard.CheckboxLabeled("FExt.UseCleanFoam".Translate(), ref UseCleanFoam);
            listingStandard.Gap();
            listingStandard.Label("FExt.DryOutTime".Translate() + "  " + DryOutTime);
            DryOutTime = (int)listingStandard.Slider(DryOutTime, 3f, 8f);
            listingStandard.Gap();
            Text.Font = GameFont.Tiny;
            listingStandard.Label("          " + "FExt.GeneralValueTip".Translate());
            Text.Font = GameFont.Small;
            if (Controller.CurrentVersion != null)
            {
                listingStandard.Gap();
                GUI.contentColor = Color.gray;
                listingStandard.Label("FExt.CurrentModVersion".Translate(Controller.CurrentVersion));
                GUI.contentColor = Color.white;
            }

            listingStandard.End();
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