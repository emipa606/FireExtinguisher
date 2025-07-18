﻿using HarmonyLib;
using Verse;

namespace FireExt;

[HarmonyPatch(typeof(ShotReport), nameof(ShotReport.HitFactorFromShooter), typeof(Thing), typeof(float),
    typeof(float?))]
public class ShotReport_HitFactorFromShooter
{
    [HarmonyPostfix]
    public static bool Prefix(Thing caster, ref float __result)
    {
        if (caster is not Pawn pawn || pawn.equipment?.PrimaryEq?.parent?.def?.defName != "Gun_Fire_Ext")
        {
            return true;
        }

        __result = 0;
        return false;
    }
}