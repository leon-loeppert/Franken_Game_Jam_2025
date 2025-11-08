using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    private List<Tile> _highlightedTiles = new List<Tile>();

    public RopeManager RopeManager;

    void Start()
    {
        GenerateGrid();
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


}