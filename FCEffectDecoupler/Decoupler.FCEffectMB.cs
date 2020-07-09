using BS_Utils.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace FCEffectDecoupler
{
    public static partial class Decoupler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812:Avoid uninstantiated internal classes", Justification = "Called by Unity")]
        private class FCEffectMB : MonoBehaviour
        {
            private bool _comboBreak;
            private int _lastNoteId;
            private const float TimeoutLength = 5.0f;
            private IEnumerator<WaitForSeconds> _coroutine;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
            private void Start()
            {
                BSEvents.levelRestarted += Hide;
                BSEvents.levelQuit += Hide;
                BSEvents.comboDidBreak += ComboBreak;
            }

            internal void ReInit(int lastNoteId)
            {
                _lastNoteId = lastNoteId;
                BSEvents.noteWasCut -= LastNoteCheck;
                BSEvents.noteWasCut += LastNoteCheck;
            }

            private void LastNoteCheck(NoteData arg1, NoteCutInfo arg2, int arg3)
            {
                if (arg1.id == _lastNoteId && arg2.allIsOK && !_comboBreak)
                {
                    _fcEffect.SetActive(true);
                    _coroutine = FallbackHide(TimeoutLength);
                    StartCoroutine(_coroutine);
                }
            }

            private static IEnumerator<WaitForSeconds> FallbackHide(float timeoutLength)
            {
                yield return new WaitForSeconds(timeoutLength);
                _fcEffect.SetActive(false);
            }

            private void ComboBreak()
            {
                _comboBreak = true;
            }

            internal void Hide(StandardLevelScenesTransitionSetupDataSO arg1, LevelCompletionResults arg2)
            {
                Hide();
            }

            internal void Hide()
            {
                _comboBreak = false;
                _fcEffect.SetActive(false);
                
                if (_coroutine == null)
                {
                    return;
                }
                
                StopCoroutine(_coroutine);
            }

            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.KeypadDivide))
                {
                    _fcEffect.SetActive(!_fcEffect.activeSelf);
                }
            }
        }
    }
}