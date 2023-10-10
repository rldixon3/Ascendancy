using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Ascendancy
{
    public class Ability_Terraforma : Psycast
    {
        public TerrainDef pick;

        public Ability_Terraforma(Pawn pawn) : base(pawn)
        {
        }

        public Ability_Terraforma(Pawn pawn, AbilityDef def)
            : base(pawn, def)
        {
        }

        public override void QueueCastingJob(LocalTargetInfo target, LocalTargetInfo destination)
        {
            if (pick == null)
            {
                var list = new List<FloatMenuOption>();
                foreach (var terrain in DefDatabase<TerrainDef>.AllDefs.Where(x => x.natural))
                {
                    list.Add(new FloatMenuOption(terrain.LabelCap, delegate
                    {
                        pick = terrain;
                        this.CompOfType<CompAbilityEffect_Terraforma>().pick = terrain;
                        Find.Targeter.BeginTargeting(this.verb);
                    }));
                }
                Find.WindowStack.Add(new FloatMenu(list));
            }
            else
            {
                base.QueueCastingJob(target, destination);
            }
            pick = null;
        }
    }

    public class CompProperties_Terraforma : CompProperties_AbilityEffect
    {
        public CompProperties_Terraforma()
        {
            compClass = typeof(CompAbilityEffect_Terraforma);
        }
    }

    public class CompAbilityEffect_Terraforma : CompAbilityEffect
    {
        public TerrainDef pick;

        public new CompProperties_Terraforma Props => (CompProperties_Terraforma)props;

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            base.DrawEffectPreview(target);
            GenDraw.DrawFieldEdges(CellRect.CenteredOn(target.Cell, 10, 10).ToList());
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            foreach (var cell in CellRect.CenteredOn(target.Cell, 10, 10).ToList())
            {
                parent.pawn.Map.terrainGrid.SetTerrain(cell, pick);
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Defs.Look(ref pick, nameof(pick));
        }
    }
}
