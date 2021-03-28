using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace FireExt
{
    // Token: 0x02000003 RID: 3
    public class FETargetUtility
    {
        // Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
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

            if (candidates.Count > 0)
            {
                return candidates.RandomElement();
            }

            return null;
        }
    }
}