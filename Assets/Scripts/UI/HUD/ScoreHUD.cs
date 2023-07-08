using Management.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class ScoreHUD : MonoBehaviour
    {
        public ScoreHolderSO scoreHolder;

        public TextMeshProUGUI pacScoreText;
        public TextMeshProUGUI highScoreText;
        public TextMeshProUGUI ghostScoreText;

        private void Update()
        {
            pacScoreText.text = scoreHolder.CurrentPacmanScore.ToString();
            ghostScoreText.text = scoreHolder.CurrentGhostScore.ToString();
            highScoreText.text = scoreHolder.PastScores.Count == 0 ? scoreHolder.CurrentGhostScore.ToString() : scoreHolder.GetHighScores(1)[0].ToString();
        }
    }
}
