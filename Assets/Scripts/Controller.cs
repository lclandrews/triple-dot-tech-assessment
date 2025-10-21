using TripleDot.Blur;
using TripleDot.Components;
using TripleDot.HomeScreen;
using TripleDot.Interfaces;

using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TripleDot
{
    public class Controller : MonoBehaviour
    {
        public static Controller Instance { get; private set; }

        [SerializeField] private PopupWindow _settingsPopupWindow;
        [SerializeField] private BlurBehaviour _blurBehaviour;  
        [field: SerializeField] public BottomViewBar BottomViewBar { get; private set; } = null;

        private int _localeIndex = 0;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Instance = null;
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning($"Multiple instances of Controller found in scene.", this);
            }
            
            if (_blurBehaviour == null)
            {
                Debug.LogWarning($"BlurBehaviour for {gameObject.name} is not assigned.", this);
            }
            
            if (BottomViewBar == null)
            {
                Debug.LogWarning($"BottomViewVar for {gameObject.name} is not assigned.", this);
            }
            
            BottomViewBar.Closed.AddListener(OnBottomBarClosed);
            BottomViewBar.ContentActivated.AddListener(OnBottomBarContentActivated);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        
        private void OnBottomBarClosed()
        {
            Debug.Log($"BottomViewBar is closed.");
        }

        private void OnBottomBarContentActivated(IIdentifiableBehaviour behaviour)
        {
            Debug.Log($"Content activated for {behaviour.Identifier}.");
        }

        public void BlurContent()
        {
            StartCoroutine(_blurBehaviour.Show());
        }

        public void RestoreContent()
        {
            StartCoroutine(_blurBehaviour.Hide());
        }
        
        public void OnSettingsClicked()
        {
            StartCoroutine(_settingsPopupWindow.Show(_blurBehaviour));
        }
        
        public void CloseSettingsPopup()
        {
            StartCoroutine(_settingsPopupWindow.Hide(_blurBehaviour));
        }

        public void CycleLocale()
        {
            ILocalesProvider localesProvider = LocalizationSettings.Instance.GetAvailableLocales();
            _localeIndex++;
            _localeIndex %= localesProvider.Locales.Count;
            LocalizationSettings.Instance.SetSelectedLocale(localesProvider.Locales[_localeIndex]);
        }
    }
}
