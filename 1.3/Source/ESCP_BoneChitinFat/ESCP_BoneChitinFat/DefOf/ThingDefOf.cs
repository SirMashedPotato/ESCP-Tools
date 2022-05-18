using System;
using Verse;
using RimWorld;

namespace ESCP_BoneChitinFat
{
    [DefOf]
    public static class ThingDefOf
    {
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }

        public static ThingDef ESCP_Item_Bone;
        public static ThingDef ESCP_Item_Chitin;
        public static ThingDef ESCP_Item_Fat;

    }
}
