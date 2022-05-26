using Verse;

namespace ESCP_Misc
{
    class BuildingProperties : DefModExtension
    {

        public bool preventDuplicatePlacement = false;

        public static BuildingProperties Get(Def def)
        {
            return def.GetModExtension<BuildingProperties>();
        }
    }
}
