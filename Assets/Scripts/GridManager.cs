using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width = 5;
    [SerializeField] public int _height = 5;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Item _itemPrefab;

    [SerializeField] private Transform _cam;

    private List<Tile> _highlightedTiles = new List<Tile>();
    private List<Tile> _allTiles = new List<Tile>();

    public RopeManager RopeManager;

    void Start()
    {
        GenerateGrid();
        SpawnOrderedItems(3);
    }

    void GenerateGrid()
    {
        float spacing = 1.1f;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 tilePosition = new Vector3(x * spacing, y * spacing, 0);
                var spawnedTile = Instantiate(_tilePrefab, tilePosition, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.SetGridManager(this);
                spawnedTile.SetGridPosition(x, y);
                _allTiles.Add(spawnedTile);
            }
        }

        _cam.transform.position = new Vector3((_width * spacing) / 2 - spacing / 2, (_height * spacing) / 2 - spacing / 2, -10);
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

    void SpawnOrderedItems(int count)
    {
        if (_allTiles.Count < count)
        {
            Debug.LogWarning("Not enough tiles to place items!");
            return;
        }

        List<Tile> availableTiles = new List<Tile>(_allTiles);
        List<Tile> chosenTiles = new List<Tile>();

        // Define color palette (replace by textures)
        List<Color> palette = new List<Color> {Color.yellow, Color.blue, Color.red};

        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableTiles.Count);
            Tile chosenTile = availableTiles[randomIndex];
            chosenTiles.Add(chosenTile);
            availableTiles.RemoveAt(randomIndex);
        }

        for (int i = 0; i < chosenTiles.Count; i++)
        {
            Vector3 itemPosition = chosenTiles[i].transform.position + Vector3.forward * -0.1f; // so it appears on top
            Item spawnedItem = Instantiate(_itemPrefab, itemPosition, Quaternion.identity);
            spawnedItem.name = $"Item {i + 1}";
            spawnedItem.SetOrderIndex(i + 1);

            // Assign random colors from palette (or later textures) => replace .SetColor by .SetTexture
            int randomColorInt = UnityEngine.Random.Range(0, palette.Count);
            Color randomColor = palette[randomColorInt];
            palette.RemoveAt(randomColorInt);
            spawnedItem.SetColor(randomColor);
        }


        

    }


}