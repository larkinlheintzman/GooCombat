using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrbDropper : MonoBehaviour
{
  // :)
  // click and draggable dropper that determines where shop orbs pop up first
    public float frequency = 0.65f;
    public float damping = 0.80f;
    public float gridSize = 1.00f;
    public Rigidbody2D rb;
    public Vector3 mousePosition = Vector3.zero;
    public bool isHeld;
    public Vector3 center = Vector3.right*4f;

    public void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    public Vector3 GridPosition(Vector3 testPosition)
    {
      return new Vector3(Mathf.Round(testPosition.x*gridSize)/gridSize, Mathf.Round(testPosition.y*gridSize)/gridSize, Mathf.Round(testPosition.z*gridSize)/gridSize);
    }

    public void FixedUpdate()
    {
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
        Vector3 Vdes = Vector3.zero;


        Vector3 Pdes = GridPosition(transform.position);
        if (isHeld) Pdes = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        else if (Vector3.Distance(transform.position, center) > 10f) Pdes = 10.0f*((transform.position - center).normalized) + center;
        Pdes.z = transform.position.z;
        Vector3 F = (Pdes - Pt0) * ksg + (Vdes - Vt0) * kdg;
        if (!isHeld && Vector3.Distance(transform.position, center) < 10f)
        {
          F *= 0.0f;
        }
        rb.AddForce (F);
    }



    public void OnMouseDown()
    {
      if (Input.GetMouseButtonDown(0))
      {
        isHeld = true;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      }
    }

    public void OnMouseDrag()
    {
      mousePosition = Input.mousePosition;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      return;
    }

    public void OnMouseUp()
    {
      mousePosition = Vector3.zero;
      isHeld = false;
    }



}
