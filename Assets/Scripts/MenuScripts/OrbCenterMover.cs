using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCenterMover : MonoBehaviour
{
    private PlayerCenter playerCenter;
    public Vector2 targetPosition = Vector2.zero;

    public void Awake()
    {
      playerCenter = FindObjectOfType<PlayerCenter>();
    }

    public void Update()
    {
      if (playerCenter != null) playerCenter.transform.position += 0.1f*(new Vector3(targetPosition.x, targetPosition.y, playerCenter.transform.position.z) - playerCenter.transform.position);
    }
}
