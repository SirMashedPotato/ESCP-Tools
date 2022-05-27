using RimWorld;
using Verse;
using System;
using System.Collections.Generic;

namespace ESCP_ThingSpawner
{
    class Comp_PawnSpawnerOnDestroy : ThingComp
    {
		public CompProperties_PawnSpawnerOnDestroy Props
		{
			get
			{
				return (CompProperties_PawnSpawnerOnDestroy)this.props;
			}
		}

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if (takingAgeDamage || (Props.notToxicFallout && previousMap.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout)))
            {
                base.PostDestroy(mode, previousMap);
                return;
            }
            var props = CreateAtProperties.Get(parent.def);
            if (props != null && props.kindToSpawn != null && Rand.Chance(props.chance))
            {
                int target = props.numberToSpawn;
                for (int i = 0; i < target; i++)
                {
                    PawnSpawnerUtility.PawnSpawner(props, null, parent, previousMap, true);
                }
            }

            base.PostDestroy(mode, previousMap);
        }

        /* ==========[Used to check if the thing is taking specific damag types]========== */

        public bool takingAgeDamage = false;

        public override void PostPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            takingAgeDamage = (dinfo.Def == DamageDefOf.Rotting && Props.notRot) || (dinfo.Def == DamageDefOf.Deterioration && Props.notDeterioration) || (dinfo.Def == DamageDefOf.Flame && Props.notFlame);
            base.PostPostApplyDamage(dinfo, totalDamageDealt);
        }
        /*
        public override void PostPreApplyDamage(DamageInfo dinfo, out bool absorbed)
        {
            takingAgeDamage = dinfo.Def == DamageDefOf.Rotting || dinfo.Def == DamageDefOf.Deterioration || (dinfo.Def == DamageDefOf.Flame);
            base.PostPreApplyDamage(dinfo, out absorbed);
        }
        */
    }
}
