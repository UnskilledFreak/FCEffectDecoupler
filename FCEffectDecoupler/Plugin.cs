using BS_Utils.Utilities;
using IPA;
using UnityEngine.SceneManagement;
using FCEffectDecoupler.HarmonyPatches;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;

namespace FCEffectDecoupler
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid unistantiated internal classes", Justification = "Instantiated by BSIPA")]
    internal class Plugin
    {
        static bool runOnce;

        [Init]
        public void Init(IPALogger logger)
        {
            Logger.log = logger;
            Logger.Info("Logger initialized");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Logger.Info("Plugin starting...");
            BSEvents.OnLoad();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            Logger.Info("done");
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MenuCore" && !runOnce)
            {
                Logger.Info("First scene change to MenuCore, apply patch");
                runOnce = true;
                Patcher.Patch();
                Decoupler.OnLoad();
            }
        }

        public void OnSceneUnloaded(Scene scene)
        {
            if (scene.name == "GameplayCore")
            {
                Decoupler.HideEffect();
            }
        }
    }
}