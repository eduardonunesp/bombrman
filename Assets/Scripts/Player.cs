using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
  // Directions which player is moving
  public enum MoveDirection
  {
    UP, DOWN, LEFT, RIGHT
  };

  public MoveDirection initialPosition = MoveDirection.RIGHT;

  // Normal speed of the player
  public float speed = 200f;

  // Max bomb
  public int bombCounter = 1;

  // Bomb instantiated
  public GameObject bomb;

  // Needs the tilemap reference to release bomb on the right spot
  public Tilemap tileMap;

  // Bomb point is the spot to check within tilemap
  public GameObject bombPoint;

  // Cached component
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private Collider2D _collider2D;

  // Player move helpers
  private Vector3 _playerMove;
  private Vector2 _playerSpeed;
  private MoveDirection _moveDirection;

  // Players bomb list
  private List<Bomb> _playerBombs;

  // Use this for initialization
  void Start()
  {
    // Caching components
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _collider2D = GetComponent<Collider2D>();

    // Setting player initial position
    _moveDirection = initialPosition;

    // Setting bombs list
    _playerBombs = new List<Bomb>();
  }

  // Update is called once per frame
  void Update()
  {
    // Getting the move from axis
    _playerMove = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

    // Updating animations based on blend tree (see animator)
    updateAnimations();

    // Listen for bomb button
    updateButtonActions();

    // Calculating player speed and movement
    Vector2 playerMove2D = new Vector2(_playerMove.x, _playerMove.y);
    _playerSpeed = playerMove2D.normalized * Time.deltaTime * speed;
  }

  void updateAnimations()
  {
    // Animator receives params for the blend tree
    _animator.SetFloat("Horizontal", _playerMove.x);
    _animator.SetFloat("Vertical", _playerMove.y);
    _animator.SetFloat("Magnitude", _playerMove.magnitude);

    // No more movement time to place the idle animation
    if (_playerMove.x == 0 || _playerMove.y == 0)
    {
      // Getting the latest direction to play the right idle animation
      if (_moveDirection == MoveDirection.LEFT)
      {
        _animator.SetBool("player_idle_left", true);
        _animator.SetBool("player_idle_right", false);
        _animator.SetBool("player_idle_down", false);
        _animator.SetBool("player_idle_up", false);
      }
      else if (_moveDirection == MoveDirection.RIGHT)
      {
        _animator.SetBool("player_idle_left", false);
        _animator.SetBool("player_idle_right", true);
        _animator.SetBool("player_idle_down", false);
        _animator.SetBool("player_idle_up", false);
      }
      else if (_moveDirection == MoveDirection.DOWN)
      {
        _animator.SetBool("player_idle_left", false);
        _animator.SetBool("player_idle_right", false);
        _animator.SetBool("player_idle_down", true);
        _animator.SetBool("player_idle_up", false);
      }
      else if (_moveDirection == MoveDirection.UP)
      {
        _animator.SetBool("player_idle_left", false);
        _animator.SetBool("player_idle_right", false);
        _animator.SetBool("player_idle_down", false);
        _animator.SetBool("player_idle_up", true);
      }
    }

    // Player is moving let's listen the the last direction
    if (_playerMove.x != 0 || _playerMove.y != 0)
    {
      if (_playerMove.x < 0)
      {
        _moveDirection = MoveDirection.LEFT;
      }
      else if (_playerMove.x > 0)
      {
        _moveDirection = MoveDirection.RIGHT;
      }
      else if (_playerMove.y < 0)
      {
        _moveDirection = MoveDirection.DOWN;
      }
      else if (_playerMove.y > 0)
      {
        _moveDirection = MoveDirection.UP;
      }
    }
  }

  void updateButtonActions()
  {
    if (_playerBombs.Count >= bombCounter) return;

    // Button pressed so drop a bomb
    if (Input.GetButtonUp("Fire1"))
    {
      // Little trick to get the center of the current tile
      // The center is based on the child component bombPoint
      Vector3Int cell = tileMap.WorldToCell(bombPoint.transform.position);
      Vector3 cellCenterPos = tileMap.GetCellCenterWorld(cell);

      if (!BoardManager.instance.canPutBombOn(cellCenterPos))
      {
        Debug.Log("Can't put a bomb over another");
        return;
      };

      // Nice, let's instantiate the death bringer
      GameObject gb = (GameObject)Instantiate(bomb, cellCenterPos, Quaternion.identity);

      // Get the bomb instantiated
      Bomb newBomb = gb.GetComponent<Bomb>();

      // Set the owner
      newBomb.setPlayerOwner(this);

      // Adding to player bomb's list
      _playerBombs.Add(newBomb);

      // Adding to board manager
      BoardManager.instance.bombs.Add(newBomb);

      // Ignore collision while player is above
      Collider2D bombCollider = (Collider2D)newBomb.GetComponent<CircleCollider2D>();
      Physics2D.IgnoreCollision(_collider2D, bombCollider);
    }
  }

  public void removeBombFromList(Bomb bomb)
  {
    // Check if the bomb belongs to this player
    if (_playerBombs.Contains(bomb))
    {
      // Remove bomb from the list
      // The bomb already gone
      _playerBombs.Remove(bomb);
      BoardManager.instance.bombs.Remove(bomb);
    }
  }

  public bool isOnPlayerList(Bomb bomb)
  {
    return _playerBombs.Contains(bomb);
  }

  void FixedUpdate()
  {
    // Calculate the physics for movement
    _rigidbody2D.velocity = _playerSpeed;
  }
}
