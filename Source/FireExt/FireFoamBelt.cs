using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FireExt
{
    // Token: 0x0200000C RID: 12
    public class FireFoamBelt : Apparel
    {
        // Token: 0x0400000A RID: 10
        private float FoamRadius = 5f;

        // Token: 0x04000009 RID: 9
        private int FoamUses = 1;

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600001E RID: 30 RVA: 0x00002A50 File Offset: 0x00000C50
        public CompFFoamBelt StimWornComp => GetComp<CompFFoamBelt>();

        // Token: 0x0600001C RID: 28 RVA: 0x00002978 File Offset: 0x00000B78
        public override IEnumerable<Gizmo> GetWornGizmos()
        {
            if (Wearer == null)
            {
                yield break;
            }

            if (Find.Selector.SingleSelectedThing != Wearer)
            {
                yield break;
            }

            string text = "FFoamBelt.FoamUses".Translate(def.label.CapitalizeFirst(), FoamUses.ToString());
            string desc = "FFoamBelt.FoamDesc".Translate(def.label.CapitalizeFirst());
            yield return new Command_Action
            {
                defaultLabel = text,
                defaultDesc = desc,
                icon = def.uiIcon,
                action = delegate { DoFFoam(Wearer, this); }
            };
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002988 File Offset: 0x00000B88
        private void DoFFoam(Pawn p, Thing t)
        {
            var list = new List<FloatMenuOption>();
            string text = "FFoamBelt.DoNothing".Translate();
            list.Add(new FloatMenuOption(text, delegate { FFoamBeltUse(p, t, false); }, MenuOptionPriority.Default,
                null, null, 29f));
            text = "FFoamBelt.Activate".Translate(def.label.CapitalizeFirst());
            list.Add(new FloatMenuOption(text, delegate { FFoamBeltUse(p, t, true); }, MenuOptionPriority.Default, null,
                null, 29f, rect => Widgets.InfoCardButton(rect.x + 5f, rect.y + ((rect.height - 24f) / 2f), def)));
            Find.WindowStack.Add(new FloatMenu(list));
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00002A68 File Offset: 0x00000C68
        public override void PostMake()
        {
            base.PostMake();
            FoamUses = GetComp<CompFFoamBelt>().Props.FoamUses;
            FoamRadius = GetComp<CompFFoamBelt>().Props.FoamRadius;
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002A9E File Offset: 0x00000C9E
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref FoamUses, "FoamUses", 1);
            Scribe_Values.Look(ref FoamRadius, "FoamRadius", 5f);
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002AD4 File Offset: 0x00000CD4
        private void FFoamBeltUse(Pawn p, Thing t, bool Using)
        {
            if (!Using || p == null)
            {
                return;
            }

            if (def.defName == "FireFoam_Belt")
            {
                ChkFFoamBelt(p, t, out var Reason, out var Passed);
                if (Passed)
                {
                    DoFFoamBeltPop(p, this);
                }
                else
                {
                    string Msg = "FFoamBelt.ReasonPrefix".Translate(def.label.CapitalizeFirst()) + Reason;
                    Messages.Message(Msg, p, MessageTypeDefOf.NeutralEvent, false);
                }
            }
            else
            {
                var ErrMsg = string.Concat("ERR: FFoam belt item def not found for ", def.label.CapitalizeFirst(),
                    " : (", def.defName, ")");
                Log.Message(ErrMsg);
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002BC4 File Offset: 0x00000DC4
        private static void ChkFFoamBelt(Pawn p, Thing t, out string Reason, out bool Passed)
        {
            Reason = null;
            if (p?.Map != null)
            {
                var close = false;
                var fires = p.Map.listerThings.ThingsOfDef(ThingDefOf.Fire);
                if (fires.Count > 0)
                {
                    foreach (var fire in fires)
                    {
                        if (!p.Position.InHorDistOf(fire.Position, ((FireFoamBelt) t).FoamRadius))
                        {
                            continue;
                        }

                        close = true;
                        break;
                    }
                }

                if (!close)
                {
                    Passed = false;
                    Reason = "FFoamBelt.NoCloseFire".Translate(p.LabelShort.CapitalizeFirst());
                }
                else
                {
                    Passed = true;
                }
            }
            else
            {
                Passed = false;
                Reason = "FFoamBelt.NoValidMap".Translate(p?.LabelShort.CapitalizeFirst());
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00002CEC File Offset: 0x00000EEC
        public override void Tick()
        {
            base.Tick();
            var flag = Wearer != null;
            if (flag)
            {
                var flag2 = Wearer.IsBurning();
                if (flag2)
                {
                    DoFFoamBeltPop(Wearer, this);
                }
            }
            else
            {
                var flag3 = this.IsBurning();
                if (flag3)
                {
                    DoFFoamBeltPop(null, this);
                }
            }
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00002D46 File Offset: 0x00000F46

        // Token: 0x06000025 RID: 37 RVA: 0x00002D50 File Offset: 0x00000F50
        private static void DoFFoamBeltPop(Pawn p, Thing t)
        {
            Map map = null;
            var origin = default(IntVec3);
            if (p != null)
            {
                if (p.Map != null)
                {
                    map = p.Map;
                    origin = p.Position;
                }
            }
            else
            {
                if (t?.Map != null)
                {
                    map = t.Map;
                    origin = t.Position;
                }
            }

            if (map != null)
            {
                var radius = ((FireFoamBelt) t).FoamRadius;
                var DmgType = DefDatabase<DamageDef>.GetNamed("FExtExtinguish", false);
                if (DmgType == null)
                {
                    DmgType = DamageDefOf.Extinguish;
                }

                var postSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FExtFireFoam", false);
                if (postSpawnThingDef == null)
                {
                    postSpawnThingDef = ThingDefOf.Filth_FireFoam;
                }

                GenExplosion.DoExplosion(origin, map, radius, DmgType, t, -1, -1f, null, null, null, null,
                    postSpawnThingDef, 1f, 3);
            }

            var fire = (Fire) p?.GetAttachment(ThingDefOf.Fire);
            fire?.Destroy();
            if (t == null)
            {
                return;
            }

            ((FireFoamBelt) t).FoamUses--;
            if (((FireFoamBelt) t).FoamUses >= 1)
            {
                return;
            }

            if (!t.DestroyedOrNull())
            {
                t.Destroy();
            }
        }
    }
}