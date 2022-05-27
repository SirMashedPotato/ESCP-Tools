using Verse;
using RimWorld;

namespace ESCP_ThingSpawner
{
    public class Building_Trap_PawnSpawner : Building_Trap
    {
        protected override void SpringSub(Pawn p)
        {
            var props = CreateAtProperties.Get(this.def);
            if (props != null && props.kindToSpawn != null && Rand.Chance(props.chance))
            {
                int target = props.numberToSpawn;
                for (int i = 0; i < target; i++)
                {
                    PawnSpawnerUtility.PawnSpawner(props, p, this, this.Map);
                }
            }
            this.Destroy();
        }
    }
}
