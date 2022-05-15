using System;
using Verse;
using RimWorld;
using Verse.AI;

namespace ESCP_FuelExtension
{
    public static class Utility
    {
		public static Thing FindSpecificFuel(Pawn pawn, ThingDef fuel)
		{
			Predicate<Thing> validator = (Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x, 1, -1, null, false);
			return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(fuel), PathEndMode.ClosestTouch, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, validator, null, 0, -1, false, RegionType.Set_Passable, false);
		}
	}
}
