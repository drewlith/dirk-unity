using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCount : MonoBehaviour
{
    TextMesh t;
    void Start() {
        t = GetComponent<TextMesh>();
    }
    void Update()
    {
        t.text = Check.total.ToString();
    }
    void OnMouseOver() {
        if (Autotracker.autotrack) {return;}
        if (Input.GetMouseButtonDown(0)) {
            if (Check.total < 49) {
                Check.total += 1;
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            if (Check.total > 0) {
                Check.total -= 1;
            }
        }
    }
}
