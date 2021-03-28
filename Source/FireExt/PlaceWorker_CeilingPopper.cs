using Verse;

namespace FireExt
{
    // Token: 0x0200000F RID: 15
    public class PlaceWorker_CeilingPopper : PlaceWorker
    {
        // Token: 0x0600002D RID: 45 RVA: 0x00002FBC File Offset: 0x000011BC
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
            Thing thingToIgnore = null, Thing thing = null)
        {
            AcceptanceReport result;
            if (!loc.InBounds(map))
            {
                result = false;
            }
            else
            {
                if (!map.roofGrid.Roofed(loc))
                {
                    result = false;
                }
                else
                {
                    if (loc.Filled(map))
                    {
                        result = false;
                    }
                    else
                    {
                        var list = loc.GetThingList(map);
                        if (list.Count > 0)
                        {
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
                        }

                        result = true;
                    }
                }
            }

            return result;
        }
    }
}