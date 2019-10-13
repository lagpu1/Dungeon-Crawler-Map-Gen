using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using RogueElements;
using UnityEditor;

public class DungeonGenerator : MonoBehaviour
{
    [Serializable]
    public struct ItemPercentage
    {
        public Tile item;
        public int percentage;
    }

    [SerializeField]
    Tilemap tilemap;

    [SerializeField]
    Tilemap objectTileMap;

    [SerializeField]
    RuleTile wallTestDynamic;

    [SerializeField]
    RuleOverrideTile floorTestDynamic;

    [SerializeField]
    RuleOverrideTile waterTestDynamic;

    [SerializeField]
    Tile stairsDown;

    [SerializeField]
    public List<ItemPercentage> items = new List<ItemPercentage>();


    private string[] level =
    {
        ".........................",
        ".........................",
        "...........#.............",
        "....###...###...###......",
        "...#.#.....#.....#.#.....",
        "...####...###...####.....",
        "...#.#############.#.....",
        "......##.......##........",
        "......#..#####..#........",
        "......#.#######.#........",
        "...#.##.#######.##.#.....",
        "..#####.###.###.#####....",
        "...#.##.#######.##.#.....",
        "......#.#######.#........",
        "......#..#####..#........",
        "......##.......##........",
        "...#.#############.#.....",
        "...####...###...####.....",
        "...#.#.....#.....#.#.....",
        "....###...###...###......",
        "...........#............."
    };

    // Start is called before the first frame update
    void Start()
    {
        if (tilemap != null)
        {
            createMap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cleanMap();
            createMap();
        }

    }

    void cleanMap()
    {
        tilemap.ClearAllTiles();
        objectTileMap.ClearAllTiles();
    }

    void createMap()
    {
        MapGen<MapGenContext> layout = new MapGen<MapGenContext>();

        //Initialize a 6x4 grid of 10x10 cells.
        InitGridPlanStep<MapGenContext> startGen = new InitGridPlanStep<MapGenContext>(1);
        startGen.CellX = 6;
        startGen.CellY = 4;

        startGen.CellWidth = 9;
        startGen.CellHeight = 9;
        layout.GenSteps.Add(-4, startGen);

        //Create a path that is composed of a ring around the edge
        GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
        path.RoomRatio = new RandRange(70);
        path.BranchRatio = new RandRange(0, 50);

        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
        //cross
        genericRooms.Add(new RoomGenSquare<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)));
        //round
        genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)));
        path.GenericRooms = genericRooms;

        SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
        genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50));
        path.GenericHalls = genericHalls;

        layout.GenSteps.Add(-4, path);

        //Output the rooms into a FloorPlan
        layout.GenSteps.Add(-2, new DrawGridToFloorStep<MapGenContext>());

        //Draw the rooms of the FloorPlan onto the tiled map, with 1 TILE padded on each side
        layout.GenSteps.Add(0, new DrawFloorToTileStep<MapGenContext>(10));

        //Add the stairs up and down
        layout.GenSteps.Add(2, new FloorStairsStep<MapGenContext, StairsUp, StairsDown>(new StairsUp(), new StairsDown()));

        //Generate water (specified by user as Terrain 2) with a frequency of 35%, using Perlin Noise in an order of 3, softness 1.
        int terrain = 2;
        PerlinWaterStep<MapGenContext> waterPostProc = new PerlinWaterStep<MapGenContext>(new RandRange(35), 3, new TileTest(terrain), 1, false);
        layout.GenSteps.Add(3, waterPostProc);

        //Remove walls where diagonals of water exist and replace with water
        layout.GenSteps.Add(4, new DropDiagonalBlockStep<MapGenContext>(new TileTest(terrain)));
        //Remove water stuck in the walls
        layout.GenSteps.Add(4, new EraseIsolatedStep<MapGenContext>(new TileTest(terrain)));

        //Apply Items
        SpawnList<Item> itemSpawns = new SpawnList<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            itemSpawns.Add(new Item(i), items[i].percentage);
        }
        
        RandomSpawnStep<MapGenContext, Item> itemPlacement = new RandomSpawnStep<MapGenContext, Item>(new PickerSpawner<MapGenContext, Item>(new LoopedRand<Item>(itemSpawns, new RandRange(8, 17))));
        layout.GenSteps.Add(6, itemPlacement);

        //Apply Mobs
        /*SpawnList<Mob> mobSpawns = new SpawnList<Mob>();
        mobSpawns.Add(new Mob((int)'r'), 20);
        mobSpawns.Add(new Mob((int)'T'), 10);
        mobSpawns.Add(new Mob((int)'D'), 5);
        RandomSpawnStep<MapGenContext, Mob> mobPlacement = new RandomSpawnStep<MapGenContext, Mob>(new PickerSpawner<MapGenContext, Mob>(new LoopedRand<Mob>(mobSpawns, new RandRange(10, 19))));
        layout.GenSteps.Add(6, mobPlacement);*/

        //Run the generator and print
        MapGenContext context = layout.GenMap(RogueElements.MathUtils.Rand.NextUInt64());
        Print(context.Map);
    }

    public void Print(Map map)
    {
        Vector3Int positionTile;

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Loc loc = new Loc(x, y);
                TileTest tile = map.Tiles[x][y];
                positionTile = new Vector3Int(x,-y,0);

                if (tile.ID <= 0)//wall
                {
                    tilemap.SetTile(positionTile, wallTestDynamic);
                }    
                else if (tile.ID == 1)//floor
                {
                    tilemap.SetTile(positionTile, floorTestDynamic);
                }
                else if (tile.ID == 2)//water
                {
                    tilemap.SetTile(positionTile, waterTestDynamic);
                }
                else
                {
                }

                foreach (StairsDown entrance in map.GenExits)
                {
                    if (entrance.Loc == loc)
                    {
                        objectTileMap.SetTile(positionTile, stairsDown);
                        Debug.Log("Stairs Down: " + loc.X + ", " + loc.Y);
                        break;
                    }
                }
                
                foreach (Item item in map.Items)
                {
                    if (item.Loc == loc)
                    {
                        objectTileMap.SetTile(positionTile, items[item.ID].item);
                    }
                }
                /*
                foreach (Mob item in map.Mobs)
                {
                    if (item.Loc == loc)
                    {
                        tileChar = (char)item.ID;
                        switch ((char)item.ID)
                        {
                            case 'r': //20
                                objectTileMap.SetTile(positionTile, tilesData[2597]);
                                break;
                            case 'T': //10
                                objectTileMap.SetTile(positionTile, tilesData[4945]);
                                break;
                            case 'D': //5
                                objectTileMap.SetTile(positionTile, tilesData[4930]);
                                break;
                        }
                        break;
                    }
                }*/
            }
        }
    }
}
