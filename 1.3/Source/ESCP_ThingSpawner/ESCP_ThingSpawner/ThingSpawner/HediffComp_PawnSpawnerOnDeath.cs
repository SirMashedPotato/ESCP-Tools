using System;
using Verse;
using RimWorld;

namespace ESCP_ThingSpawner
{
    class HediffComp_PawnSpawnerOnDeath : HediffComp
    {
		public HediffCompProperties_PawnSpawnerOnDeath Props
		{
			get
			{
				return (HediffCompProperties_PawnSpawnerOnDeath)this.props;
			}
		}


        public override void Notify_PawnKilled()
        {
            var props = CreateAtProperties.Get(this.Def);
            if (props != null && props.kindToSpawn != null)
            {
                int target = props.numberToSpawn;
                for (int i = 0; i < target; i++)
                {
                    PawnSpawnerUtility.PawnSpawner(props, null, base.parent.pawn);
                }
            }

            base.Notify_PawnKilled();
        }
    }
}
