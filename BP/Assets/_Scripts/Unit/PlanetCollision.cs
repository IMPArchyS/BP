using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("starHitbox"))
        {
            Debug.Log("CS-OBJ: " + gameObject.name + " -> HIT BY STAR");
            // try get component planet
            if (gameObject.transform.parent.name == "Earth")
            {
                GameObject moon = GameObject.Find("Moon");
                moon.SetActive(false);
            }
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("planetHitbox") && other.gameObject != gameObject)
        {
            SolarSystem.Instance.MoonFormation(other.transform.parent.gameObject.GetComponent<Planet>());
        }
    }
}
