using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt;

public class DamageWorker_FExtNoCamShake : DamageWorker
{
    private const float DamageAmountToFireSizeRatio = 0.1f;

    public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
    {
        FleckMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
        ExplosionVisualEffectCenter(explosion);
    }

    public override DamageResult Apply(DamageInfo dinfo, Thing victim)
    {
        var result = new DamageResult();
        if (victim is not Fire { Destroyed: false } fire)
        {
            return result;
        }

        base.Apply(dinfo, victim);
        fire.fireSize -= dinfo.Amount;
        if (fire.fireSize <= DamageAmountToFireSizeRatio)
        {
            fire.Destroy();
        }

        return result;
    }
}