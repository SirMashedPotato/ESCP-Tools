using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Reflection;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;
using Verse.AI;
using Verse.AI.Group;
using System.Text;
using UnityEngine;
using RimWorld.QuestGen;
using Verse.Grammar;

namespace ESCP_BoneChitinFat
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ESCP_BoneChitinFat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("ButcherProducts")]
    public static class Thing_ButcherProducts_Patch
    {
        [HarmonyPostfix]
        public static void StrangButcherPatch(ref Thing __instance, ref IEnumerable<Thing> __result)
        {
            if (__instance is Pawn && __instance.def.butcherProducts != null)
            {
                Pawn pawn = __instance as Pawn;
                bool flag = false;

                //get the age based divider

                int div = 1;
                switch (pawn.ageTracker.CurLifeStageIndex)
                {
                    case 0:
                        div = 3;
                        break;
                    case 1:
                        div = 2;
                        break;
                    default:
                        break;
                }

                //modify appropriate amounts

                List<Thing> newList = new List<Thing>();
                foreach (Thing thing in __result)
                {
                    var settings = LoadedModManager.GetMod<ESCP_BoneChitinFat_Mod>().GetSettings<ESCP_BoneChitinFat_ModSettings>();
                    if (thing.def == ThingDefOf.ESCP_Item_Bone || thing.def == ThingDefOf.ESCP_Item_Chitin || thing.def == ThingDefOf.ESCP_Item_Fat)
                    {
                        flag = true;
                        float mult = 1;
                        if(thing.def == ThingDefOf.ESCP_Item_Bone)
                        {
                            mult = settings.ESCP_Tools_BoneBoneFat_BoneMultiplier;
                        }
                        if (thing.def == ThingDefOf.ESCP_Item_Chitin)
                        {
                            div = 1;
                            mult = settings.ESCP_Tools_BoneChitinFat_ChitinMultiplier;
                        }
                        if (thing.def == ThingDefOf.ESCP_Item_Fat)
                        {
                            mult = settings.ESCP_Tools_BoneFatFat_FatMultiplier;
                        }
                        float count = thing.stackCount * mult;
                        count /= div;
                        if (count >= 1)
                        {
                            Thing newThing = ThingMaker.MakeThing(thing.def, null);
                            newThing.stackCount = (int)count;
                            newList.Insert(newList.Count, newThing);
                        }
                    }
                    else
                    {
                        newList.Insert(newList.Count, thing);
                    }

                }
                if (flag)
                {
                    __result = newList;
                }
            }
        }
    }
}
