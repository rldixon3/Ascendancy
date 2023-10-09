using HarmonyLib;
using RimWorld;
using Verse;

namespace Ascendancy
{
    [HarmonyPatch(typeof(CompPowerPlantWind), "DesiredPowerOutput", MethodType.Getter)]
    public static class CompPowerPlantWind_DesiredPowerOutput_Patch
    {
        public static void Postfix(ref float __result)
        {
            if (GameCondition_Ascendancy.Instance.lastTimeWindWaveApplied > 0 && Find.TickManager.TicksGame < GameCondition_Ascendancy.Instance.lastTimeWindWaveApplied + (GenDate.TicksPerDay * 2))
            {
                __result *= 2;
            }
        }
    }
}
