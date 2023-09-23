using RimWorld;
using Verse;

namespace Ascendancy
{
    public class CompProperties_Ignite : CompProperties_AbilityEffect
    {
        public CompProperties_Ignite()
        {
            compClass = typeof(CompAbilityEffect_Ignite);
        }
    }
    public class CompAbilityEffect_Ignite : CompAbilityEffect
    {
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (target.HasThing && target.Thing.CanEverAttachFire() && !target.Thing.HasAttachment(ThingDefOf.Fire))
            {
                FireUtility.TryAttachFire(target.Thing, 0.5f);
            }
            else
            {
                Fire obj = (Fire)ThingMaker.MakeThing(ThingDefOf.Fire);
                obj.fireSize = 0.5f;
                GenSpawn.Spawn(obj, target.Cell, parent.pawn.Map, Rot4.North);
            }
        }
    }
}
