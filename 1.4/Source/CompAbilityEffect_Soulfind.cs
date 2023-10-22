using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Soulfind : CompProperties_EffectWithDest
    {
        public CompProperties_Soulfind()
        {
            compClass = typeof(CompAbilityEffect_Soulfind);
        }
    }

    public class CompAbilityEffect_Soulfind : CompAbilityEffect_WithDest
    {
        public override TargetingParameters targetParams => new TargetingParameters
        {
            canTargetCorpses = true,
            canTargetItems = true,
            mapObjectTargetsMustBeAutoAttackable = false,
        };

        public new CompProperties_Soulfind Props => (CompProperties_Soulfind)props;

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (target.Thing is not Pawn)
            {
                if (throwMessages)
                {
                    Messages.Message("AR.TargetPawnToSacrifice".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }
            return base.Valid(target, throwMessages);
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            var corpse = target.Thing as Corpse;
            if (corpse is null || corpse.InnerPawn?.RaceProps.Humanlike is false || corpse.GetRotStage() == RotStage.Dessicated)
            {
                if (showMessages)
                {
                    Messages.Message("AR.TargetHumanCorpseToResurrect".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }
            return base.ValidateTarget(target, showMessages);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var corpse = dest.Thing as Corpse;
            ResurrectionUtility.Resurrect(corpse.InnerPawn);
            target.Pawn.Kill(null);
        }
    }
}
