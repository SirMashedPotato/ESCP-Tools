using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using System.Reflection;
using System.Linq;

namespace ESCP_ThingCatching
{

    [StaticConstructorOnStartup]
    public static class Main
    {
        public static List<ESCP_ThingCatching.CatchableOutputDef> defs = new List<CatchableOutputDef> { };

        static Main()
        {
            defs = DefDatabase<ESCP_ThingCatching.CatchableOutputDef>.AllDefsListForReading;
        }
    }

    public class Recipe_CatchThing : RecipeWorker
    {

        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            if (Main.defs.NullOrEmpty())
            {
                Log.Error("No ESCP_BugCatching.CatchableOutputDef found!");
            }
            else
            {
                var recipeProps = RecipeProperties.Get(this.recipe);
                if (recipeProps == null)
                {
                    Log.Error("Recipe properties not found!");
                } 
                else
                {
                    List<ESCP_ThingCatching.CatchableOutputDef> validDefs = Main.defs.Where(x => 
                        (x.biomes.NullOrEmpty() || x.biomes.Contains(billDoer.Map.Biome.defName))
                        && (x.catchTags.NullOrEmpty() || recipeProps.catchTags.Where(y => x.catchTags.Contains(y)).Any())
                        ).ToList();

                    if (validDefs.NullOrEmpty())
                    {
                        Log.Error("No ESCP_BugCatching.CatchableOutputDef for this biome found!");
                    }
                    else
                    {
                        //getting the def
                        ESCP_ThingCatching.CatchableOutputDef output = validDefs.RandomElementByWeightWithFallback(x => x.chance * 100, Main.defs[0]);
                        /* Spawning items
                         * done this way if count is larger than stack size
                         */
                        if (output.outputThingDef != null)
                        {

                            float temp = (float)output.outputRange.RandomInRange * recipeProps.effeciencyMult;
                            int num = (int)temp;
                            int limit = output.outputThingDef.stackLimit;
                            while (num != 0)
                            {
                                Thing thing = ThingMaker.MakeThing(output.outputThingDef);
                                if (num >= limit)
                                {
                                    thing.stackCount = limit;
                                    num -= limit;
                                }
                                else
                                {
                                    thing.stackCount = num;
                                    num = 0;
                                }
                                GenPlace.TryPlaceThing(thing, billDoer.Position, billDoer.Map, ThingPlaceMode.Near, null, null, default(Rot4));
                                if (output.isSpecial)
                                {
                                    Messages.Message("ESCP_Tools_Catching_Special".Translate(billDoer.Name, thing.Label), thing, MessageTypeDefOf.PositiveEvent, false);
                                }
                            }
                        }
                        else
                        {
                            Messages.Message("ESCP_Tools_Catching_Failed".Translate(billDoer.Name), billDoer, MessageTypeDefOf.RejectInput, false);
                        }
                    }
                }
            }
              
            //do the vanilla shite
            base.Notify_IterationCompleted(billDoer, ingredients);
        }
    }
}
