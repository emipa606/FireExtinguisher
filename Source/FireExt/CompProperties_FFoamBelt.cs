using Verse;

namespace FireExt;

public class CompProperties_FFoamBelt : CompProperties
{
    public readonly float FoamRadius = 5f;

    public readonly int FoamUses = 1;

    public CompProperties_FFoamBelt()
    {
        compClass = typeof(CompFFoamBelt);
    }
}