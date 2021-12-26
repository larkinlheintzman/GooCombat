using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ClickAndDrag : MonoBehaviour
{
  public Vector3 mousePosition = Vector3.zero;
  public Vector3[] pts = new Vector3[5];
  public List<Collider2D> heldSet = new List<Collider2D>();
  public int heldSize = 20;
  public LineRenderer lr;
  public bool holdingOrbs = false;
  public Vector3 anchorPos = Vector3.zero;

  public void Awake()
  {
    lr = GetComponent<LineRenderer>();
    lr.material = new Material(Shader.Find("Particles/Standard Unlit"));
    lr.positionCount = 5;

    pts[0] = Vector3.zero;
    pts[1] = Vector3.zero;
    pts[2] = Vector3.zero;
    pts[3] = Vector3.zero;
    pts[4] = Vector3.zero;
    lr.SetPositions(pts);
  }

  public void DropHolds()
  {
    // cancel holds
    List<Collider2D> tempHeldSet = new List<Collider2D>(heldSet);
    foreach (Collider2D col in heldSet)
    {
      OrbMover mover = col.gameObject.GetComponent<OrbMover>();
      OrbDisplay display = col.gameObject.GetComponent<OrbDisplay>();
      if (mover != null)
      {
        mover.isHeld = false;
        display.hoovered = false;
        display.isHeld = false;
      }
      tempHeldSet.Remove(col);
    }
    holdingOrbs = false;
    heldSet = tempHeldSet;
  }

  public void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      if (holdingOrbs)
      {
        DropHolds();
      }
    }
  }

  public float offsetScaler = 1.0f;
  public Vector3 WidthOffset(Vector3 corner, Vector3 center)
  {
    Vector3 rel = corner - center;
    return corner + offsetScaler*rel.normalized*(lr.startWidth/2f);
  }

  public void OnMouseDown()
  {
    if (Input.GetMouseButtonDown(0))
    {
      mousePosition = Input.mousePosition;
      mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
      mousePosition.z = 1.5f;
      anchorPos = mousePosition;
      pts[0] = mousePosition;
      pts[1] = mousePosition;
      pts[2] = mousePosition;
      pts[3] = mousePosition;
      pts[4] = mousePosition;
      lr.SetPositions(pts);
    }
  }

  public void OnMouseDrag()
  {
    mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    mousePosition.z = 1.5f;
    Vector3 center3D = (anchorPos + mousePosition)/2f;
    pts[0] = WidthOffset(anchorPos, center3D);
    pts[1] = WidthOffset(new Vector3(mousePosition.x, anchorPos.y, mousePosition.z), center3D);
    pts[2] = WidthOffset(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z), center3D);
    pts[3] = WidthOffset(new Vector3(anchorPos.x, mousePosition.y, mousePosition.z), center3D);
    pts[4] = WidthOffset(new Vector3(anchorPos.x, anchorPos.y, mousePosition.z), center3D);
    lr.SetPositions(pts);
  }

  public void OnMouseUp()
  {
    // if (!Input.GetMouseButtonDown(0))
    mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    mousePosition.z = 1.5f;

    // do overlap check and flip them to holds
    Collider2D[] results = new Collider2D[20];
    Vector3 center3D = (anchorPos + mousePosition)/2f;
    Vector2 center = new Vector2(center3D.x, center3D.y);
    Vector2 size = new Vector2(Mathf.Abs(mousePosition.x - anchorPos.x), Mathf.Abs(mousePosition.y - anchorPos.y));
    float angle = 0.0f;
    ContactFilter2D contactFilter = new ContactFilter2D();
    contactFilter.layerMask = ~LayerMask.GetMask("Default");

    int numHits = Physics2D.OverlapBox(center, size, angle, contactFilter, results);

    if (!holdingOrbs)
    {
      for(int i = 0; i < numHits; i++)
      {
        OrbMover mover = results[i].gameObject.GetComponent<OrbMover>();
        OrbDisplay display = results[i].gameObject.GetComponent<OrbDisplay>();
        if (mover != null)
        {
          mover.isHeld = true;
          display.hoovered = true;
          display.isHeld = true;
          heldSet.Add(results[i]);
          holdingOrbs = true;
        }
      }
      for(int i = numHits; i < heldSize; i++)
      {
        results[i] = null;
      }
    }
    else
    {
      DropHolds();
    }

    // clear box
    pts[0] = Vector3.zero;
    pts[1] = Vector3.zero;
    pts[2] = Vector3.zero;
    pts[3] = Vector3.zero;
    pts[4] = Vector3.zero;
    anchorPos = Vector3.zero;
    lr.SetPositions(pts);
  }


}
