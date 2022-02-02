using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette
{
    public Color32[] colors = new Color32[16];

    public static Palette Decode(byte[] data) { // Should be 32 bytes (16 colors, 2 bytes per color).
        Palette palette = new Palette();
        for (int i = 0; i < palette.colors.Length; i++) {
            // Colors can be an intensity from 0-31. SNES uses a "BGR" Format but we're gonna use RGB, since our bit utility pulls from right to left anyways. MSB is ignored.
            byte[] colorData = new byte[]{0,0};
            colorData[0] = data[i*2+1];
            colorData[1] = data[i*2];
            palette.colors[i].r = (byte)(Bits.GetValue(colorData, 0, 5) * 8);
            palette.colors[i].g = (byte)(Bits.GetValue(colorData, 5, 5) * 8);
            palette.colors[i].b = (byte)(Bits.GetValue(colorData, 10, 5) * 8);
            palette.colors[i].a = 0xFF;
        }
        return palette;
    }
}
