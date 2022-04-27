using System;
using Verse;
using RimWorld;

namespace ESCP_GiveAbilties
{
    class CompUseEffect_GiveHediff : CompUseEffect_Artifact
	{
		new public CompProperties_UseEffectGiveHediff Props
		{
			get
			{
				return (CompProperties_UseEffectGiveHediff)this.props;
			}
		}

		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
			if (usedBy.health.hediffSet.GetFirstHediffOfDef(Props.hediff) == null)
			{
				usedBy.health.AddHediff(Props.hediff);
			}
		}

		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (p.health.hediffSet.GetFirstHediffOfDef(Props.hediff) != null)
			{
				failReason = "ESCP_Tools_Ability_AlreadyHasHediff".Translate(p.Name, this.parent.Label);
				return false;
			}
            if (Props.requiredTrait != null && !p.story.traits.allTraits.Any(x=>x.def == Props.requiredTrait))
            {
				failReason = "ESCP_Tools_Ability_HasNotTrait".Translate(p.Name, Props.requiredTrait.label);
				return false;
			}
			if (Props.requiredHediff != null && !p.health.hediffSet.HasHediff(Props.requiredHediff))
			{
				failReason = "ESCP_Tools_Ability_HasNotHediff".Translate(p.Name, Props.requiredHediff.label);
				return false;
			}
			return base.CanBeUsedBy(p, out failReason);
		}
	}
}
