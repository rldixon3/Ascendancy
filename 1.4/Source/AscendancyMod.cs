using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace Ascendancy
{
    public class AscendancyMod : Mod
    {
        public AscendancyMod(ModContentPack pack) : base(pack)
        {
			new Harmony("AscendancyMod").PatchAll();
        }
    }
}
