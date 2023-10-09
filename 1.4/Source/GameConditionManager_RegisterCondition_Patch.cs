using HarmonyLib;
using RimWorld;

namespace Ascendancy
{
    [HarmonyPatch(typeof(GameConditionManager), "RegisterCondition")]
    public static class GameConditionManager_RegisterCondition_Patch
    {
        public static void Prefix(GameConditionManager __instance, GameCondition cond)
        {
            if (cond is GameCondition_HeatWave heatWave)
            {
                for (var i = 0; i < GameCondition_Ascendancy.Instance.windWaveAppliedCount; i++)
                {
                    heatWave.duration /= 2;
                }
            }
        }
    }
}
