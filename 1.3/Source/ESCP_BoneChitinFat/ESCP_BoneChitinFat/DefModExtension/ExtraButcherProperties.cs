using Verse;
using RimWorld;

namespace ESCP_BoneChitinFat
{
    class ExtraButcherProperties : DefModExtension
    {

        public bool hasBone = true;

        public bool hasChitin = false;

        public bool hasFat = true;

        public static ExtraButcherProperties Get(Def def)
        {
            return def.GetModExtension<ExtraButcherProperties>();
        }
    }
}
