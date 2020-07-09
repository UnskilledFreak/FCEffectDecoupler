using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FCEffectDecoupler
{
    public static partial class Decoupler
    {
        private static GameObject _fcEffect;
        private static Scene? _fcScene;
        private static FCEffectMB _instance;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Fuck off")]
        internal static void OnLoad()
        {
            GameObject prefab = CustomSaber.Utilities.SaberAssetLoader.CustomSabers.FirstOrDefault(x => x.FileName == "Plasma_Katana.saber")?.Sabers.transform.Find("FullComboEffect")?.gameObject;
            if (prefab == null)
            {
                Logger.Error("could not find Plasma Katana Sabers! Effect will not work!");
                return;
            }

            _fcEffect = UnityEngine.Object.Instantiate(prefab.transform.Find("FullComboRoot").gameObject);
            UnityEngine.Object.Destroy(prefab);
            _fcEffect.transform.position = new Vector3(0, 1.5f, 0);
            _fcEffect.GetComponentInChildren<AudioSource>().volume = 0.5f;
            _fcScene = SceneManager.CreateScene("FCScene");
            SceneManager.MoveGameObjectToScene(_fcEffect, _fcScene.Value);
            _instance = new GameObject("FCEffectMB").AddComponent<FCEffectMB>();
            SceneManager.MoveGameObjectToScene(_instance.gameObject, _fcScene.Value);
        }

        internal static void ReInitFCEffectMB(int lastNoteId)
        {
            if (_instance == null)
            {
                return;
            }
            
            _instance.ReInit(lastNoteId);
        }

        internal static void HideEffect()
        {
            if (_instance == null)
            {
                return;
            }
            
            _instance.Hide();
        }
    }
}