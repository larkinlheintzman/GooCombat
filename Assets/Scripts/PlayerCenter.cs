using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCenter : OrbCenter
{

  public bool gameStarted = false;
  private bool oldGameStarted = false;
  public int money = 500;
  public int oldMoney = 0;


  public override void Awake()
  {
    base.Awake(); // pre load lists n that
    // since we're the player we need to stick around
    DontDestroyOnLoad(gameObject);
  }

  public override void AddOrb(Orb orbToAdd)
  {
    base.AddOrb(orbToAdd);
    // move orb to starting position if game hasnt started
    if (!gameStarted)
    {
      OrbDropper drop = FindObjectOfType<OrbDropper>();
      if (drop!=null) orbs[orbs.Count - 1].transform.position = drop.transform.position + new Vector3(Random.value*0.0001f, Random.value*0.0001f, 0.0f);
    }
    // this orb is coming with us
    DontDestroyOnLoad(orbs[orbs.Count-1]);
  }

  public override void Update()
  {
    base.Update(); // handles adding orbs n that
    // do something only the player cluster needs to
    if (number == 0 && gameStarted)
    {
      // need a way to call a game over, even though you start with zero orbs...
      SceneManager.LoadScene("LossScene");
      gameStarted = false;

    }

    // questionable!!
    if (oldGameStarted != gameStarted && gameStarted)
    {
      // entered a game
      oldGameStarted = gameStarted;
      oldMoney = money;
    }

    oldGameStarted = gameStarted;

  }
}
