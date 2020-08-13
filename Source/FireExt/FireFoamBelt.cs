using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FireExt
{
	// Token: 0x0200000C RID: 12
	public class FireFoamBelt : Apparel
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002978 File Offset: 0x00000B78
		public override IEnumerable<Gizmo> GetWornGizmos()
		{
			Pawn wearer = base.Wearer;
			bool flag2 = base.Wearer != null;
			if (flag2)
			{
				bool flag = Find.Selector.SingleSelectedThing == base.Wearer;
				bool flag3 = flag;
				if (flag3)
				{
					string text = "FFoamBelt.FoamUses".Translate(this.def.label.CapitalizeFirst(), this.FoamUses.ToString());
					string desc = "FFoamBelt.FoamDesc".Translate(this.def.label.CapitalizeFirst());
					yield return new Command_Action
					{
						defaultLabel = text,
						defaultDesc = desc,
						icon = this.def.uiIcon,
						action = delegate()
						{
							this.DoFFoam(base.Wearer, this);
						}
					};
					text = null;
					desc = null;
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002988 File Offset: 0x00000B88
		public void DoFFoam(Pawn p, Thing t)
		{
			List<FloatMenuOption> list = new List<FloatMenuOption>();
			string text = "FFoamBelt.DoNothing".Translate();
			list.Add(new FloatMenuOption(text, delegate()
			{
				this.FFoamBeltUse(p, t, false);
			}, MenuOptionPriority.Default, null, null, 29f, null, null));
			text = "FFoamBelt.Activate".Translate(this.def.label.CapitalizeFirst());
			list.Add(new FloatMenuOption(text, delegate()
			{
				this.FFoamBeltUse(p, t, true);
			}, MenuOptionPriority.Default, null, null, 29f, (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, this.def), null));
			Find.WindowStack.Add(new FloatMenu(list));
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A50 File Offset: 0x00000C50
		public CompFFoamBelt StimWornComp
		{
			get
			{
				return base.GetComp<CompFFoamBelt>();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A68 File Offset: 0x00000C68
		public override void PostMake()
		{
			base.PostMake();
			this.FoamUses = base.GetComp<CompFFoamBelt>().Props.FoamUses;
			this.FoamRadius = base.GetComp<CompFFoamBelt>().Props.FoamRadius;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A9E File Offset: 0x00000C9E
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.FoamUses, "FoamUses", 1, false);
			Scribe_Values.Look<float>(ref this.FoamRadius, "FoamRadius", 5f, false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void FFoamBeltUse(Pawn p, Thing t, bool Using)
		{
			bool flag = Using && p != null;
			if (flag)
			{
				bool flag2 = this.def.defName == "FireFoam_Belt";
				if (flag2)
				{
					string Reason;
					bool Passed;
					FireFoamBelt.ChkFFoamBelt(p, t, out Reason, out Passed);
					bool flag3 = Passed;
					if (flag3)
					{
						FireFoamBelt.DoFFoamBeltPop(p, this);
					}
					else
					{
						string Msg = "FFoamBelt.ReasonPrefix".Translate(this.def.label.CapitalizeFirst()) + Reason;
						Messages.Message(Msg, p, MessageTypeDefOf.NeutralEvent, false);
					}
				}
				else
				{
					string ErrMsg = string.Concat(new string[]
					{
						"ERR: FFoam belt item def not found for ",
						this.def.label.CapitalizeFirst(),
						" : (",
						this.def.defName,
						")"
					});
					Log.Message(ErrMsg, false);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static void ChkFFoamBelt(Pawn p, Thing t, out string Reason, out bool Passed)
		{
			Reason = null;
			bool flag = ((p != null) ? p.Map : null) != null;
			if (flag)
			{
				bool close = false;
				List<Thing> fires = p.Map.listerThings.ThingsOfDef(ThingDefOf.Fire);
				bool flag2 = fires.Count > 0;
				if (flag2)
				{
					foreach (Thing fire in fires)
					{
						bool flag3 = p.Position.InHorDistOf(fire.Position, (t as FireFoamBelt).FoamRadius);
						if (flag3)
						{
							close = true;
							break;
						}
					}
				}
				bool flag4 = !close;
				if (flag4)
				{
					Passed = false;
					Reason = "FFoamBelt.NoCloseFire".Translate((p != null) ? p.LabelShort.CapitalizeFirst() : null);
				}
				else
				{
					Passed = true;
				}
			}
			else
			{
				Passed = false;
				Reason = "FFoamBelt.NoValidMap".Translate((p != null) ? p.LabelShort.CapitalizeFirst() : null);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002CEC File Offset: 0x00000EEC
		public override void Tick()
		{
			base.Tick();
			bool flag = base.Wearer != null;
			if (flag)
			{
				bool flag2 = base.Wearer.IsBurning();
				if (flag2)
				{
					FireFoamBelt.DoFFoamBeltPop(base.Wearer, this);
				}
			}
			else
			{
				bool flag3 = this.IsBurning();
				if (flag3)
				{
					FireFoamBelt.DoFFoamBeltPop(null, this);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D46 File Offset: 0x00000F46
		public override void TickRare()
		{
			base.TickRare();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D50 File Offset: 0x00000F50
		public static void DoFFoamBeltPop(Pawn p, Thing t)
		{
			Map map = null;
			IntVec3 origin = default(IntVec3);
			bool flag = p != null;
			if (flag)
			{
				bool flag2 = ((p != null) ? p.Map : null) != null;
				if (flag2)
				{
					map = p.Map;
					origin = p.Position;
				}
			}
			else
			{
				bool flag3 = ((t != null) ? t.Map : null) != null;
				if (flag3)
				{
					map = t.Map;
					origin = t.Position;
				}
			}
			bool flag4 = map != null;
			if (flag4)
			{
				float radius = (t as FireFoamBelt).FoamRadius;
				DamageDef DmgType = DefDatabase<DamageDef>.GetNamed("FExtExtinguish", false);
				bool flag5 = DmgType == null;
				if (flag5)
				{
					DmgType = DamageDefOf.Extinguish;
				}
				SoundDef explosionSound = null;
				ThingDef postSpawnThingDef = DefDatabase<ThingDef>.GetNamed("Filth_FExtFireFoam", false);
				bool flag6 = postSpawnThingDef == null;
				if (flag6)
				{
					postSpawnThingDef = ThingDefOf.Filth_FireFoam;
				}
				GenExplosion.DoExplosion(origin, map, radius, DmgType, t, -1, -1f, explosionSound, null, null, null, postSpawnThingDef, 1f, 3, false, null, 0f, 1, 0f, false, null, null);
			}
			bool flag7 = p != null;
			if (flag7)
			{
				Fire fire = (Fire)p.GetAttachment(ThingDefOf.Fire);
				bool flag8 = fire != null;
				if (flag8)
				{
					fire.Destroy(DestroyMode.Vanish);
				}
			}
			(t as FireFoamBelt).FoamUses--;
			bool flag9 = (t as FireFoamBelt).FoamUses < 1;
			if (flag9)
			{
				bool flag10 = !t.DestroyedOrNull();
				if (flag10)
				{
					t.Destroy(DestroyMode.Vanish);
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private int FoamUses = 1;

		// Token: 0x0400000A RID: 10
		private float FoamRadius = 5f;
	}
}
