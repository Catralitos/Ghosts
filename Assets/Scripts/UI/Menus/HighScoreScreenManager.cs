using System.Collections.Generic;
using Events.ScriptableObjects;
using Management.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menus
{
    public class HighScoreScreenManager : MonoBehaviour
    {
        public ScoreHolderSO scoreHolder;
        public List<TextMeshProUGUI> highScoreTexts;

        public Button retryButton;
        public Button creditsButton;
    
        [Header("Broadcasting on")] public VoidEventChannelSO menuLoadedEvent;
        
        private void Start()
        {
            retryButton.onClick.AddListener(Retry);
            creditsButton.onClick.AddListener(LoadCredits);
        
            List<int> highScores = scoreHolder.GetHighScores(10);
            for (int i = 0; i < highScores.Count; i++)
            {
                highScoreTexts[i].text = (i + 1) + ". " + highScores[i];
            }

            for (int j = highScores.Count; j < highScoreTexts.Count; j++)
            {
                highScoreTexts[j].gameObject.SetActive(false);
            }
            menuLoadedEvent.RaiseEvent();
        }

        private void Retry()
        {
            scoreHolder.Init();
            SceneManager.LoadScene(1);
        }
    
        private static void LoadCredits()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
