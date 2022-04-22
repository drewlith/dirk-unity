using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    TextMesh t;
    public bool available;
    public bool done;
    bool soft;
    Transform checkMark;
    public bool dragon;
    public bool quest;
    public string address;
    public static int total;
    void Start()
    {
        t = GetComponent<TextMesh>();
        t.text = name;
        gameObject.AddComponent<BoxCollider2D>();
        checkMark = transform.GetChild(0);
    }

    public void Toggle() {
        if (!done) {
            done = true;
            if (!quest) {
                total += 1;
            }
            soft = false;
            t.color = Color.yellow;
            checkMark.gameObject.SetActive(true);
        } else {
            Reset();
        }
    }

    public void Reset() {
        if (done && !quest) {
            total -= 1;
        }
        done = false;
        t.color = Color.white;
        checkMark.gameObject.SetActive(false);
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            Toggle();
        }
        if (Input.GetMouseButtonDown(1)) {
            Soft();
        }
    }

    void Soft() {   // Checks can be marked "soft-checked" if you right click them, typically done if the player
                    // peeks a check or has decided to skip a check.
        if (done) {return;}
        if (soft) {
            t.color = Color.white;
            soft = false;
        } else {
            t.color = Color.gray;
            soft = true;
        }
    }

}
