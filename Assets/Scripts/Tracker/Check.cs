using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    TextMesh t;
    public bool done;
    public bool soft;
    static int total;
    GameObject checkMark;
    void Start()
    {
        t = GetComponent<TextMesh>();
        t.text = name;
        gameObject.AddComponent<BoxCollider2D>();
        checkMark = transform.GetChild(0).gameObject;
        checkMark.transform.position = new Vector3(transform.position.x - 0.28f, transform.position.y - 0.28f, 0);
        checkMark.SetActive(false);
    }

    // Update is called once per frame
    void Toggle() {
        if (!done) {
            done = true;
            soft = false;
            total += 1;
            t.color = Color.yellow;
            checkMark.SetActive(true);
        } else {
            done = false;
            total -= 1;
            t.color = Color.white;
            checkMark.SetActive(false);
        }
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
