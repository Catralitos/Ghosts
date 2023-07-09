using Events.ScriptableObjects;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze_Elements
{
    public class Pellet : MonoBehaviour
    {

        public LayerMask pacmanMask;
    
        [Header("Broadcasting on")] public VoidEventChannelSO pelletEatenEvent;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (pacmanMask.HasLayer(other.gameObject.layer))
            {
                pelletEatenEvent.RaiseEvent();
                gameObject.SetActive(false);
            }
        }
    }
}
