using System;
using Verse;
using RimWorld;

namespace ESCP_FuelExtension
{
    public class CompStoreFuelThing : ThingComp
    {
        public ThingDef fuelUsed;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref this.fuelUsed, "fuelUsed");
        }
    }
}
