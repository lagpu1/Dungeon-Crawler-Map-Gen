using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class MouseTileCreator : MonoBehaviour 
{
    /*[SerializeField]
    Tilemap tilemap;

    [SerializeField]
    List<Tile> tilesData = new List<Tile>();

	// Use this for initialization
	void Start () 
    {
        string[] tilesPath;
        tilesPath = AssetDatabase.FindAssets("t:Tile", new[] { "Assets/Tiles" });

        if (tilesPath.Length != 0)
        {
            foreach (string tilePath in tilesPath)
            {
                Tile tile = (Tile)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tilePath), typeof(Tile));

                if (tile != null)
                    tilesData.Add(tile);
            }
        }
        else 
        {
            Debug.LogWarning("Error at finding tiles"); 
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (tilemap != null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3Int positionTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (!tilemap.HasTile(positionTile))
                    tilemap.SetTile(positionTile, tilesData[Random.Range(0, tilesData.Count - 1)]);
                else
                    Debug.LogWarning("Tile at that position");
            }
        }
        else 
        {
            Debug.LogWarning("No tilemap selected to paint");
        }
    }*/
}
