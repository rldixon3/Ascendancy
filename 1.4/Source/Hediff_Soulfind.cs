using RimWorld;
using Verse;

namespace Ascendancy
{
    public class Hediff_Soulfind : HediffWithComps
    {
        public override void PostRemoved()
        {
            base.PostRemoved();
            var hediff = HediffMaker.MakeHediff(AR_DefOf.PsychicComa, pawn);
            hediff.TryGetComp<HediffComp_Disappears>().ticksToDisappear = GenDate.TicksPerDay * 5;
            pawn.health.AddHediff(hediff);
        }
    }
}
