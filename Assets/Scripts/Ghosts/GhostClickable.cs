using UnityEngine;

namespace Ghosts
{
    public class GhostClickable : MonoBehaviour
    {
        public Mind mind;
        public GhostOrderer go;
        
        private void OnMouseDown()
        {
            mind.ChangeGhost(transform.parent.gameObject);
            go.enabled = true;
            Debug.Log("Picked " + transform.parent.gameObject);
        }
    }
}
