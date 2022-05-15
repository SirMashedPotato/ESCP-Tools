using System;
using Verse;
using RimWorld;

namespace ESCP_FuelExtension
{
    public class CompFuelRate : ThingComp
    {
        public CompProperties_FuelRate Props
        {
            get
            {
                return (CompProperties_FuelRate)this.props;
            }
        }
    }
}
