using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt
{
    // Token: 0x02000009 RID: 9
    public class DamageWorker_FExtNoCamShake : DamageWorker
    {
        // Token: 0x04000004 RID: 4
        private const float DamageAmountToFireSizeRatio = 0.1f;

        // Token: 0x0600000F RID: 15 RVA: 0x00002380 File Offset: 0x00000580
        public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
        {
            FleckMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
            ExplosionVisualEffectCenter(explosion);
        }

        // Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
        public override DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            var result = new DamageResult();
            DamageResult result2;
            if (!(victim is Fire fire) || fire.Destroyed)
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
}