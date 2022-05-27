using System;
using Verse;
using RimWorld;

namespace ESCP_ThingSpawner
{
    class DeathActionWorker_PawnSpawner : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            var props = CreateAtProperties.Get(corpse.InnerPawn.def);
            if (props != null && props.kindToSpawn != null && Rand.Chance(props.chance))
            {
                int target = props.numberToSpawn;
                for (int i = 0; i < target; i++)
                {
                    PawnSpawnerUtility.PawnSpawner(props, null, corpse, corpse.Map);
                }
            }
        }
    }
}
