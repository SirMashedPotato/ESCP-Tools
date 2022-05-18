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
            foreach (IntVec3 item in GenAdj.OccupiedRect(center, rot, entDef.Size))
            {
                List<Thing> thingList = item.GetThingList(map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Thing thing2 = thingList[i];
                    if (thing2 != thingToIgnore && (thing2.def.building != null || thing2.def.entityDefToBuild != null) && ((entDef is ThingDef thingDef && thingDef.building != null && !thingDef.building.isEdifice && !thing2.def.building.isEdifice) || (thing2.def.IsBlueprint && thing2.def.entityDefToBuild != null && thing2.def.entityDefToBuild is ThingDef thingDef2 && thingDef2.building != null && !thingDef2.building.isEdifice)))
                    {
                        __result = new AcceptanceReport("SpaceAlreadyOccupied".Translate());
                        return;
                    }
                }
            }
        }
    }
}
