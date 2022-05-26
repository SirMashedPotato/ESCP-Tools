using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace ESCP_Misc
{
    /*
     * Gifted to us by the great SirLalaPyon
     */

    [HarmonyPatch(typeof(GenConstruct), "CanPlaceBlueprintAt")]
    public static class GenConstruct_CanPlaceBlueprintOver_Patch
    {
        public static void Postfix(ref AcceptanceReport __result, BuildableDef entDef, IntVec3 center, Rot4 rot, Map map, bool godMode = false, Thing thingToIgnore = null, Thing thing = null, ThingDef stuffDef = null)
        {
            if (BuildingProperties.Get(entDef) != null && BuildingProperties.Get(entDef).preventDuplicatePlacement)
            {
                foreach (IntVec3 item in GenAdj.OccupiedRect(center, rot, entDef.Size))
                {
                    List<Thing> thingList = item.GetThingList(map);
                    for (int i = 0; i < thingList.Count; i++)
                    {
                        Thing thing2 = thingList[i];
                        if (BuildingProperties.Get(thing2.def) != null && BuildingProperties.Get(thing2.def).preventDuplicatePlacement)
                        {
                            __result = new AcceptanceReport("SpaceAlreadyOccupied".Translate());
                            return;
                        }
                    }
                }
            }
        }
    }
}
