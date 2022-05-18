using UnityEngine;
using Verse;
using System;

namespace ESCP_BoneChitinFat
{
    class ESCP_BoneChitinFat_Mod : Mod
    {

        ESCP_BoneChitinFat_ModSettings settings;

        public ESCP_BoneChitinFat_Mod(ModContentPack contentPack) : base(contentPack)
        {
            this.settings = GetSettings<ESCP_BoneChitinFat_ModSettings>();
        }

        public override string SettingsCategory() => "ESCP_Tools_BoneChitinFat_ModName".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Tools_BoneBoneFat_BoneMultiplier".Translate() + " (" + settings.ESCP_Tools_BoneBoneFat_BoneMultiplier * 100 + "%)");
            settings.ESCP_Tools_BoneBoneFat_BoneMultiplier = (float)Math.Round(listing_Standard.Slider(settings.ESCP_Tools_BoneBoneFat_BoneMultiplier, 0f, 5f) * 20) / 20;
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Tools_BoneChitinFat_ChitinMultiplier".Translate() + " (" + settings.ESCP_Tools_BoneChitinFat_ChitinMultiplier * 100 + "%)");
            settings.ESCP_Tools_BoneChitinFat_ChitinMultiplier = (float)Math.Round(listing_Standard.Slider(settings.ESCP_Tools_BoneChitinFat_ChitinMultiplier, 0f, 5f) * 20) / 20;
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Tools_BoneFatFat_FatMultiplier".Translate() + " (" + settings.ESCP_Tools_BoneFatFat_FatMultiplier * 100 + "%)");
            settings.ESCP_Tools_BoneFatFat_FatMultiplier = (float)Math.Round(listing_Standard.Slider(settings.ESCP_Tools_BoneFatFat_FatMultiplier, 0f, 5f) * 20) / 20;
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart".Translate(), ref settings.ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart);
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "ESCP_Reset".Translate());
            if (Widgets.ButtonText(rectDefault, "ESCP_Reset".Translate(), true, true, true))
            {
                ESCP_BoneChitinFat_ModSettings.ResetSettings(settings);
            }
            listing_Standard.Gap();

            listing_Standard.GapLine();
            listing_Standard.Gap();

            listing_Standard.End();
            base.DoSettingsWindowContents(inRect);
        }

        /* getters */

        public static bool ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart()
        {
            return LoadedModManager.GetMod<ESCP_BoneChitinFat_Mod>().GetSettings<ESCP_BoneChitinFat_ModSettings>().ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart;
        }

        public static float ESCP_Tools_BoneBoneFat_BoneMultiplier()
        {
            return LoadedModManager.GetMod<ESCP_BoneChitinFat_Mod>().GetSettings<ESCP_BoneChitinFat_ModSettings>().ESCP_Tools_BoneBoneFat_BoneMultiplier;
        }

        public static float ESCP_Tools_BoneChitinFat_ChitinMultiplier()
        {
            return LoadedModManager.GetMod<ESCP_BoneChitinFat_Mod>().GetSettings<ESCP_BoneChitinFat_ModSettings>().ESCP_Tools_BoneChitinFat_ChitinMultiplier;
        }

        public static float ESCP_Tools_BoneFatFat_FatMultiplier()
        {
            return LoadedModManager.GetMod<ESCP_BoneChitinFat_Mod>().GetSettings<ESCP_BoneChitinFat_ModSettings>().ESCP_Tools_BoneFatFat_FatMultiplier;
        }
    }
}
