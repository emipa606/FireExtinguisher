using RimWorld;
using Verse;
using Verse.Sound;

namespace FireExt;

public class CompCeilingPopper : ThingComp
{
    public CompProperties_CeilingPopper Props => (CompProperties_CeilingPopper)props;

    public override void CompTick()
    {
        if (!(parent.Spawned && parent.IsHashIntervalTick(100)))
        {
            return;
        }


        if (parent.Position.Roofed(parent.Map))
        {
            return;
        }

        var SInfo = SoundInfo.InMap(parent);
        SoundDefOf.Roof_Collapse.PlayOneShot(SInfo);
        FilthMaker.TryMakeFilth(parent.Position, parent.Map, ThingDefOf.Filth_RubbleBuilding);
        parent.Destroy();
    }
}