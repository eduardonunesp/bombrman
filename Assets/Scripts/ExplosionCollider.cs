using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name.StartsWith("Bomb"))
    {
      other.gameObject.BroadcastMessage("Explode");
    }

    if (other.gameObject.name.StartsWith("Player"))
    {
      other.gameObject.BroadcastMessage("Kill");
    }
  }
}
