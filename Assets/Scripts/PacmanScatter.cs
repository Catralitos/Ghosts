using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScatter : PacmanBehavior
{
    private void OnDisable()
    {
        pacman.chase.Enable();
    }

    private void FixedUpdate() {
        RaycastHit2D ghostHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.35f, 0f, pacman.movement.direction, 5.0f, pacman.ghostLayer | pacman.wallLayer);

        if (ghostHit.collider != null && (pacman.ghostLayer & 1 << ghostHit.collider.gameObject.layer) == 1 << ghostHit.collider.gameObject.layer) {
            Debug.Log("AHHHHH a ghost");
            pacman.movement.SetDirection(pacman.movement.direction * -1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
         Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxScore = float.MinValue;

            Debug.Log("-------------------------------------------------------");
            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Debug.Log("availableDirection: " + availableDirection);
                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float score = 0;

                foreach(Transform pellet in pacman.pelletMap) {
                    if (pellet.gameObject.activeSelf)
                        score += pelletWeight / (pellet.position - newPosition).sqrMagnitude;
                }
                foreach(Transform ghost in pacman.ghosts) {
                    float test = ghostWeight / Mathf.Pow((ghost.position - newPosition).sqrMagnitude, 1.0f);
                    Debug.Log("Ghost score: " + test);
                    score -= test;
                }

                if (availableDirection == pacman.movement.direction * -1.0f) {
                    score -= sameDirectionPenalty;
                    Debug.Log("Punishment " + score);
                }

                if (score > maxScore)
                {
                    direction = availableDirection;
                    maxScore = score;
                }
                Debug.Log(score);
            }

            Debug.Log(direction);
            pacman.movement.SetDirection(direction);
        }
    }
}
