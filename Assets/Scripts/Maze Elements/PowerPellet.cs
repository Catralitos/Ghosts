using Events.ScriptableObjects;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze_Elements
{
    public class PowerPellet : MonoBehaviour
    {

        public LayerMask pacmanMask;
    
        [Header("Broadcasting on")] public VoidEventChannelSO powerPelletEatenEvent;

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }
    
        private void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 1) gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (pacmanMask.HasLayer(other.gameObject.layer))
            {
                powerPelletEatenEvent.RaiseEvent();
                gameObject.SetActive(false);
            }
        }
    }
}