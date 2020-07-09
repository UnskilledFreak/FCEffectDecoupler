using HarmonyLib;
using System;
using System.Reflection;

namespace FCEffectDecoupler.HarmonyPatches
{
    [HarmonyPatch]
    internal class Patch
    {
        [HarmonyTargetMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Bugs in Harmony require this to be present")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1801:Review unused parameters", Justification = "Bugs in Harmony require this to be present")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Fuck off")]
        public static MethodBase Target() => Type.GetType("CustomSaber.Utilities.SaberScript, CustomSaber").GetMethod("AddEvents", BindingFlags.NonPublic | BindingFlags.Instance);


        [HarmonyPostfix]
        public static void Postfix(int ___lastNoteId)
        {
            Decoupler.ReInitFCEffectMB(___lastNoteId);
        }
    }
}