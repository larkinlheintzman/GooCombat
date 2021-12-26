using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSizer : OrbBehavior
{
  public float inflation = 1.0f;
  public override void Init(OrbCenter parent, Orb orbObj)
  {
    base.Init(parent, orbObj);
    inflation = orb.radius;
    orb.radius = orb.startRadius - inflation;

  }
  public void Update()
  {
    if (!init) return;
    if (inflation > 0f)
    {
      inflation -= 1f*Time.deltaTime;
      orb.radius = orb.startRadius - inflation;
    }
    else
    {
      orb.radius = orb.startRadius - (orb.startRadius/0.9f)*(1f - (orb.life/orb.maxLife));
    }


  }
}
