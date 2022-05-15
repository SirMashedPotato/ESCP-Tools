using System;
using Verse;
using RimWorld;

namespace ESCP_FuelExtension
{
    public class CompProperties_FuelRate : CompProperties
    {
        public CompProperties_FuelRate()
        {
            this.compClass = typeof(CompFuelRate);
        }
        public float rate = 1f;//default value just in case
    }
}
