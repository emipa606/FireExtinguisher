using RimWorld;
using Verse;

namespace FireExt
{
    // Token: 0x0200000A RID: 10
    public class FExtFoam : Filth
    {
        // Token: 0x04000005 RID: 5
        private int FFspawnTick;

        // Token: 0x06000012 RID: 18 RVA: 0x00002433 File Offset: 0x00000633
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref FFspawnTick, "FFspawnTick");
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002450 File Offset: 0x00000650
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            FFspawnTick = Find.TickManager.TicksGame;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x0000246C File Offset: 0x0000066C
        private void Refill()
        {
            FFspawnTick = Find.TickManager.TicksGame;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002480 File Offset: 0x00000680
        public override void Tick()
        {
            if (FFspawnTick + (Controller.Settings.DryOutTime * 60 * 60) < Find.TickManager.TicksGame)
            {
                Destroy();
            }
            else
            {
                if (Find.TickManager.TicksGame % Controller.Settings.DamTickPeriod != 0)
                {
                    return;
                }

                var TargetMap = Map;
                var TargetCell = Position;
                var Filthlist = TargetCell.GetThingList(TargetMap);
                if (Filthlist.Count <= 0)
                {
                    return;
                }

                foreach (var thing in Filthlist)
                {
                    if (thing is not Filth)
                    {
                        continue;
                    }

                    var filthCell = thing.Position;
                    if (filthCell != TargetCell)
                    {
                        continue;
                    }

                    if (thing.def.defName == "Filth_FExtFireFoam")
                    {
                        continue;
                    }

                    var CleaningAmount = thing.def.filth.cleaningWorkToReduceThickness *
                                         (Controller.Settings.CleanDmgResist * Controller.Settings.DamTickPeriod);
                    if (Find.TickManager.TicksGame - FFspawnTick > CleaningAmount)
                    {
                        DoFFCleaning(thing);
                    }
                }
            }
        }

        // Token: 0x06000016 RID: 22 RVA: 0x000025FD File Offset: 0x000007FD
        public override void ThickenFilth()
        {
            base.ThickenFilth();
            Refill();
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002610 File Offset: 0x00000810
        private void DoFFCleaning(Thing targ)
        {
            var filth = targ as Filth;
            var flag = filth != null;
            if (flag)
            {
                filth.ThinFilth();
            }
        }
    }
}