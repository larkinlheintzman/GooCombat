using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbStarter : MonoBehaviour
{
  public PlayerCenter playerCenter;

  public void GameStart()
  {
    if (playerCenter != null) playerCenter.gameStarted = true;
    else
    {
      playerCenter = FindObjectOfType<PlayerCenter>();
      if (playerCenter != null) playerCenter.gameStarted = true;
    }
  }
}
