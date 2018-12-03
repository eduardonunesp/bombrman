using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
  void Awake()
  {
    GameObject boardManager = GameObject.Find("BoardManager");

    if (!boardManager)
    {
      Debug.LogWarning("You need to add Prefab BoardManager");
      return;
    }

    //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
    if (BoardManager.instance == null)
    {

      //Instantiate gameManager prefab
      Instantiate(boardManager);
    }
  }
}
