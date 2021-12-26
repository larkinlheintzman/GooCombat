using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCenter : MonoBehaviour
{

  public List<GameObject> orbs;
  private Orb[] orbTypes;
  public int number = 0;
  [Range(0, 200)]
  public int desiredNumber = 0;

  public virtual void Awake()
  {
    orbs = new List<GameObject>();
    orbTypes = Resources.LoadAll<Orb>("Orbs");
  }

  public void AddOrb()
  {
    // add an orb
    GameObject newOrb = new GameObject();
    Vector2 randOffset = 0.1f*Random.insideUnitCircle;
    newOrb.transform.position += new Vector3(randOffset.x, randOffset.y, 0.0f) + transform.position;
    OrbManager orbManager = newOrb.AddComponent<OrbManager>();
    orbManager.UpdateComponents(this, Instantiate(orbTypes[Random.Range(0, orbTypes.Length)]));
    orbs.Add(newOrb);
    number = number + 1;
    return;
  }

  public virtual void AddOrb(Orb orbToAdd)
  {
    // add an orb
    GameObject newOrb = new GameObject();
    Vector2 randOffset = 0.1f*Random.insideUnitCircle;
    newOrb.transform.position += new Vector3(randOffset.x, randOffset.y, 0.0f) + transform.position;
    OrbManager orbManager = newOrb.AddComponent<OrbManager>();
    orbManager.UpdateComponents(this, Instantiate(orbToAdd));
    orbs.Add(newOrb);
    number = number + 1;
    desiredNumber = desiredNumber + 1;
    return;
  }

  public void RemoveOrb()
  {
    GameObject orbToRemove = orbs[number - 1];
    orbs.Remove(orbToRemove);
    Destroy(orbToRemove);
    number = number - 1;
    return;
  }

  // Update is called once per frame
  public virtual void Update()
  {
    if (number < desiredNumber)
    {
      AddOrb();
    }
    else if (number > desiredNumber)
    {
      RemoveOrb();
    }
  }

  public void OnDestroy()
  {
    foreach(GameObject urb in orbs)
    {
      // clean up clean up errybody
      Destroy(urb);
    }
  }
}
