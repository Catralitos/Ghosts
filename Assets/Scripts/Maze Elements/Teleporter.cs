using Extensions;
using UnityEngine;

namespace Maze_Elements
{
    public class Teleporter : MonoBehaviour
    {
        public LayerMask teleportables;

        public int teleporterDistance;
        public bool rightTeleporter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (teleportables.HasLayer(other.gameObject.layer))
            {
                int multiplier = rightTeleporter ? -1 : 1;
                GameObject o = other.gameObject;
                o.transform.position += new Vector3(multiplier * teleporterDistance, 0, 0);
            }
        }
    }
}
