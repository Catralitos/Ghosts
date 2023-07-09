using Maze_Elements;
using UnityEngine;

namespace Pacman
{
    public class PacmanChase : PacmanBehavior
    {
        private void OnDisable()
        {
            pacman.scatter.Enable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();

            if (node != null && enabled)
            {
                Vector2 direction = Vector2.zero;
                float maxScore = float.MinValue;

                // Find the available direction that moves farthest from pacman
                foreach (Vector2 availableDirection in node.availableDirections)
                {
                    // If the distance in this direction is greater than the current
                    // max distance then this direction becomes the new farthest
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                    float score = 0;

                    foreach(Transform pellet in pacman.pelletMap) {
                        score += pelletWeight / (pellet.position - newPosition).sqrMagnitude;
                    }
                    foreach(Transform ghost in pacman.ghosts) {
                        score += ghostWeight / (ghost.position - newPosition).sqrMagnitude;
                    }

                    if (score > maxScore)
                    {
                        direction = availableDirection;
                        maxScore = score;
                    }
                }

                pacman.movement.SetDirection(direction);
            }
        }

    }
}