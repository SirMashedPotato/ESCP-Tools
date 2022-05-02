using System;
using Verse;
using Verse.Sound;
using RimWorld;
using Verse.AI;
using System.Collections.Generic;

namespace ESCP_ThingSpawner
{
    public static class PawnSpawnerUtility
    {
        public static void PawnSpawner(CreateAtProperties props, Thing target, Thing spawner)
        {
            Pawn newPawn = PawnGenerator.GeneratePawn(props.kindToSpawn, props.spawnAsPlayerFaction ? Faction.OfPlayer : null);
            PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, spawner);
            if (props.soundDef != null)
            {
                SoundDef sound = props.soundDef;
                sound.PlayOneShot(new TargetInfo(spawner.Position, spawner.Map, false));
            }
            if (props.attackTarget)
            {
                if (target != null)
                {
                    Job job = new Job(RimWorld.JobDefOf.AttackMelee, target);
                    newPawn.jobs.StartJob(job);
                }
            }
            if (props.mentalStateDef != null)
            {
                newPawn.mindState.mentalStateHandler.TryStartMentalState(props.mentalStateDef);
            }
        }
    }
}
