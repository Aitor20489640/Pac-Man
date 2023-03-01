using System;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Movement movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (movement.direction == Vector2.up)
        {
            SpriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down)
        {
            SpriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left)
        {
            SpriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right)
        {
            SpriteRenderer.sprite = right;
        }
    }
}
