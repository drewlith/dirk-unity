using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;

public class ROM : MonoBehaviour
{
    public static bool romDataExists;
    public static byte[] DATA; // The entire ROM.
    public static Palette[] PALETTES; // All field palettes I think.
    public static Dictionary<string,Tile[]> TILESETS = new Dictionary<string, Tile[]>(); // Dictionary containing tilesets keyed by character name.
    void Awake()
    {
        if (File.Exists(Application.dataPath + "/rom_data.smc")) {
            LoadData();
        }
    }

    void LoadData() {
        string path = Application.dataPath + "/rom_data.smc";
        romDataExists = true;
        TILESETS = new Dictionary<string, Tile[]>();
        DATA = File.ReadAllBytes(path);
        PALETTES = GetPalettes(Read(DATA,0x268000, 0x400));
        TILESETS.Add("Terra", GetTiles(Read(DATA,0x150000,0x16A0)));
        TILESETS.Add("Locke", GetTiles(Read(DATA,0x1516A0,0x16A0)));
        TILESETS.Add("Cyan", GetTiles(Read(DATA,0x152D40,0x16A0)));
        TILESETS.Add("Shadow", GetTiles(Read(DATA,0x1543E0,0x16A0)));
        TILESETS.Add("Edgar", GetTiles(Read(DATA,0x155A80,0x16A0)));
        TILESETS.Add("Sabin", GetTiles(Read(DATA,0x157120,0x16A0)));
        TILESETS.Add("Celes", GetTiles(Read(DATA,0x1587C0,0x16A0)));
        TILESETS.Add("Strago", GetTiles(Read(DATA,0x159E60,0x16A0)));
        TILESETS.Add("Relm", GetTiles(Read(DATA,0x15B500,0x16A0)));
        TILESETS.Add("Setzer", GetTiles(Read(DATA,0x15CBA0,0x16A0)));
        TILESETS.Add("Mog", GetTiles(Read(DATA,0x15E240,0x16A0)));
        TILESETS.Add("Gau", GetTiles(Read(DATA,0x15F8E0,0x16A0)));
        TILESETS.Add("Gogo", GetTiles(Read(DATA,0x160F80,0x16A0)));
        TILESETS.Add("Umaro", GetTiles(Read(DATA,0x162620,0x16A0)));
        TILESETS.Add("Magicite", GetTiles(Read(DATA,0x17E960, 0xA0)));
        TILESETS.Add("Dragons", GetTiles(Read(DATA,0x17B5C0, 0x500)));
        TILESETS.Add("Ultros", GetTiles(Read(DATA,0x172640, 0x5C0)));
        TILESETS.Add("Treasure", GetTiles(Read(DATA,0x17E4A0, 0xC0)));
    }

    public static Palette[] GetPalettes(byte[] data) {
        Palette[] palettes = new Palette[data.Length/32];
        for (int i = 0; i < palettes.Length; i++) {
            palettes[i] = Palette.Decode(Read(data, i*32, 32));
        }
        return palettes;
    }

    public static Tile[] GetTiles(byte[] data) {
        Tile[] tiles = new Tile[data.Length/32];
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i] = Tile.Decode(Read(data, i*32, 32));
        }
        return tiles;
    }

    public static byte[] Read(byte[] data, int offset, int size) { // Used for getting a chunk of data.
        byte[] newData = new byte[size];
        for (int i = 0; i < size; i++) {
            newData[i] = data[offset+i];
        }
        return newData;
    }

    public void SaveROM() {
        string[] path = StandaloneFileBrowser.OpenFilePanel("Select FF6 ROM", "", "smc", false);
        if (path[0].Length > 0) {
            File.Copy(path[0], Application.dataPath + "/rom_data.smc", true);
            LoadData();
            Autotracker.autotrack = false;
            ROMChecker.reset = true;
        }
    }
}
