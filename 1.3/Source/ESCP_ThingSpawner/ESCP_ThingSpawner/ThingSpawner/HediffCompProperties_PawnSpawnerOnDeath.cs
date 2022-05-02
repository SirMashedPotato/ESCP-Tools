using System;
using Verse;

namespace ESCP_ThingSpawner
{
    class HediffCompProperties_PawnSpawnerOnDeath : HediffCompProperties
    {
        public HediffCompProperties_PawnSpawnerOnDeath()
        {
            this.compClass = typeof(HediffComp_PawnSpawnerOnDeath);
        }
    }
}
