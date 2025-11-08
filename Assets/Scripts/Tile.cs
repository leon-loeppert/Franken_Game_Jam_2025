using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    private GridManager _gridManager;

    private bool _isHighlighted = false;

    public void SetGridManager(GridManager manager)
    {
        _gridManager = manager;
    }

    private int _x, _y;

    public void SetGridPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public int X => _x; // optional getter
    public int Y => _y; // optional getter

    // Check for adjacency
    private bool IsAdjacentToPrevHighlightedTile()
{
        Tile PrevHighlighted = _gridManager.GetHighlightedTiles()[_gridManager.GetHighlightedTiles().Count - 1];

        int dx = Mathf.Abs(_x - PrevHighlighted.X);
        int dy = Mathf.Abs(_y - PrevHighlighted.Y);

        if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1))
            return true; // directly next to a highlighted tile

        return false; // no adjacent highlighted tile found
}



    void OnMouseDown()
    {
         if (!_gridManager.GetHighlightedTiles().Contains(this) &&
         (_gridManager.GetHighlightedTiles().Count == 0 || IsAdjacentToPrevHighlightedTile()))
        {
            Highlight();
        }
        //Highlight(this);
    }

    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0)) // mouse is held down
        {
            if (!_gridManager.GetHighlightedTiles().Contains(this) &&
                (_gridManager.GetHighlightedTiles().Count == 0 || IsAdjacentToPrevHighlightedTile()))
            {
                Highlight();
            }
        }
    }

    private void OnMouseExit()
    {
        //nothing
    }


    private void Highlight()
    {

        if (_isHighlighted) return; // avoid double highlighting (is this needed?)

        _highlight.SetActive(true);
        _isHighlighted = true;
        _gridManager.AddHighlightedTile(this);
        
        _gridManager.RopeManager.DrawRope(_gridManager.GetHighlightedTiles()); // draw complete rope from scratch on every new highlight
    }

    public void ResetHighlight()
    {
        _highlight.SetActive(false);
        _isHighlighted = false;
        _gridManager.RemoveHighlightedTile(this);
    }

    

private void Update()
{
    if (!Input.GetMouseButton(0))
    {
        // Only clear if the tile is highlighted
        if (_isHighlighted)
        {
            ResetHighlight();
        }

    }
}
}
