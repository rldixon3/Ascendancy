using RimWorld;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Erasure : CompProperties_AbilityEffect
    {
        public CompProperties_Erasure()
        {
            compClass = typeof(CompAbilityEffect_Erasure);
        }
    }

    public class CompAbilityEffect_Erasure : CompAbilityEffect
    {
        public new CompProperties_Erasure Props => (CompProperties_Erasure)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var pawn = target.Pawn;
            var negativeMemories = pawn.needs.mood.thoughts.memories.memories.Where(x => x.MoodOffset() < 0).ToList();
            foreach (var m in negativeMemories )
            {
                pawn.needs.mood.thoughts.memories.RemoveMemory(m);
            }
        }
    }
}
