using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrbMover)), RequireComponent(typeof(OrbDisplay)), RequireComponent(typeof(OrbSensor)), RequireComponent(typeof(OrbSizer)), RequireComponent(typeof(OrbCollision)), RequireComponent(typeof(OrbKiller))]
public class OrbManager : MonoBehaviour
{

  public void UpdateComponents(OrbCenter parent, Orb orb)
  {

    OrbMover mover = gameObject.GetComponent<OrbMover>();
    OrbDisplay display = gameObject.GetComponent<OrbDisplay>();
    OrbSensor sensor = gameObject.GetComponent<OrbSensor>();
    OrbSizer sizer = gameObject.GetComponent<OrbSizer>();
    OrbCollision collider = gameObject.GetComponent<OrbCollision>();
    OrbKiller death = gameObject.GetComponent<OrbKiller>();

    mover.Init(parent, orb);
    display.Init(parent, orb);
    sensor.Init(parent, orb);
    sizer.Init(parent, orb);
    collider.Init(parent, orb);
    death.Init(parent, orb);

    // now we need to check what type this orb is, and add the corresponding component
    if (orb is Oscillator)
    {
      OrbOscillator oscillator = gameObject.AddComponent<OrbOscillator>();
      // add oscillator component: ??
      oscillator.Init(parent, orb as Oscillator);
    }

    gameObject.name = orb.name;
  }

}
