using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTile : MonoBehaviour
{
    public int world; // 0 = Balance, 1 = Ruin, 2 = Either
    TextMesh t;
    Color color;
    public Check c;
    BoxCollider2D bc;
    string checkName;
    public static bool disabled;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        t = GetComponent<TextMesh>();
        checkName = t.text;
        if (world == 0) {
            color = new Color (0,1,0,0.75f);
        } else if (world == 1) {
            color = new Color (1,0,0,0.75f);
        } else {
            color = new Color (1,1,1,0.75f);
        }
    }

    void Update() {
        if (disabled) {
            t.text = "";
            bc.enabled = false;
        } else {
            if (c.available) {
                t.text = checkName;
                bc.enabled = true;
            } else {
                t.text = "";
                bc.enabled = false;
            }
            if (c.done) {
                t.color = Color.yellow;
            } else {
                t.color = color;
            }
        }

    }

    void OnMouseDown() {
        c.Toggle();
    }

}
