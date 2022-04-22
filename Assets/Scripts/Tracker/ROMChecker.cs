using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROMChecker : MonoBehaviour
{
    public GameObject noRomMessage;
    public GameObject tracker;
    bool activatedTracker;
    public static bool reset;

    void Update()
    {
        if (ROM.romDataExists && !activatedTracker) {
            noRomMessage.SetActive(false);
            tracker.SetActive(true);
            activatedTracker = true;
        }
        if (reset) {
            tracker.SetActive(false);
            activatedTracker = false;
            reset = false;
        }
    }
}
