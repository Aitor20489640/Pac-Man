using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> avaliableDirections { get; private set; }
    public LayerMask obstacleLayer;
    
    private void Start()
    {
        avaliableDirections = new List<Vector2>();
        
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, direction, 1f, obstacleLayer);
        if (hit.collider == null)
        {
            avaliableDirections.Add(direction);
        }
    }
}
