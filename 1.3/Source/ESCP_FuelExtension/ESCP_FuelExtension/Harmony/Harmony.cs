using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using System.Reflection;

namespace ESCP_FuelExtension
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ESCP_FuelExtension");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(CompRefuelable), nameof(CompRefuelable.Refuel), new Type[] { typeof(List<Thing>) })]
    public class StoreFuel_Patch
    {
        [HarmonyPrefix]
        public static bool StoreFuel(CompRefuelable __instance, List<Thing> fuelThings)
        {
            if (fuelThings == null || __instance.parent.GetComp<CompStoreFuelThing>() == null)
            {
                return true;//null check just in case
            }
            Thing fuelThing = fuelThings.Last();//no specific reason to use .last, just felt like it
            ThingDef fuelThingDef = fuelThing.def;//gotta need the thingdef since the thing is destroyed in the process
            __instance.parent.GetComp<CompStoreFuelThing>().fuelUsed = fuelThingDef;//saves the thingdef
            return true;//always returning true since there's no need to prevent vanilla code nor other prefixes
        }
    }

    [HarmonyPatch(typeof(CompRefuelable), "ConsumptionRatePerTick", MethodType.Getter)]
    public class FuelRate_Patch
    {
        [HarmonyPostfix]
        public static void FuelRate(CompRefuelable __instance, ref float __result)
        {
            float multiplier = 1f;//default value just in case
            if (__instance.parent.GetComp<CompStoreFuelThing>() != null && __instance.HasFuel)
            {
                ThingDef fuelUsed = __instance.parent.GetComp<CompStoreFuelThing>().fuelUsed;//gets fuel
                if (fuelUsed.GetCompProperties<CompProperties_FuelRate>() != null)//null check in case the fuel doesn't have a comp for its rate
                {
                    multiplier = fuelUsed.GetCompProperties<CompProperties_FuelRate>().rate;//gets its rate
                }
            }
            //Log.Message("rate: " + 60000f * multiplier);//this is only for debugging, might wanna delete it after testing
            __result = __instance.Props.fuelConsumptionRate / (60000f * multiplier);//vanilla tickrate is 60000f
        }
    }

    [HarmonyPatch(typeof(CompRefuelable), nameof(CompRefuelable.CompInspectStringExtra))]
    public class InspecStringPatch
    {
        [HarmonyPrefix]
        public static bool InspectString(CompRefuelable __instance, ref string __result, ref float ___fuel)
        {
            if (__instance.parent.GetComp<CompStoreFuelThing>() != null && !__instance.Props.consumeFuelOnlyWhenUsed && __instance.HasFuel)
            {
                float multiplier = 1f;
                ThingDef fuelUsed = __instance.parent.GetComp<CompStoreFuelThing>().fuelUsed;//gets fuel
                if (fuelUsed.GetCompProperties<CompProperties_FuelRate>() != null)//null check in case the fuel doesn't have a comp for its rate
                {
                    multiplier = fuelUsed.GetCompProperties<CompProperties_FuelRate>().rate;//gets its rate
                }
                if (fuelUsed.GetCompProperties<CompProperties_FuelRate>() != null)//null check in case the fuel doesn't have a comp for its rate
                {
                    //Basically just the vanilla stuff, but modified to allow for the multiplier
                    string text = string.Concat(new string[]
            {
                __instance.Props.FuelLabel,
                ": ",
                ___fuel.ToStringDecimalIfSmall(),
                " / ",
                __instance.Props.fuelCapacity.ToStringDecimalIfSmall()
            });
                    if (!__instance.Props.consumeFuelOnlyWhenUsed && __instance.HasFuel)
                    {
                        int numTicks = (int)(___fuel / __instance.Props.fuelConsumptionRate * 60000f * multiplier);
                        text = text + " (" + numTicks.ToStringTicksToPeriod(true, false, true, true) + ")";
                    }
                    if (!__instance.HasFuel && !__instance.Props.outOfFuelMessage.NullOrEmpty())
                    {
                        text += string.Format("\n{0} ({1}x {2})", __instance.Props.outOfFuelMessage, __instance.GetFuelCountToFullyRefuel(), __instance.Props.fuelFilter.AnyAllowedDef.label);
                    }
                    if (__instance.Props.targetFuelLevelConfigurable)
                    {
                        text += "\n" + "ConfiguredTargetFuelLevel".Translate(__instance.TargetFuelLevel.ToStringDecimalIfSmall());
                    }
                    __result =  text;

                    return false;
                }
            }
            return true;
        }
    }
}
