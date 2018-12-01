using UnityEngine;

public class ExplosionCenter : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
  }
}
