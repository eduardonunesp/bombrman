using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  // Explosion center instantiate after destroy this
  public GameObject explosionCenter;

  // Number of seconds til explode!
  public float bombTimeout = 3f;

  // Caching
  private Collider2D _collider2D;

  // Player that owner this bomb
  private Player _playerOwner;

  void Start()
  {
    _collider2D = GetComponent<Collider2D>();
    Debug.Log("Bomb on position: " + transform.position);
  }

  void Update()
  {
    // Decrease timeout on each frame based on delta time
    bombTimeout -= Time.deltaTime;

    // Timeout reached going to explode
    if (bombTimeout <= 0)
      Explode();

    // Only check if this bomb belongs to the right player
    if (_playerOwner && _playerOwner.isOnPlayerList(this))
    {
      // Get player collider
      Collider2D playerCollider2D = _playerOwner.GetComponent<Collider2D>();
      int possibleNumberOfCollisions = 10;

      // Create colliders
      Collider2D[] colliders = new Collider2D[possibleNumberOfCollisions];
      ContactFilter2D contactFilter2D = new ContactFilter2D();

      // Get overlaps numbers
      int collisionsCount = _collider2D.OverlapCollider(contactFilter2D, colliders);

      // TODO: Fix this
      // Debug.Log("OverlapCoutner: " + collisionsCount);
      // Only colliding with foreground :)
      // if (collisionsCount < 2)
      // {
      //   Physics2D.IgnoreCollision(playerCollider2D, _collider2D, false);
      // }
    }
  }

  void Explode()
  {
    // Instantiate the explosion center on this bomb position
    Instantiate(explosionCenter, transform.position, Quaternion.identity);

    // Remove from player's bomb list if player owner exists
    if (_playerOwner)
    {
      // Remove this bomb
      _playerOwner.removeBombFromList(this);
    }

    // Destroy this object now
    Destroy(gameObject);
  }

  // Set the player which owns this bomb
  public void setPlayerOwner(Player player)
  {
    _playerOwner = player;
  }
}
