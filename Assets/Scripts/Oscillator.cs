using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Oscillator", menuName = "Orb/Oscillator")]
public class Oscillator : Orb
{
    [Header("Oscillator Additions")]
    public float oscillationSpeed;
    public float oscillationSize;
}
