using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esper : MonoBehaviour
{
    SpriteRenderer r;
    static int count;
    TextMesh t;
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        t = GetComponentInChildren<TextMesh>();
        t.text = count.ToString();
        count = 0;
        CreateSprite();
    }

    public void Add() {
        if (count < 27) {
            count += 1;
        }
        t.text = count.ToString();
    }

    public void Subtract() {
        if (count > 0) {
            count -= 1;
        }
        t.text = count.ToString();
    }

    public void SetZero() {
        count = 0;
        t.text = count.ToString();
    }

    void CreateSprite() {   
        Palette palette = ROM.PALETTES[2];
        Tile[] tiles = ROM.TILESETS[name];
        tiles = new Tile[] {tiles[3], tiles[4], tiles[1], tiles[2]}; // Rearrange tiles.
        Texture2D texture = new Texture2D(16,16);
        int k = 2;
        for (int i = 0; i < tiles.Length; i++) {
            if (i % 2 == 0 && i > 0) {k--;}
            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    Color32 pixelColor;
                    if (tiles[i].map[x,y] == 0) {
                        pixelColor = new Color32(0,0,0,0);
                    } else {
                        pixelColor = palette.colors[tiles[i].map[x,y]];
                    }
                    texture.SetPixel(x+((i*8)%16),(7-y)+(k*8),pixelColor);
                }
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        r.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width,texture.height), new Vector2(0.5f, 0.5f), 12);                                        
    }
}
