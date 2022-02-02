using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bits : MonoBehaviour // I don't think it needs to be a MonoBehavior but I also don't think it really matters.
{
    static byte[] MASK = {0x1,0x2,0x4,0x8,0x10,0x20,0x40,0x80}; 
    static byte[] CLEAR_MASK = {0xFE,0xFD,0xFB,0xF7,0xEF,0xDF,0xBF,0x7F};

    static int GetByteIndex(byte[] data, int bit) { // Utility
        return (data.Length - 1) - bit / 8;
    }

    public static bool GetBit(byte[] data, int bit) { // Determines if a bit set or not. Index 0 = Least Significant Bit.
        if ((data[GetByteIndex(data, bit)] & MASK[bit%8]) == MASK[bit%8]) {
            return true;
        } else {
            return false;
        }
    }

    public static byte[] SetBit(byte[] data, int bit) { // Sets bit at index "bit" (makes it 1)
        int index = GetByteIndex(data, bit);
        data[index] = (byte)(data[index] | MASK[bit%8]);
        return data;
    }

    public static byte[] ClearBit(byte[] data, int bit) { // Clears bit at index "bit" (makes it 0)
        int index = GetByteIndex(data, bit);
        data[index] = (byte)(data[index] & CLEAR_MASK[bit%8]);
        return data;
    }

    public static int GetValue(byte[] data, int offset, int size) { // Gets integer value starting at offset of specified bit size, it starts from the Least Significant Bit.
                                                                    // Example, you have a 32-bit value, you want the 8-bit value of the 3rd byte, do GetValue(data, 8, 8)
        int value = 0;
        for (int i = 0; i < size; i++) {
            if (GetBit(data, offset + i)) {
                value += (int)Math.Pow(2,i);
            }
        }
        return value;
    }

    public static byte[] SetValue(byte[] data, int offset, int size, int value) { // Sets an integer value, works similar to Get.
        for (int i = 0; i < size; i++) {
            if ((value & (int)Math.Pow(2,i)) == (int)Math.Pow(2,i)) {
                data = SetBit(data,offset+i);
            } else {
                data = ClearBit(data,offset+i);
            }
        }
        return data;
    }
    
}
