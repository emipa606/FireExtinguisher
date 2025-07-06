using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace FireExt;

public class FETargetUtility
{
    internal static Thing GetFeTarget(Building_TurretGun btg, float range)
    {
        var candidates = new List<Thing>();
        var list = GenRadial.RadialDistinctThingsAround(btg.Position, btg.Map, range, false).ToList();
        if (list.Count <= 0)
        {
            return null;
        }

        foreach (var thing in list)
        {
            if (thing.def != ThingDefOf.Fire && !thing.IsBurning() ||
                !GenSight.LineOfSight(btg.Position, thing.Position, btg.Map, true))
            {
                continue;
            }

            if (thing.def != ThingDefOf.Fire)
            {
                if (thing.Faction != null)
                {
                    if (!thing.Faction.HostileTo(btg.Faction))
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