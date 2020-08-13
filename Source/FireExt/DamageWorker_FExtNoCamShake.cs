using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt
{
	// Token: 0x02000009 RID: 9
	public class DamageWorker_FExtNoCamShake : DamageWorker
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002380 File Offset: 0x00000580
		public override void ExplosionStart(Explosion explosion, List<IntVec3> cellsToAffect)
		{
			MoteMaker.ThrowSmoke(explosion.Position.ToVector3(), explosion.Map, 1f);
			this.ExplosionVisualEffectCenter(explosion);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
		public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
		{
			DamageWorker.DamageResult result = new DamageWorker.DamageResult();
			Fire fire = victim as Fire;
			bool flag = fire == null || fire.Destroyed;
			DamageWorker.DamageResult result2;
			if (flag)
			{
				result2 = result;
			}
			else
			{
				base.Apply(dinfo, victim);
				fire.fireSize -= dinfo.Amount;
				bool flag2 = fire.fireSize <= 0.1f;
				if (flag2)
				{
					fire.Destroy(DestroyMode.Vanish);
				}
				result2 = result;
			}
			return result2;
		}

		// Token: 0x04000004 RID: 4
		private const float DamageAmountToFireSizeRatio = 0.1f;
	}
}
