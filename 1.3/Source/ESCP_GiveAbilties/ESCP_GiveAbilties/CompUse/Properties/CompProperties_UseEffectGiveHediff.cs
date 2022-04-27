using System;
using Verse;
using RimWorld;

namespace ESCP_GiveAbilties
{
    class CompProperties_UseEffectGiveHediff : CompProperties_UseEffectArtifact
    {
        public CompProperties_UseEffectGiveHediff()
        {
            this.compClass = typeof(CompUseEffect_GiveHediff);
        }

        public HediffDef hediff;
        public TraitDef requiredTrait;
        public HediffDef requiredHediff;
    }
}
