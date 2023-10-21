using RimWorld;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Weave : CompProperties_AbilityEffect
    {
        public CompProperties_Weave()
        {
            compClass = typeof(CompAbilityEffect_Weave);
        }
    }

    public class CompAbilityEffect_Weave : CompAbilityEffect
    {
        public new CompProperties_Weave Props => (CompProperties_Weave)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var pawn = target.Pawn;
            while (true)
            {
                var missingParts = pawn.health.hediffSet.hediffs.OfType<Hediff_MissingPart>().ToList();
                if (missingParts.TryRandomElement( out var missingPart))
                {
                    pawn.health.RestorePart(missingPart.Part);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
