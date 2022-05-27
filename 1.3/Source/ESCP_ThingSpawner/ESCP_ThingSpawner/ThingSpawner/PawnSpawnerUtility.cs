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
        public static void PawnSpawner(CreateAtProperties props, Thing target, Thing spawner, Map map, bool genSpawn = false)
        {
            Pawn newPawn = PawnGenerator.GeneratePawn(props.kindToSpawn, props.spawnAsPlayerFaction ? Faction.OfPlayer : props.faction != null && FactionUtility.DefaultFactionFrom(props.faction) != null ? FactionUtility.DefaultFactionFrom(props.faction) : null);
            if (genSpawn)
            {
                GenSpawn.Spawn(newPawn, spawner.Position, map);

            }
            else
            {
                PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, spawner);

            }
            if (props.soundDef != null)
            {
                SoundDef sound = props.soundDef;
                sound.PlayOneShot(new TargetInfo(spawner.Position, map, false));
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
            if(props.message != "")
            {
                Messages.Message(props.message, newPawn, props.messageTypeDef ?? MessageTypeDefOf.NegativeEvent, true);
            }
        }
    }
}
