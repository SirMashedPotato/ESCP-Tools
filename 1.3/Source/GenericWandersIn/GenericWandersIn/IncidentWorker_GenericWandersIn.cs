using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection.Emit;
using System.Linq;


namespace GenericWandersIn
{
	class IncidentWorker_GenericWandersIn : IncidentWorker
	{
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			var props = IncidentProperties.Get(this.def);
			if (!base.CanFireNowSub(parms) && props != null && props.kindDef != null)
			{
				return false;
			}
			Map map = (Map)parms.target;
			IntVec3 intVec;
			return !map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(props.kindDef.race) && this.TryFindEntryCell(map, out intVec);
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			var props = IncidentProperties.Get(this.def);
			Map map = (Map)parms.target;
			IntVec3 intVec;
			if (!this.TryFindEntryCell(map, out intVec))
			{
				return false;
			}
			int num = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) / props.kindDef.combatPower);
			int max = props.max.RandomInRange;
			num = Mathf.Clamp(num, 1, max);
			int num2 = Rand.RangeInclusive(90000, 150000);
			IntVec3 invalid = IntVec3.Invalid;
			if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid))
			{
				invalid = IntVec3.Invalid;
			}
			Pawn pawn = null;
			for (int i = 0; i < num; i++)
			{
				IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
				pawn = PawnGenerator.GeneratePawn(props.kindDef, null);
				GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
				if (props.leaveMapAfterTime)
				{
					pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
				}
				if (invalid.IsValid)
				{
					pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
				}
			}
			Find.LetterStack.ReceiveLetter(this.def.letterLabel.Formatted(props.kindDef.label).CapitalizeFirst(), this.def.letterText.Formatted(num, props.kindDef.label), this.def.letterDef, pawn, null, null);
			return true;
		}

		private bool TryFindEntryCell(Map map, out IntVec3 cell)
		{
			return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null);
		}
	}
}

