using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Node node = other.GetComponent<Node>();
        
        if (node != null && this.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
        }
    }
}
