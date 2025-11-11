using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width = 5;
    [SerializeField] public int _height = 5;
    [SerializeField] public float _spacing = 1.1f;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private List<Item> _itemPrefabs;

    [SerializeField] private Transform _cam;

    [SerializeField] private Transform _itemQueuePanel;

    private List<Tile> _highlightedTiles = new List<Tile>();
    public List<Tile> _allTiles = new List<Tile>();

    public int countItems;

    public RopeManager RopeManager;


    void Start()
    {
        GenerateGrid();
        SpawnOrderedItems();

        RopeManager._gridManager = this; // now they are automatically linked, without inspector
    }

    void GenerateGrid()
    {
        //float spacing = 1.1f;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 tilePosition = new Vector3(x * _spacing, y * _spacing, 0);
                var spawnedTile = Instantiate(_tilePrefab, tilePosition, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.SetGridManager(this);
                spawnedTile.SetGridPosition(x, y);
                _allTiles.Add(spawnedTile);
            }
        }

        _cam.transform.position = new Vector3((_width * _spacing) / 2 - _spacing / 2, (_height * _spacing) / 2 - _spacing / 2, -10);
    }

    // Add a tile to the list
    public void AddHighlightedTile(Tile tile)
    {
        if (!_highlightedTiles.Contains(tile))
            _highlightedTiles.Add(tile);
    }

    // Remove a tile from the list
    public void RemoveHighlightedTile(Tile tile)
    {
        if (_highlightedTiles.Contains(tile))
            _highlightedTiles.Remove(tile);
    }

    // Provide access to the current highlighted tiles
    public List<Tile> GetHighlightedTiles()
    {
        return _highlightedTiles;
    }

    void SpawnOrderedItems()
    {
        countItems = _itemPrefabs.Count;
        
        if (_allTiles.Count < countItems)
        {
            Debug.LogWarning("Not enough tiles to place items!");
            return;
        }

        // For random locations (which tiles are still available for spawning?)
        List<Tile> availableTiles = new List<Tile>(_allTiles);
        List<Tile> chosenTiles = new List<Tile>();

        List<Item> itemPool = new List<Item>(_itemPrefabs); // we don't want list to be emptied permanently

        List<int> itemLocations = new List<int> { 0, 6, 12, 13, 14, 15};

        List<int> ItemIndexList = new List<int>();
        
        for (int i = 0; i < countItems; i++)
        {
            // int randomTileIndex = UnityEngine.Random.Range(0, availableTiles.Count); // random number!
            int randomTileIndex = itemLocations[i];
            Tile chosenTile = availableTiles[randomTileIndex];
            chosenTiles.Add(chosenTile);
            availableTiles.RemoveAt(randomTileIndex);

            // Pick random item prefab
            int randomItemIndex = UnityEngine.Random.Range(0, itemPool.Count);
            ItemIndexList.Add(randomItemIndex);

            Item currentItemPrefab = itemPool[randomItemIndex];
            itemPool.RemoveAt(randomItemIndex); // no duplicates

            // Spawn item at chosen location in grid
            Vector3 itemPosition = chosenTile.transform.position + Vector3.forward * -0.1f; //put it to front
            Item spawnedItem = Instantiate(currentItemPrefab, itemPosition, currentItemPrefab.transform.rotation);
            spawnedItem.name = $"Item {i + 1}";
            spawnedItem.SetOrderIndex(i + 1);
            // Assign the spawned item to the tile
            chosenTile.SetItem(spawnedItem);

            // Spawn 3D item inside the panel
            Item itemQueue = Instantiate(currentItemPrefab, _itemQueuePanel);

            float itemSpacing = 1.5f; // space between items in world units
            itemQueue.transform.localPosition = new Vector3(0, -i * itemSpacing, 0);
            itemQueue.transform.localRotation = Quaternion.identity;
            itemQueue.transform.localScale = Vector3.one * 0.5f;


        }




        

    }


}