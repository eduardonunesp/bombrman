using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  // Explosion center instantiate after destroy this
  public GameObject explosionCenter;

  // Number of seconds til explode!
  public float bombTimeout = 3f;

  void Update()
  {
    // Decrease timeout on each frame based on delta time
    bombTimeout -= Time.deltaTime;

    // Timeout reached going to explode
    if (bombTimeout <= 0)
      Explode();
  }

  void Explode()
  {
    // Instantiate the explosion center on this bomb position
    Instantiate(explosionCenter, transform.position, Quaternion.identity);

    // Destroy this object now
    Destroy(gameObject);
  }
}
