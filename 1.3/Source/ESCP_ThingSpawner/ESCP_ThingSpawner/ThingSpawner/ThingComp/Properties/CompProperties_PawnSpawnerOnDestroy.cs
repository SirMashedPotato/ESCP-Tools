using System;
using Verse;

namespace ESCP_ThingSpawner
{
    class CompProperties_PawnSpawnerOnDestroy : CompProperties
    {
        public CompProperties_PawnSpawnerOnDestroy()
        {
            this.compClass = typeof(Comp_PawnSpawnerOnDestroy);
        }
        public bool notRot = true;
        public bool notFlame = true;
        public bool notDeterioration = true;
        public bool notToxicFallout = true;
    }
}
