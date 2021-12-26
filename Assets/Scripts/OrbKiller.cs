using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbKiller : OrbBehavior
{

  public void Update()
  {
    if (!init) return;
    if (orb.life <= 0.0f)
    {
      orb.life = 0.0f;
      // goodbye cruel world
      // wait need to remove myself from the list too
      if (center != null)
      {
        center.orbs.Remove(gameObject);

        // decrement counters
        center.desiredNumber -= 1;
        center.number -= 1;
        // ok now bon voyage
        Destroy(gameObject);
      }
    }
  }

}
