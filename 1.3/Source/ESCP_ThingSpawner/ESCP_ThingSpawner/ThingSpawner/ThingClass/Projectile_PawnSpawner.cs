using Verse;

namespace ESCP_ThingSpawner
{
    public class Projectile_ThingSpawner : Projectile_Explosive
    {

        protected override void Impact(Thing hitThing)
        {
            var props = CreateAtProperties.Get(this.def);
            if (props != null && props.kindToSpawn != null && Rand.Chance(props.chance))
            {
                int target = props.numberToSpawn;
                for (int i = 0; i < target; i++)
                {
                    PawnSpawnerUtility.PawnSpawner(props, this.intendedTarget.Thing, this, this.Map);
                }
                if (props.recordDef != null && this.launcher is Pawn p)
                {
                    p.records.Increment(props.recordDef);
                }

            }
            this.landed = true;
            this.Destroy(DestroyMode.Vanish);
        }
    }
}
