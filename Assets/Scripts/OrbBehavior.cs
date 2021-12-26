using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class OrbBehavior : MonoBehaviour
{
  // base shit to track for something that modifies behavior of orb
  public Orb orb;
  public Rigidbody2D rb;
  public CircleCollider2D col;
  public OrbCenter center; // current center, can be swapped
  public bool isHeld = false;
  public bool init = false;

  public virtual void Init(OrbCenter parent, Orb orbObj)
  {
    if (!rb) rb = gameObject.GetComponent<Rigidbody2D>();
    if (!col) col = gameObject.GetComponent<CircleCollider2D>();
    orb = orbObj;
    rb.sharedMaterial = orb.physMaterial;
    col.sharedMaterial = orb.physMaterial;
    center = parent;
    init = true;
  }

  public virtual void OnMouseDown()
  {
    if (Input.GetMouseButtonDown(0))
    {
      isHeld = true;
    }
  }

  public virtual void OnMouseDrag()
  {
    return;
  }

  public virtual void OnMouseUp()
  {
    isHeld = false;
  }
}
