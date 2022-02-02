using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int[,] map = new int[8,8];

    public static Tile Decode(byte[] data) { // 32 bytes only! returns the 4bpp format for the tile. Refer to this: https://www.ff6hacking.com/forums/thread-3617.html
        Tile tile = new Tile();
        byte[][] pairs = new byte[8][];
        for (int i = 0; i < 8; i++) { // Sort bytes into pairs
            byte[] pair = new byte[4];
            pair[0] = data[i*2];
            pair[1] = data[i*2+1];
            pair[2] = data[i*2+16];
            pair[3] = data[i*2+17];
            pairs[i] = pair;
        }
        for (int y = 0; y < 8; y++) { // Get Pallette IDs by overlaying bytes 
            for (int x = 7; x >= 0; x--) { // Iterate bits
                int value = 0;
                if (Bits.GetBit(pairs[y],x+24)) {value += 1;}
                if (Bits.GetBit(pairs[y],x+16)) {value += 2;} 
                if (Bits.GetBit(pairs[y],x+8)) {value += 4;}
                if (Bits.GetBit(pairs[y],x)) {value += 8;}
                tile.map[7-x,y] = value;
            }
        }
        return tile;
    }
}
