using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    SpriteRenderer r;
    static int total;
    public int paletteNumber;
    public int bitNum;
    public bool obtained;
    public Check[] myChecks;
    int spriteID;
    Sprite[] sprites = new Sprite[20];
    int counter;
    BoxCollider2D coll;

    void Start() {
        r = GetComponent<SpriteRenderer>();
        CreateSprites();
        r.sprite = sprites[0];
        coll = gameObject.AddComponent<BoxCollider2D>();
    }

    void Update() {
        if (obtained) {
            r.color = new Color(1f,1f,1f,1f);
        } else {
            r.color = new Color(0.3f,0.3f,0.3f,1f);
        }
    }

    public void Track() { 
        if (obtained) {
            obtained = false;
            for (int i = 0; i < myChecks.Length; i++) {
                myChecks[i].available = false;
                myChecks[i].done = false;
            }
            total -= 1;
        } else {
            obtained = true;
            for (int i = 0; i < myChecks.Length; i++) {
                myChecks[i].available = true;
            }
            total += 1;
        }
    }

    void OnMouseDown() {
        Track();
    }

    //  Creates a 16x24 sprite using 6 tiles.
    //  [0][1]
    //  [2][3]
    //  [4][5]
    void CreateSprite(int[] tileIDs, int spriteID) {   
        Palette palette = ROM.PALETTES[paletteNumber];
        Tile[] all = ROM.TILESETS[name];
        Tile[] tiles = new Tile[] {all[tileIDs[0]],all[tileIDs[1]],all[tileIDs[2]],all[tileIDs[3]],all[tileIDs[4]],all[tileIDs[5]]}; 
        Texture2D texture = new Texture2D(16,24);
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
        sprites[spriteID] = Sprite.Create(texture, new Rect(0f, 0f, texture.width,texture.height), new Vector2(0.5f, 0.5f), 12);                                        
    }

    void CreateSprites() {  // Can be used to initialize all the poses and store em as sprites.
        CreateSprite(new int[] {0x0,0x1,0x6,0x7,0x8,0x9}, 0); // Normal Facing Camera Pose
        CreateSprite(new int[] {0x51,0x52,0x53,0x54,0x55,0x56}, 1); // KO'd
        CreateSprite(new int[] {0x7C,0x7D,0x7E,0x7F,0x80,0x81}, 2); // Shocked
        CreateSprite(new int[] {0x38,0x39,0x3A,0x3B,0x3C,0x3D}, 3); // Kneeling
        CreateSprite(new int[] {0x76,0x77,0x78,0x79,0x7A,0x7B}, 4); // Angry
    }
}
