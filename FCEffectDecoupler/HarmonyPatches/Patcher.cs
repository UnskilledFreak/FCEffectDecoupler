using HarmonyLib;

namespace FCEffectDecoupler.HarmonyPatches
{
    /// <summary>
    /// Harmony Patcher, set to auto-detect
    /// </summary>
    internal static class Patcher
    {
        /// <summary>
        /// Tracks if the Patcher has run or not.
        /// </summary>
        private static bool _runOnce;
        
        /// <summary>
        /// Used to patch the game, applies all patches.
        /// </summary>
        internal static void Patch()
        {
            if (_runOnce)
            {
                return;
            }
            
            new Harmony("Uialeth.FCEffectDecoupler").PatchAll();
            _runOnce = true;
        }
    }
}