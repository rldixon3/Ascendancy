using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Ascendancy
{
    public class CompProperties_PersonaManifest : CompProperties_AbilityEffect
    {
        public CompProperties_PersonaManifest()
        {
            compClass = typeof(CompAbilityEffect_PersonaManifest);
        }
    }

    public class CompAbilityEffect_PersonaManifest : CompAbilityEffect
    {
        public new CompProperties_PersonaManifest Props => (CompProperties_PersonaManifest)props;

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            var pawn = target.Pawn;
            CompBladelinkWeapon compBladelinkWeapon = pawn?.equipment?.bondedWeapon?.TryGetComp<CompBladelinkWeapon>();
            if (compBladelinkWeapon is null)
            {
                if (throwMessages)
                {
                    Messages.Message("AR.RequiresPersonaWeapon".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }
            return base.Valid(target, throwMessages);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            var pawn = target.Pawn; 
            CompBladelinkWeapon compBladelinkWeapon = pawn.equipment.bondedWeapon.TryGetComp<CompBladelinkWeapon>();
            if (compBladelinkWeapon != null)
            {
                IEnumerable<WeaponTraitDef> allDefs = DefDatabase<WeaponTraitDef>.AllDefs;
                IEnumerable<WeaponTraitDef> source = allDefs.Where((WeaponTraitDef x) => compBladelinkWeapon.CanAddTrait(x));
                if (source.Any())
                {
                    compBladelinkWeapon.traits.Add(source.RandomElementByWeight((WeaponTraitDef x) => x.commonality));
                }
            }
        }
    }
}
