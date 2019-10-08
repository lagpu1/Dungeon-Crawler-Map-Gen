﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RogueElements;

// EXAMPLES 1-3
public class Map
{
    public const int WALL_TERRAIN_ID = 0;
    public const int ROOM_TERRAIN_ID = 1;

    public ReRandom Rand;

    public TileTest[][] Tiles;

    public int Width { get { return Tiles.Length; } }
    public int Height { get { return Tiles[0].Length; } }

    public List<StairsUp> GenEntrances;
    public List<StairsDown> GenExits;
    public List<Item> Items;
    public List<Mob> Mobs;

    public Map()
    {
        GenEntrances = new List<StairsUp>();
        GenExits = new List<StairsDown>();
        Items = new List<Item>();
        Mobs = new List<Mob>();
    }

    public void InitializeTiles(int width, int height)
    {
        Tiles = new TileTest[width][];
        for (int ii = 0; ii < width; ii++)
        {
            Tiles[ii] = new TileTest[height];
            for (int jj = 0; jj < height; jj++)
                Tiles[ii][jj] = new TileTest();
        }
    }


}
