using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDisplay : OrbBehavior
{

  [Header("Display")]
  public bool hoovered = false;
  private LineRenderer lineRenderer;
  private float theta_scale = 0.025f;
  private float lastLife = 1.0f;
  private float noiseAmount = 0.0f;
  private float oldThickness = 0.0f;

  private float rippleAmplitude = 0.5f;
  private float rippleFrequency = 5f;
  private float relaxSpeed = 0.05f;

  public override void Init(OrbCenter parent, Orb orb)
  {
    base.Init(parent, orb);

    if (!lineRenderer)
    {
      lineRenderer = gameObject.AddComponent<LineRenderer>();
      lineRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
    }

    float sizeValue = (2.0f * Mathf.PI + theta_scale) / theta_scale;

    oldThickness = orb.thickness;

    lineRenderer.startWidth = orb.thickness;
    lineRenderer.endWidth = orb.thickness;
    lineRenderer.positionCount = (int)sizeValue + 1;
    lineRenderer.startColor = orb.color;
    lineRenderer.endColor = orb.color;

  }

  public void Update()
  {
    if (!init) return;
    // draw le orb
    float theta = 0f;
    Vector3[] positions = new Vector3[lineRenderer.positionCount];
    if (orb.life < lastLife)
    {
      // we been hit  man
      noiseAmount += Time.deltaTime*(lastLife - orb.life);
    }

    if (hoovered || isHeld)
    {
      orb.thickness += relaxSpeed*((oldThickness + 0.15f) - orb.thickness);
    }
    else
    {
      orb.thickness += relaxSpeed*((oldThickness) - orb.thickness);
    }

    if (hoovered && !isHeld)
    {
      hoovered = false;
    }

    for (int i = 0; i < lineRenderer.positionCount; i++)
    {
        theta += theta_scale;
        float displayRadius = orb.radius - orb.thickness/2f;
        float x = orb.radius * Mathf.Cos(theta) + noiseAmount*Mathf.PerlinNoise(Mathf.Cos(theta) + Time.time*3f, Mathf.Sin(theta) + Time.time*3f) - noiseAmount/2f;
        float y = orb.radius * Mathf.Sin(theta) + noiseAmount*Mathf.PerlinNoise(Mathf.Cos(theta-1f) + Time.time*3f, Mathf.Sin(theta-1f) + Time.time*3f) - noiseAmount/2f;
        x += gameObject.transform.position.x;
        y += gameObject.transform.position.y;
        positions[i] = new Vector3(x, y, transform.position.z);
    }
    lineRenderer.SetPositions(positions);

    // getting to cute with it
    int n = 10;
    if (isHeld || hoovered) n = 50;
    AnimationCurve curve = new AnimationCurve();
    float rippleTheta = Time.time%(Mathf.PI*2f);
    for(int i = 0; i < n; i++)
    {
      curve.AddKey((float)i/(n-1), rippleAmplitude*(Mathf.Sin(rippleTheta) + 1f)*(orb.thickness - oldThickness) + orb.thickness);
      rippleTheta += (2f*Mathf.PI*rippleFrequency)/(n-1f);
    }
    lineRenderer.widthCurve = curve;

    lastLife = orb.life;
    if (noiseAmount > 0f) noiseAmount -= 2f*Time.deltaTime;
  }

  private void OnMouseOver()
  {
    // indicate hovering
    if (center is PlayerCenter)
    {
      // further only apply offsets if game has not started
      if (!(center as PlayerCenter).gameStarted)
      {
        hoovered = true;
      }
    }
  }

  private void OnMouseExit()
  {
    // indicate hovering
    if (center is PlayerCenter)
    {
      if (!isHeld) hoovered = false;
    }
  }
}
