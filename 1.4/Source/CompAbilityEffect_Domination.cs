using RimWorld;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Domination : CompProperties_AbilityEffect
    {
        public CompProperties_Domination()
        {
            compClass = typeof(CompAbilityEffect_Domination);
        }
    }

    public class CompAbilityEffect_Domination : CompAbilityEffect
    {
        public new CompProperties_Domination Props => (CompProperties_Domination)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var pawn = target.Pawn;
            if (pawn.Faction != parent.pawn.Faction)
            {
                pawn.SetFaction(parent.pawn.Faction, parent.pawn);
            }
            if (pawn.Ideo != parent.pawn.Ideo)
            {
                pawn.ideo.SetIdeo(parent.pawn.Ideo);
            }
            pawn.ideo.Certainty = 1f;
        }
    }



}
