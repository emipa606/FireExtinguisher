using Verse;

namespace FireExt;

public class CompProperties_FFoamBelt : CompProperties
{
    public float FoamRadius = 5f;

    public int FoamUses = 1;

    public CompProperties_FFoamBelt()
    {
        compClass = typeof(CompFFoamBelt);
    }
}