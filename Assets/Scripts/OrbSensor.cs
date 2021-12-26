using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSensor : OrbBehavior
{

  [Header("Sensing")]
  public Transform nearestAlly;
  public Transform nearestEnemy;
  private GameObject sensorObject;
  private CircleCollider2D sensorCol;
  public Collider2D[] nearby;
  public int nearbySize = 10;

  public override void Init(OrbCenter parent, Orb orb)
  {
    base.Init(parent, orb);
    if (!sensorObject) sensorObject = new GameObject();
    sensorObject.transform.parent = transform;
    sensorObject.transform.localPosition = Vector3.zero;
    sensorObject.transform.localScale = Vector3.one;
    sensorObject.layer = 3; // sensor layer
    sensorObject.name = orb.name + "_sensor";
    if (!sensorCol) sensorCol = sensorObject.AddComponent<CircleCollider2D>();
    sensorCol.isTrigger = true;
    sensorCol.radius = orb.senseRadius;
    sensorCol.offset = Vector3.zero;
    nearby = new Collider2D[nearbySize];
  }

  public void Update()
  {
    if (!init) return;
    sensorCol.radius = orb.senseRadius;
    // check for things nearby
    ContactFilter2D contactFilter = new ContactFilter2D();
    contactFilter.layerMask = ~LayerMask.GetMask("Sensor");
    // clear out array otherwise
    int hits = sensorCol.OverlapCollider(contactFilter, nearby);
    for(int i = 0; i < hits; i++)
    {
      if (nearby[i].gameObject.transform.IsChildOf(transform)) nearby[i] = null;
    }
    for(int i = hits; i < nearbySize; i++)
    {
      nearby[i] = null;
    }
  }

  public void OnDestroy()
  {
    Destroy(sensorObject);
  }

}
