using System.Collections.Generic;
using UnityEngine;

public class FindChildrenWithTag : MonoBehaviour
{
    public string searchTag;
    public List<GameObject> objects = new();

    private void Awake()
    {
        if (searchTag != null)
            FindObjectWithTag(searchTag);
    }

    public void FindObjectWithTag(string tag)
    {
        objects.Clear();
        Transform parent = transform;
        GetChildObject(parent, tag);
    }

    public void GetChildObject(Transform parent, string tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tag))
                objects.Add(child.gameObject);

            if (child.childCount > 0)
                GetChildObject(child, tag);
        }
    }
}
