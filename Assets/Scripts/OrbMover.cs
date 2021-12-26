using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMover : OrbBehavior
{

  [Header("Movement")]
  public float frequency = 0.65f;
  public float damping = 0.80f;
  public float gridSize = 1.00f;
  private Vector3 mousePosition = Vector3.zero;

  public override void Init(OrbCenter parent, Orb orbObj)
  {
    base.Init(parent, orbObj);
    rb.mass = orb.mass;
    rb.drag = orb.drag;
    rb.gravityScale = 0.0f;
    rb.sharedMaterial = orb.physMaterial;
    col.radius = orb.radius;
    col.offset = Vector3.zero;
    col.sharedMaterial = orb.physMaterial;
  }

  public Vector3 GridPosition(Vector3 testPosition)
  {
    return new Vector3(Mathf.Round(testPosition.x*gridSize)/gridSize, Mathf.Round(testPosition.y*gridSize)/gridSize, Mathf.Round(testPosition.z*gridSize)/gridSize);
  }

  public void FixedUpdate()
  {
    if (!init) return;
    // psh "add force" get a load of this guy
    // rb.AddForce((center.transform.position - transform.position).normalized*orb.speed);
    float kp = (6f*frequency)*(6f*frequency)* 0.25f;
    float kd = 4.5f*frequency*damping;

    float dt = Time.fixedDeltaTime;
    float g = 1 / (1 + kd * dt + kp * dt * dt);
    float ksg = kp * g;
    float kdg = (kd + kp * dt) * g;
    Vector3 Pt0 = transform.position;
    Vector3 Vt0 = rb.velocity;
    Vector3 Pdes = center.transform.position;

    // apply orb center offsets if player
    if (center is PlayerCenter)
    {
      // further only apply offsets if game has not started
      if (!(center as PlayerCenter).gameStarted)
      {
        // Vector2 offset2D = (center as PlayerCenter).offsets[gameObject];
        if (!isHeld)
        {
          Pdes = GridPosition(transform.position);
          if (Vector3.Distance(Pdes, center.transform.position) > 10.0f)
          {
            Pdes = Vector3.Normalize(Pdes)*10.0f;
          }
        }
        else
        {
          mousePosition = Input.mousePosition;
          mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

          Pdes = GridPosition(new Vector3(mousePosition.x, mousePosition.y, 1.5f));
          Pdes.z = 1.5f; // just in case
          kp = (6f*(frequency + 1f))*(6f*(frequency + 1f))* 0.25f;
          kd = 4.5f*(frequency + 1f)*damping;
          dt = Time.fixedDeltaTime;
          g = 1 / (1 + kd * dt + kp * dt * dt);
          ksg = kp * g;
          kdg = (kd + kp * dt) * g;
        }
      }
    }

    Vector3 Vdes = Vector3.zero;
    Vector3 F = (Pdes - Pt0) * ksg + (Vdes - Vt0) * kdg;
    rb.AddForce (F);

    rb.mass = orb.mass;
    rb.drag = orb.drag;
    col.radius = orb.radius;
  }

}
