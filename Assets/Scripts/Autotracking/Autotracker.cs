using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autotracker : MonoBehaviour
{
    public static bool autotrack = false;
    public GameObject websocket;
    public int updateInterval;
    public Character[] characters;
    public Check[] checks;
    public Esper espers;
    byte[] MASK = {0x1,0x2,0x4,0x8,0x10,0x20,0x40,0x80}; 
    int counter;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (websocket.activeSelf == false) {
                websocket.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.End)) {
            websocket.SetActive(false);
            autotrack = false;
        }
    }

    void FixedUpdate()
    {
        counter += 1;
        if (counter % updateInterval == 0 && autotrack) {
            StartCoroutine(AutoTrack());
        }
    }

    IEnumerator AutoTrack() {
        QUSBWS.GetAddress("F51EDC","2"); // Characters
        yield return new WaitForSeconds(1.5f);
        byte[] data = QUSBWS.data;
        for (int i = 0; i < characters.Length; i++) {
            if (Bits.GetBit(data, characters[i].bitNum)) {
                if (!characters[i].obtained) {
                    characters[i].Track();
                }
            } else {
                if (characters[i].obtained) {
                    characters[i].Track();
                }
            }
        }
        QUSBWS.GetAddress("F51A69","4"); // Espers
        yield return new WaitForSeconds(1.5f);
        data = QUSBWS.data;
        espers.SetZero();
        for (int i = 0; i < 32; i++) {
            if (Bits.GetBit(data,i)) {
                espers.Add();
            }
        }
        QUSBWS.GetAddress("F51E80","DF"); // Checks, Quests, and Dragons
        yield return new WaitForSeconds(1.5f);
        data = QUSBWS.data;
        for (int i = 0; i < checks.Length; i++) {
            checks[i].Reset();
            int bit = Int32.Parse(checks[i].address, System.Globalization.NumberStyles.HexNumber);
            int index = bit / 8;
            if ((data[index] & MASK[bit%8]) == MASK[bit%8]) {
                checks[i].Toggle();
            }
        }
    }
}
