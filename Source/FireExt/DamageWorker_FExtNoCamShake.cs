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
        DamageResult result2;
        if (victim is not Fire fire || fire.Destroyed)
        {
            result2 = result;
        }
        else
        {
            base.Apply(dinfo, victim);
            fire.fireSize -= dinfo.Amount;
            if (fire.fireSize <= 0.1f)
            {
                fire.Destroy();
            }

            result2 = result;
        }

        return result2;
    }
}