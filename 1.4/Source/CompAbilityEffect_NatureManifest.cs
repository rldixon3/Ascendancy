using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_NatureManifest : CompProperties_AbilityEffect
    {
        public CompProperties_NatureManifest()
        {
            compClass = typeof(CompAbilityEffect_NatureManifest);
        }
    }

    public class CompAbilityEffect_NatureManifest : CompAbilityEffect
    {
        public new CompProperties_NatureManifest Props => (CompProperties_NatureManifest)props;


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var incidents = new List<IncidentDef>
            {
                IncidentDefOf.GauranlenPodSpawn,
                DefDatabase<IncidentDef>.GetNamedSilentFail("PoluxTreeSpawn"),
                DefDatabase<IncidentDef>.GetNamedSilentFail("AnimaTreeSpawn"),
            }.Where(x => x is not null).ToList();
            if (incidents.TryRandomElement(out var incident))
            {
                var parms = StorytellerUtility.DefaultParmsNow(incident.category, parent.pawn.Map);
                incident.Worker.TryExecute(parms);
            }
        }
    }
}
