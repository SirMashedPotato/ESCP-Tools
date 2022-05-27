using Verse;
using RimWorld;
using System.Collections.Generic;

namespace ESCP_ThingSpawner
{
    public class CreateAtProperties : DefModExtension
    {
        public PawnKindDef kindToSpawn = null;
        public bool spawnAsPlayerFaction = true;
        public FactionDef faction;
        public MentalStateDef mentalStateDef;
        public bool attackTarget = true;
        public int numberToSpawn = 1;
        public SoundDef soundDef;
        public RecordDef recordDef;
        public float chance = 1f;
        public string message = "";
        public MessageTypeDef messageTypeDef;

        public static CreateAtProperties Get(Def def)
        {
            return def.GetModExtension<CreateAtProperties>();
        }
    }
}
