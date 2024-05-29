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
            transform.parent.gameObject.SetActive(false);
        }
    }
}
