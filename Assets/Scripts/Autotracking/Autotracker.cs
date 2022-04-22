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
    public BossCount bossCount;
    public TreasureCount treasureCount;
    public Esper espers;
    public Dragons dragons;
    byte[] MASK = {0x1,0x2,0x4,0x8,0x10,0x20,0x40,0x80}; 
    int counter;

    void FixedUpdate()
    {
        counter += 1;
        if (counter % updateInterval == 0 && autotrack) {
            StartCoroutine(AutoTrack());
        }
    }

    public void StartAutotracker() {
        if (websocket.activeSelf == false) {
            websocket.SetActive(true);
        } else {
            websocket.SetActive(false);
            autotrack = false;
        }
    }

    IEnumerator AutoTrack() {

        QUSBWS.GetAddress("F51EDC","2"); // Characters
        yield return new WaitForSeconds(0.1f);
            if (!autotrack) {
                yield break;
            }
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
        yield return new WaitForSeconds(0.1f);
            if (!autotrack) {
                yield break;
            }
        data = QUSBWS.data;
        espers.SetZero();
        for (int i = 0; i < 32; i++) {
            if (Bits.GetBit(data,i)) {
                espers.Add();
            }
        }

        QUSBWS.GetAddress("F51E80","DF"); // Checks & Dragons
        yield return new WaitForSeconds(0.1f);
            if (!autotrack) {
                yield break;
            }
        data = QUSBWS.data;
        dragons.SetZero();
        for (int i = 0; i < checks.Length; i++) {
            checks[i].Reset();
            int bit = Int32.Parse(checks[i].address, System.Globalization.NumberStyles.HexNumber);
            int index = bit / 8;
            if ((data[index] & MASK[bit%8]) == MASK[bit%8]) {
                checks[i].Toggle();
                if (checks[i].dragon) {
                    dragons.Add();
                }
            }
        }
        
        QUSBWS.GetAddress("F51E40","2F"); // Treasures
        yield return new WaitForSeconds(0.1f);
            if (!autotrack) {
                yield break;
            }
        data = QUSBWS.data;
        treasureCount.SetZero();
        for (int i = 0; i < 0x2F*8; i++) {
            if (Bits.GetBit(data,i)) {
                treasureCount.Add();
            }
        }
        
        QUSBWS.GetAddress("F51FF8","1"); // Bosses
        yield return new WaitForSeconds(0.1f);
            if (!autotrack) {
                yield break;
            }
        data = QUSBWS.data;
        bossCount.SetTotal(data[0]);
    }
    
}
