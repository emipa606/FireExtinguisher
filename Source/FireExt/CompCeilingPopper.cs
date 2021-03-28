using RimWorld;
using Verse;
using Verse.Sound;

namespace FireExt
{
    // Token: 0x02000004 RID: 4
    public class CompCeilingPopper : ThingComp
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000005 RID: 5 RVA: 0x0000220D File Offset: 0x0000040D
        public CompProperties_CeilingPopper Props => (CompProperties_CeilingPopper) props;

        // Token: 0x06000006 RID: 6 RVA: 0x0000221C File Offset: 0x0000041C
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
}