using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace FireExt;

public class FETargetUtility
{
    internal static Thing GetFETarget(Building_TurretGun BTG, float range)
    {
        var candidates = new List<Thing>();
        var list = GenRadial.RadialDistinctThingsAround(BTG.Position, BTG.Map, range, false).ToList();
        if (list.Count <= 0)
        {
            return null;
        }

        foreach (var thing in list)
        {
            if (thing.def != ThingDefOf.Fire && !thing.IsBurning() ||
                !GenSight.LineOfSight(BTG.Position, thing.Position, BTG.Map, true))
            {
                continue;
            }

            if (thing.def != ThingDefOf.Fire)
            {
                if (thing.Faction != null)
                {
                    if (!thing.Faction.HostileTo(BTG.Faction))
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

        return candidates.Count > 0 ? candidates.RandomElement() : null;
    }
}