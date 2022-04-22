using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutotrackButton : MonoBehaviour
{
    public GameObject off;
    public GameObject on;

    void Update()
    {
        if (Autotracker.autotrack) {
            on.SetActive(true);
            off.SetActive(false);
        } else {
            on.SetActive(false);
            off.SetActive(true);
        }
    }
}
