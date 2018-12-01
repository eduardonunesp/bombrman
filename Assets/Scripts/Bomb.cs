using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
  public GameObject explosionCenter;

  // Use this for initialization
  void Start()
  {
    StartCoroutine(Countdown());
  }

  private IEnumerator Countdown()
  {
    int counter = 3;
    while (counter > 0)
    {
      yield return new WaitForSeconds(1);
      counter--;
    }
    Explode();
  }

  void Explode()
  {
    Instantiate(explosionCenter, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
