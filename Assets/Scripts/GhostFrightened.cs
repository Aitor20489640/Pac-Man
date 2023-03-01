using System;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    
    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        
        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();
        
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        eaten = true;

        Vector3 position = ghost.home.inside.position;
        ghost.transform.position = position;
        ghost.home.Enable(this.duration);
        
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void OnEnable()
    {
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled) 
            {
                Eaten();
            }
            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;
            
            foreach (Vector2 availableDirection in node.avaliableDirections)
            {
                Vector3 newPosition = transform.position + (Vector3) availableDirection;
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }
            
            ghost.movement.SetDirection(direction);
        }
    }
}
