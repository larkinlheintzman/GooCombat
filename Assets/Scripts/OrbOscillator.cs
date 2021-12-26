using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrbMover))]
public class OrbOscillator : OrbBehavior
{
  private Oscillator osc; // local type copy
  private float uniqueOffset = 0.0f;

  public override void Init(OrbCenter parent, Orb orb)
  {
    base.Init(parent, orb);
    osc = (Oscillator)orb;
    uniqueOffset = 1000.0f*Random.value;
  }

  public void Update()
  {
    if (!init) return;
    osc.radius += osc.oscillationSize*Mathf.Sin(Time.time*osc.oscillationSpeed + uniqueOffset);
  }

}
