using UnityEngine;
using Random = UnityEngine.Random;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.avaliableDirections.Count);

            if (node.avaliableDirections[index] == -ghost.movement.direction && node.avaliableDirections.Count > 1)
            {
                index++;

                if (index >= node.avaliableDirections.Count)
                {
                    index = 0;
                }
            }
            
            ghost.movement.SetDirection(node.avaliableDirections[index]);
        }
    }
}
