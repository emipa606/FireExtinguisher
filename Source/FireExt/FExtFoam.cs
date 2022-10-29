using RimWorld;
using Verse;

namespace FireExt;

public class FExtFoam : Filth
{
    private int FFspawnTick;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref FFspawnTick, "FFspawnTick");
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        FFspawnTick = Find.TickManager.TicksGame;
    }

    private void Refill()
    {
        FFspawnTick = Find.TickManager.TicksGame;
    }

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

    public override void ThickenFilth()
    {
        base.ThickenFilth();
        Refill();
    }

    private void DoFFCleaning(Thing targ)
    {
        if (targ is Filth filth)
        {
            filth.ThinFilth();
        }
    }
}