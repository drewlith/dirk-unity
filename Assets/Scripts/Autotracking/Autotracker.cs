using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autotracker : MonoBehaviour
{
    public static bool autotrack;
    public int updateInterval;
    public Character[] characters;
    public Esper espers;
    int counter;

    void FixedUpdate()
    {
        counter += 1;
        if (counter % updateInterval == 0 && autotrack) {
            StartCoroutine(AutoTrack());
        }
    }

    IEnumerator AutoTrack() {
        QUSBWS.GetAddress("F51EDC","2"); // Characters
        yield return new WaitForSeconds(1f);
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
        yield return new WaitForSeconds(1f);
        data = QUSBWS.data;
        espers.SetZero();
        for (int i = 0; i < 32; i++) {
            if (Bits.GetBit(data,i)) {
                espers.Add();
            }
        }
    }
}
