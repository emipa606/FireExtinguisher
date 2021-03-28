using Verse;

namespace FireExt
{
    // Token: 0x02000007 RID: 7
    public class CompProperties_FFoamBelt : CompProperties
    {
        // Token: 0x04000002 RID: 2
        public float FoamRadius = 5f;

        // Token: 0x04000001 RID: 1
        public int FoamUses = 1;

        // Token: 0x0600000B RID: 11 RVA: 0x0000230A File Offset: 0x0000050A
        public CompProperties_FFoamBelt()
        {
            compClass = typeof(CompFFoamBelt);
        }
    }
}