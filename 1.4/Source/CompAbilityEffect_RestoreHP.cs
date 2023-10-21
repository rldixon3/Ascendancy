using RimWorld;
using UnityEngine;
using Verse;

namespace Ascendancy
{
    public class CompProperties_RestoreHP : CompProperties_AbilityEffect
    {

        public CompProperties_RestoreHP()
        {
            compClass = typeof(CompAbilityEffect_RestoreHP);
        }
    }

    public class CompAbilityEffect_RestoreHP : CompAbilityEffect
    {
        public new CompProperties_RestoreHP Props => (CompProperties_RestoreHP)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var thing = target.Thing;
            if (thing != null)
            {
                var hpToAdd = thing.MaxHitPoints / 10f;
                thing.HitPoints = (int)Mathf.Min(thing.MaxHitPoints, thing.HitPoints + hpToAdd);
            }
        }
    }
}
