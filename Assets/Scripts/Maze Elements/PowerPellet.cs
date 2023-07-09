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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (pacmanMask.HasLayer(other.gameObject.layer))
            {
                powerPelletEatenEvent.RaiseEvent();
                Destroy(gameObject);
            }
        }
    }
}