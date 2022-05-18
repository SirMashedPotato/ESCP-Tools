using Verse;

namespace ESCP_BoneChitinFat
{
    class ESCP_BoneChitinFat_ModSettings : ModSettings
    {
        public bool ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart = ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart_def;
        public float ESCP_Tools_BoneBoneFat_BoneMultiplier = ESCP_Tools_BoneBoneFat_BoneMultiplier_def;
        public float ESCP_Tools_BoneChitinFat_ChitinMultiplier = ESCP_Tools_BoneChitinFat_ChitinMultiplier_def;
        public float ESCP_Tools_BoneFatFat_FatMultiplier = ESCP_Tools_BoneFatFat_FatMultiplier_def;

        //defaults
        private static readonly bool ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart_def = false;
        private static readonly float ESCP_Tools_BoneBoneFat_BoneMultiplier_def = 1f;
        private static readonly float ESCP_Tools_BoneChitinFat_ChitinMultiplier_def = 1f;
        private static readonly float ESCP_Tools_BoneFatFat_FatMultiplier_def = 1f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart, "ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart", ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart_def);
            Scribe_Values.Look(ref ESCP_Tools_BoneBoneFat_BoneMultiplier, "ESCP_Tools_BoneBoneFat_BoneMultiplier", ESCP_Tools_BoneBoneFat_BoneMultiplier_def);
            Scribe_Values.Look(ref ESCP_Tools_BoneChitinFat_ChitinMultiplier, "ESCP_Tools_BoneChitinFat_ChitinMultiplier", ESCP_Tools_BoneChitinFat_ChitinMultiplier_def);
            Scribe_Values.Look(ref ESCP_Tools_BoneFatFat_FatMultiplier, "ESCP_Tools_BoneFatFat_FatMultiplier", ESCP_Tools_BoneFatFat_FatMultiplier_def);;
            base.ExposeData();
        }

        public static void ResetSettings(ESCP_BoneChitinFat_ModSettings settings)
        {
            settings.ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart = ESCP_Tools_BoneChitinFat_EnableLoggingOnGameStart_def;
            settings.ESCP_Tools_BoneBoneFat_BoneMultiplier = ESCP_Tools_BoneBoneFat_BoneMultiplier_def;
            settings.ESCP_Tools_BoneChitinFat_ChitinMultiplier = ESCP_Tools_BoneChitinFat_ChitinMultiplier_def;
            settings.ESCP_Tools_BoneFatFat_FatMultiplier = ESCP_Tools_BoneFatFat_FatMultiplier_def;
        }
    }
}
