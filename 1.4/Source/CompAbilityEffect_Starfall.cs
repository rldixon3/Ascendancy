using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Starfall : CompProperties_AbilityEffect
    {
        public CompProperties_Starfall()
        {
            compClass = typeof(CompAbilityEffect_Starfall);
        }
    }

    public class CompAbilityEffect_Starfall : CompAbilityEffect
    {
        public new CompProperties_Starfall Props => (CompProperties_Starfall)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Map map = parent.pawn.Map;
            var parms = default(ThingSetMakerParams);
            parms.countRange = new IntRange(15 * 15, 15 * 15);
            List<Thing> list = ThingSetMakerDefOf.Meteorite.root.Generate(parms);
            SkyfallerMaker.SpawnSkyfaller(ThingDefOf.MeteoriteIncoming, list, target.Cell, map);
        }
    }
}
