using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostHome : GhostBehavior
{
   public Transform inside;
   public Transform outside;

   private void OnEnable()
   {
      StopAllCoroutines();
   }

   private void OnDisable()
   {
      if (gameObject.activeSelf)
      {
         StartCoroutine(ExitTransition());
      }
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if (enabled && col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
      {
         ghost.movement.SetDirection(-ghost.movement.direction);
      }
   }

   private IEnumerator ExitTransition()
   {
      ghost.movement.SetDirection(Vector2.up, true);
      ghost.movement.rigidBody.isKinematic = true;
      ghost.movement.enabled = false;

      Vector3 position = transform.position;

      float duration = 0.5f;
      float elapsed = 0;

      while (elapsed < duration)
      {
         Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
         ghost.transform.position = newPosition;
         elapsed += Time.deltaTime;
         yield return null;
      }

      elapsed = 0;
      
      while (elapsed < duration)
      {
         Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
         ghost.transform.position = newPosition;
         elapsed += Time.deltaTime;
         yield return null;
      }
      
      ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
      ghost.movement.rigidBody.isKinematic = false;
      ghost.movement.enabled = true;
   }
}
