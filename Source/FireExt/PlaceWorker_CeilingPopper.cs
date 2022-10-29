using Verse;

namespace FireExt;

public class PlaceWorker_CeilingPopper : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)
    {
        if (!loc.InBounds(map))
        {
            return false;
        }

        if (!map.roofGrid.Roofed(loc))
        {
            return false;
        }

        if (loc.Filled(map))
        {
            return false;
        }

        var list = loc.GetThingList(map);
        if (list.Count <= 0)
        {
            return true;
        }

        foreach (var thingy in list)
        {
            if (thingy is not Building)
            {
                continue;
            }

            var def = thingy.def;
            if (def?.entityDefToBuild != null)
            {
                if (thingy.def.entityDefToBuild == checkingDef)
                {
                    return false;
                }
            }

            var isDoor = thingy.def.IsDoor;
            if (isDoor)
            {
                return false;
            }
        }

        return true;
    }
}