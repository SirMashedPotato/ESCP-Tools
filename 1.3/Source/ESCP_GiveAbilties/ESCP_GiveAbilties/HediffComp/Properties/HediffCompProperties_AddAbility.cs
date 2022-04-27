using System;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace ESCP_GiveAbilties
{
    class HediffCompProperties_AddAbility : HediffCompProperties
    {
        public HediffCompProperties_AddAbility()
        {
            this.compClass = typeof(HediffComp_AddAbility);
        }

        public AbilityDef abilityDef;
        public List<AbilityDef> abilityDefList;
        public float severityRequired = 0f;
        public bool removeHediffOnApply = false;
        public bool removeAbilitiesOnRemove = false;
        public bool displayMessage = true;
    }
}
