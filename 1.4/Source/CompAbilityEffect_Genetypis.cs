using RimWorld;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Genetypis : CompProperties_AbilityEffect
    {
        public CompProperties_Genetypis()
        {
            compClass = typeof(CompAbilityEffect_Genetypis);
        }
    }

    public class CompAbilityEffect_Genetypis : CompAbilityEffect
    {
        public new CompProperties_Genetypis Props => (CompProperties_Genetypis)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var pawn = target.Pawn;
            bool xenotype = Rand.Bool;
            var randomGene = DefDatabase<GeneDef>.AllDefsListForReading.RandomElement();
            pawn.genes.AddGene(randomGene, xenotype);
        }
    }



}
