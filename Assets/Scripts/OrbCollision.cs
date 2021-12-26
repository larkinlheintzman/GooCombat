using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollision : OrbBehavior
{

  public void CheckDeath(OrbCollision enemyOrb)
  {
    // if we die, need to give money to killer
    if (orb.life <= 0.0f)
    {
      if (enemyOrb.center is PlayerCenter && !(center is PlayerCenter))
      {
        (enemyOrb.center as PlayerCenter).money += orb.value;
      }
    }
  }

  public void DoCollision(Collision2D hitInfo)
  {
    // when hitting something, check if it's target is the same as yours, ignore it if it is
    OrbCollision enemyOrb = hitInfo.gameObject.GetComponent<OrbCollision>();
    if (enemyOrb != null)
    {
      // check if actually enemy
      if (!GameObject.ReferenceEquals(enemyOrb.center.gameObject, center.gameObject))
      {
        // tis enemy, do dambah
        enemyOrb.orb.life -= orb.damage;
        orb.life -= enemyOrb.orb.damage;

        CheckDeath(enemyOrb);
      }
    }
  }

  public void OnCollisionEnter2D(Collision2D hit)
  {
    DoCollision(hit);
  }


  public void OnCollisionStay2D(Collision2D stay)
  {
    DoCollision(stay);
  }


}
