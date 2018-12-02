using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
  private enum MoveDirection
  {
    UP, DOWN, LEFT, RIGHT
  };

  public float speed = 200f;
  public GameObject bomb;
  public Tilemap tileMap;
  public GameObject bombPoint;

  private Rigidbody2D _rigidbody2D;
  private Vector3 _playerMove;
  private Vector2 _playerSpeed;
  private MoveDirection _moveDirection;
  private Animator _animator;

  // Use this for initialization
  void Start()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    _playerMove = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
    updateAnimations();
    updateButtonActions();
    Vector2 playerMove2D = new Vector2(_playerMove.x, _playerMove.y);
    _playerSpeed = playerMove2D.normalized * Time.deltaTime * speed;
  }

  void updateAnimations()
  {
    _animator.SetFloat("Horizontal", _playerMove.x);
    _animator.SetFloat("Vertical", _playerMove.y);
    _animator.SetFloat("Magnitude", _playerMove.magnitude);

    if (_playerMove.x == 0 || _playerMove.y == 0)
    {
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
    if (Input.GetButtonUp("Fire1"))
    {
      Vector3Int cell = tileMap.WorldToCell(bombPoint.transform.position);
      Vector3 cellCenterPos = tileMap.GetCellCenterWorld(cell);
      Instantiate(bomb, cellCenterPos, Quaternion.identity);
    }
  }

  void FixedUpdate()
  {
    _rigidbody2D.velocity = _playerSpeed;
  }
}
