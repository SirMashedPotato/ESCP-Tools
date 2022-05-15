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

        public override string CompInspectStringExtra()
        {

            if (this.parent.GetComp<CompRefuelable>() != null)
            {
                var c = this.parent.GetComp<CompRefuelable>();
                return c.HasFuel && fuelUsed != null ? "ESCP_Tools_FuelExtension_CurrentFuel".Translate(fuelUsed.label) : null;
            }
            return null;
        }
    }
}
