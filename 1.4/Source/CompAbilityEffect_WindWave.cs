using RimWorld;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_WindWave : CompProperties_AbilityEffect
    {

        public CompProperties_WindWave()
        {
            compClass = typeof(CompAbilityEffect_WindWave);
        }
    }

    public class CompAbilityEffect_WindWave : CompAbilityEffect
    {
        public new CompProperties_WindWave Props => (CompProperties_WindWave)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            GameCondition_Ascendancy.Instance.lastTimeWindWaveApplied = Find.TickManager.TicksGame;
            GameCondition_Ascendancy.Instance.windWaveAppliedCount++;

            var conditions = parent.pawn.Map.GameConditionManager.ActiveConditions.Concat(Find.World.GameConditionManager.ActiveConditions).ToList();
            foreach (var condition in conditions)
            {
                if (condition.def == GameConditionDefOf.VolcanicWinter || condition.def == GameConditionDefOf.ToxicFallout)
                {
                    condition.End();
                }
            }
            var cells = parent.pawn.Map.AllCells;
            foreach (var cell in cells)
            {
                cell.Unpollute(parent.pawn.Map);
            }
        }
    }
}
