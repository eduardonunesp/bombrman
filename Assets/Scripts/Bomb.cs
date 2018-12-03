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
  private float _counterToActivatePhysics = 0.5f;

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
      Collider2D throughArea = _collider2D.GetComponent<Collider2D>();

      if (!throughArea.bounds.Contains(_playerOwner.transform.position))
      {
        _counterToActivatePhysics -= Time.deltaTime;
        if (_counterToActivatePhysics <= 0)
        {
          Physics2D.IgnoreCollision(playerCollider2D, _collider2D, false);
        }
      }
    }
  }

  public void Explode()
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

  private void OnCollisionEnter2D(Collision2D other)
  {
    Debug.Log("Bomb collides with " + other.gameObject.name);
    // Debug.Log(other.gameObject.name.StartsWith("Explosion"));
    // if (other.gameObject.name.StartsWith("Explosion"))
    // {
    //   bombTimeout = 10f;
    //   Explode();
    // }
  }
}
