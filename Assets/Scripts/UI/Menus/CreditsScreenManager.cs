using Audio;
using Events.ScriptableObjects;
using Management.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menus
{
    public class CreditsScreenManager : MonoBehaviour
    {
        public ScoreHolderSO scoreHolder;
        
        [Header("Buttons")] public Button retryButton;
        public Button backToTitleButton;

        [Header("Broadcasting on")] public VoidEventChannelSO menuLoadedEvent;
        
        private void Start()
        {
            retryButton.onClick.AddListener(RetryGame);
            backToTitleButton.onClick.AddListener(BackToTitle);
            menuLoadedEvent.RaiseEvent();
        }

        private void RetryGame()
        {
            scoreHolder.Init();
            SceneManager.LoadScene(scoreHolder.singlePlayerGame ? 1 : 2);
        }

        private static void BackToTitle()
        {
            SceneManager.LoadScene(0);
        }
        
    }
}

