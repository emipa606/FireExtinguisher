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

    private void refill()
    {
        FFspawnTick = Find.TickManager.TicksGame;
    }

    protected override void Tick()
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

            var targetMap = Map;
            var targetCell = Position;
            var filthList = targetCell.GetThingList(targetMap);
            if (filthList.Count <= 0)
            {
                return;
            }

            foreach (var thing in filthList)
            {
                if (thing is not Filth)
                {
                    continue;
                }

                var filthCell = thing.Position;
                if (filthCell != targetCell)
                {
                    continue;
                }

                if (thing.def.defName == "Filth_FExtFireFoam")
                {
                    continue;
                }

                var cleaningAmount = thing.def.filth.cleaningWorkToReduceThickness *
                                     (Controller.Settings.CleanDmgResist * Controller.Settings.DamTickPeriod);
                if (Find.TickManager.TicksGame - FFspawnTick > cleaningAmount)
                {
                    doFfCleaning(thing);
                }
            }
        }
    }

    public override void ThickenFilth()
    {
        base.ThickenFilth();
        refill();
    }

    private static void doFfCleaning(Thing targ)
    {
        if (targ is Filth filth)
        {
            filth.ThinFilth();
        }
    }
}