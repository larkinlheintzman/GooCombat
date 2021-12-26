using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Orb", menuName = "Orb/Orb")]
public class Orb : ScriptableObject
{
  public new string name = "Derek";
  public Color color = Color.red;
  public float life = 1.0f;
  public float maxLife = 10.0f;
  public float damage = 1.0f; // damage per tick
  public float startRadius = 1.0f; // start size, they shrink on life loss
  public float radius = 1.0f; // display radius and rb collider radius
  public float senseRadius = 1.0f; // trigger collider radius
  public float thickness = 0.25f; // displayed line thickness
  public float mass = 1.0f;
  public float drag = 0.25f;
  public float hardness = 1.0f; // tbd
  public int cost = 1; // how much to purchase when building
  public int value = 1; // how much earned from slaying
  public PhysicsMaterial2D physMaterial; // physics characteristics
}
