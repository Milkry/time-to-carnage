using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    /// <summary>
    /// Finds the first child of a parent object that matches the tag
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>Returns true if found. False if not.</returns>
    public static bool FindChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Finds the first child of a parent object that matches the tag
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns>Returns the child gameobject. Null if not found</returns>
    public static GameObject FindChildWithTagAndReturnIt(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.CompareTag(tag))
            {
                return t.GetChild(i).gameObject;
            }
        }
        return null;
    }
}
