using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Ascendancy
{
    [HarmonyPatch(typeof(ImmunityHandler), "DiseaseContractChanceFactor",
    new Type[] { typeof(HediffDef), typeof(HediffDef), typeof(BodyPartRecord) },
    new ArgumentType[] { ArgumentType.Normal, ArgumentType.Out, ArgumentType.Normal })]
    public static class ImmunityHandler_DiseaseContractChanceFactor_Patch
    {
        [HarmonyPriority(int.MinValue)]
        public static void Postfix(ref float __result, ImmunityHandler __instance, HediffDef diseaseDef, ref HediffDef immunityCause, BodyPartRecord part = null)
        {
            if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(AR_DefOf.AR_Stasis) != null)
            {
                immunityCause = null;
                __result = 0f;
            }
        }
    }

    [HarmonyPatch(typeof(IncidentWorker_DiseaseHuman), "PotentialVictimCandidates")]
    public static class IncidentWorker_DiseaseHuman_PotentialVictimCandidates
    {
        public static IEnumerable<Pawn> Postfix(IEnumerable<Pawn> __result, IncidentWorker_DiseaseHuman __instance)
        {
            foreach (var p in __result)
            {
                if (p.health.hediffSet.GetFirstHediffOfDef(AR_DefOf.AR_Stasis) != null)
                {
                    continue;
                }
                else
                {
                    yield return p;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Pawn_HealthTracker), "AddHediff", new Type[]
    {
        typeof(Hediff), typeof(BodyPartRecord), typeof(DamageInfo?), typeof(DamageWorker.DamageResult)
    })]
    public static class Pawn_HealthTracker_AddHediff_Patch
    {
        [HarmonyPriority(int.MaxValue)]
        private static bool Prefix(Pawn_HealthTracker __instance, Pawn ___pawn, ref Hediff hediff, BodyPartRecord part = null, DamageInfo? dinfo = null, DamageWorker.DamageResult result = null)
        {
            if (___pawn.health.hediffSet.GetFirstHediffOfDef(AR_DefOf.AR_Stasis) != null)
            {
                if (hediff.def.PreventFromCatching())
                {
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(HediffSet), "AddDirect")]
    public static class HediffSet_AddDirect_Patch
    {
        [HarmonyPriority(int.MaxValue)]
        private static bool Prefix(HediffSet __instance, Pawn ___pawn, Hediff hediff)
        {
            if (___pawn.health.hediffSet.GetFirstHediffOfDef(AR_DefOf.AR_Stasis) != null)
            {
                if (hediff.def.PreventFromCatching())
                {
                    return false;
                }
            }
            return true;
        }

        public static bool PreventFromCatching(this HediffDef hediffDef)
        {
            return hediffDef.chronic || hediffDef.makesSickThought 
                || hediffDef.CompProps<HediffCompProperties_Immunizable>() != null || hediffDef == HediffDefOf.BloodLoss;
        }
    }


    [HarmonyPatch]
    public static class Hediff_Tick_Patch
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Hediff), nameof(Hediff.Tick));
            yield return AccessTools.Method(typeof(Hediff), nameof(Hediff.PostTick));
        }
        public static bool Prefix(Hediff __instance)
        {
            if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(AR_DefOf.AR_Stasis) != null)
            {
                if (__instance.def.PreventFromCatching())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
