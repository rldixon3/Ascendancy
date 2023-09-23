using RimWorld;
using Verse;

namespace Ascendancy
{
    public class CompProperties_AbilityExplosion : CompProperties_AbilityEffect
    {
        public float radius;

        public DamageDef damageDef;

        public CompProperties_AbilityExplosion()
        {
            compClass = typeof(CompAbilityEffect_Explosion);
        }
    }

    public class CompAbilityEffect_Explosion : CompAbilityEffect
    {
        public new CompProperties_AbilityExplosion Props => (CompProperties_AbilityExplosion)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            GenExplosion.DoExplosion(target.Cell, parent.pawn.MapHeld, Props.radius, Props.damageDef, parent.pawn);
        }

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            GenDraw.DrawRadiusRing(target.Cell, Props.radius);
        }
    }
}
