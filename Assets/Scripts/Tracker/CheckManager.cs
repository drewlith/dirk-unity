using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
    int totalDone;
    int total;
    Check[] all;

    void FixedUpdate() {
        ManageTracker();
    }

    public void ManageTracker() {
        all = transform.GetComponentsInChildren<Check>();
        total = 0;
        totalDone = 0;
        float x = 0;
        float y = 0;
        for (int i = 0; i < all.Length; i++) {
            if (all[i].available) {
                total += 1;
                if (all[i].done) {
                    totalDone += 1;
                }
                all[i].transform.localPosition = new Vector3(x,y,0);
                y = y - 0.6f;
                if (total == 22) {
                    x = 7f;
                    y = 0;
                }
            } else {
                all[i].transform.position = new Vector3(0,30,0);
            }
        }
    }
}
