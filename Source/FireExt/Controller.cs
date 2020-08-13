using System;
using UnityEngine;
using Verse;

namespace FireExt
{
	// Token: 0x02000008 RID: 8
	public class Controller : Mod
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002338 File Offset: 0x00000538
		public override string SettingsCategory()
		{
			return "FExt.Name".Translate();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002359 File Offset: 0x00000559
		public override void DoSettingsWindowContents(Rect canvas)
		{
			Controller.Settings.DoWindowContents(canvas);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002368 File Offset: 0x00000568
		public Controller(ModContentPack content) : base(content)
		{
			Controller.Settings = base.GetSettings<Settings>();
		}

		// Token: 0x04000003 RID: 3
		public static Settings Settings;
	}
}
