using Audio;
using Events.ScriptableObjects;
using Management.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menus
{
    /// <summary>
    /// This class manages the title screen.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class TitleScreenManager : MonoBehaviour
    {
        /// <summary>
        /// The start game button
        /// </summary>
        [Header("Buttons")] public Button startButton;
        /// <summary>
        /// The start 2p game button
        /// </summary>
        public Button startButton2;
        /// <summary>
        /// The tutorial button
        /// </summary>
        public Button tutorialButton;
        /// <summary>
        /// The back button
        /// </summary>
        public Button backButton1;
        /// <summary>
        /// The exit button
        /// </summary>
        public Button exitButton;

        /// <summary>
        /// The title screen
        /// </summary>
        [Header("Screens")] public GameObject titleScreen;
        
        /// <summary>
        /// The tutorial screen
        /// </summary>
        public GameObject tutorialScreen;
        
        [Header("Score Holder")] public ScoreHolderSO scoreHolder;

        [Header("Broadcasting on")] public VoidEventChannelSO menuLoadedEvent;
        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            startButton2.onClick.AddListener(StartGame2);
            tutorialButton.onClick.AddListener(ShowTutorial);
            backButton1.onClick.AddListener(ShowTitleScreen);
            exitButton.onClick.AddListener(ExitGame);
            menuLoadedEvent.RaiseEvent();
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartGame()
        {
            scoreHolder.Init();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private void StartGame2()
        {
            scoreHolder.Init();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        /// <summary>
        /// Shows the tutorial.
        /// </summary>
        private void ShowTutorial()
        {
            //We hide the title screen and display the tutorial
            titleScreen.SetActive(false);
            tutorialScreen.SetActive(true);
        }

        /// <summary>
        /// Shows the title screen.
        /// </summary>
        private void ShowTitleScreen()
        {
            tutorialScreen.SetActive(false);
            titleScreen.SetActive(true);
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private static void ExitGame()
        {
            Application.Quit();
        }
    }
}