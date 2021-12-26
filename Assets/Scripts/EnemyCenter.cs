using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCenter : OrbCenter
{

  PlayerCenter player;

  public override void Awake()
  {
    base.Awake();
    player = FindObjectOfType<PlayerCenter>();
  }

  public override void Update()
  {
    base.Update(); // handles adding orbs n that
    // do something only the player cluster needs to
    if (number == 0)
    {
      // print("enemy dead!");
      // trigger scene change back to builder menu
      if (player == null) player = FindObjectOfType<PlayerCenter>();
      player.gameStarted = false;
      SceneManager.LoadScene("WinScene");
    }

    // run at player
    if (player == null) player = FindObjectOfType<PlayerCenter>();
    transform.position += 0.1f*(player.transform.position - transform.position);

  }
}
