using UnityEngine;

public class DestroyOnAnimationEnded : MonoBehaviour
{
  // Use this for initialization
  void Start()
  {
    // Get animator component
    Animator animator = this.GetComponent<Animator>();

    // We're want the animation state 0
    int animationState = 0;

    // Get the info for the state animationState
    AnimatorStateInfo animationInfo = animator.GetCurrentAnimatorStateInfo(animationState);

    // Get animation length
    float animationLength = animationInfo.length;

    // Destroy object on animation length (seconds)
    Destroy(gameObject, animationLength);
  }
}