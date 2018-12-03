using UnityEngine;

public class ExplosionCenter : MonoBehaviour
{
  public GameObject explosionUp;
  public GameObject explosionLeft;
  public GameObject explosionDown;
  public GameObject explosionRight;
  private float raycastDistance = 0.6f;
  private LayerMask foregroundLayer;
  private LayerMask explosionLayer;
  private LayerMask bombLayer;
  private int finalLayer;

  // Use this for initialization
  void Start()
  {
    foregroundLayer = LayerMask.GetMask("Foreground");
    explosionLayer = LayerMask.GetMask("CollateralExplosion");
    bombLayer = LayerMask.GetMask("Bomb");
    finalLayer = foregroundLayer | explosionLayer | bombLayer;
    ExplodeUp();
    ExplodeLeft();
    ExplodeRight();
    ExplodeDown();
  }

  void ExplodeUp()
  {
    Vector2 upPosition = new Vector2(transform.position.x, transform.position.y + 1);
    RaycastHit2D testUp = Physics2D.Raycast(upPosition, Vector2.up, raycastDistance, finalLayer);

    if (testUp.collider == null)
    {
      Instantiate(explosionUp, upPosition, Quaternion.identity);
    }
    else
    {
      Debug.Log("Explosion up raycast with: " + testUp.collider.name);
    }
  }

  void ExplodeLeft()
  {
    Vector2 leftPosition = new Vector2(transform.position.x - 1, transform.position.y);
    RaycastHit2D testLeft = Physics2D.Raycast(leftPosition, Vector2.left, raycastDistance, finalLayer);

    if (testLeft.collider == null)
    {
      Instantiate(explosionLeft, leftPosition, Quaternion.identity);
    }
    else
    {
      Debug.Log("Explosion left raycast with: " + testLeft.collider.name);
    }
  }

  void ExplodeRight()
  {
    Vector2 rightPosition = new Vector2(transform.position.x + 1, transform.position.y);
    RaycastHit2D testRight = Physics2D.Raycast(rightPosition, Vector2.right, raycastDistance, finalLayer);

    if (testRight.collider == null)
    {
      Instantiate(explosionRight, rightPosition, Quaternion.identity);
    }
    else
    {
      Debug.Log("Explosion right raycast with: " + testRight.collider.name);
    }
  }

  void ExplodeDown()
  {
    Vector2 downPosition = new Vector2(transform.position.x, transform.position.y - 1);
    RaycastHit2D testDown = Physics2D.Raycast(downPosition, Vector2.down, raycastDistance, finalLayer);

    if (testDown.collider == null)
    {
      Instantiate(explosionDown, downPosition, Quaternion.identity);
    }
    else
    {
      Debug.Log("Explosion down raycast with: " + testDown.collider.name);
    }
  }
}
