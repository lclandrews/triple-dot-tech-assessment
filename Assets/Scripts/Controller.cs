using TripleDot.Blur;
using TripleDot.Home;
using TripleDot.Navigation;
using TripleDot.Interfaces;
using TripleDot.LevelCompleted;
using TripleDot.Map;
using TripleDot.Settings;
using TripleDot.Shop;

using UnityEngine;
using UnityEngine.Localization.Settings;

namespace TripleDot
{
    public class Controller : MonoBehaviour
    {
        public static Controller Instance { get; private set; }

        [Header("Screens")]
        [SerializeField] private LevelCompletedScreen _levelCompletedScreen;
        [SerializeField] private HomeScreen _homeScreen;
        [SerializeField] private MapScreen _mapScreen;
        [SerializeField] private ShopScreen _shopScreen;
        
        [Header("Popups")]
        [SerializeField] private SettingsPopup _settingsPopupWindow;
        
        [Header("Blur")]
        [SerializeField] private BlurBehaviour _blurBehaviour;
        
        [Header("Navigation")]
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

        public void OpenLevelCompletedScreen()
        {
            StartCoroutine(_levelCompletedScreen.Show(_blurBehaviour));
        }

        public void CloseLevelCompletedScreen()
        {
            StartCoroutine(_levelCompletedScreen.Hide(_blurBehaviour));
        }
        
        public void OpenHomeScreen()
        {
            StartCoroutine(_homeScreen.Show(_blurBehaviour));
        }

        public void OpenMapScreen()
        {
            StartCoroutine(_mapScreen.Show(_blurBehaviour));
        }

        public void OpenShopScreen()
        {
            StartCoroutine(_shopScreen.Show(_blurBehaviour));
        }
    }
}
