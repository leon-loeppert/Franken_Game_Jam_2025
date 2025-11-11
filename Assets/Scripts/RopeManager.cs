using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class RopeManager : MonoBehaviour
{
    [SerializeField] private GameObject _straightSegmentPrefab;
    [SerializeField] private GameObject _cornerSegmentPrefab;
    [SerializeField] private Transform _ropeParent;

    [SerializeField] private AudioClip _ropeExtendSound;
    [SerializeField] private AudioClip _failedChainSound;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] public GridManager _gridManager;

    // Keep track of instantiated rope pieces
    private List<GameObject> _ropeSegments = new List<GameObject>();

    public void DrawRope(List<Tile> highlightedTiles) //takes highlighted tiles as input
    {
        // Clear existing rope
        foreach (GameObject segment in _ropeSegments)
            Destroy(segment);
        _ropeSegments.Clear();

        // skip if not enough tiles (you need at least two tiles to form a segment)
        if (highlightedTiles.Count < 2) return;

        /* bool isLoop = false;
        if (highlightedTiles.Count > 2)
        {
            Tile first = highlightedTiles[0];
            Tile last = highlightedTiles[highlightedTiles.Count - 1];

            // Check if they are directly connected (adjacent)
            if ((Mathf.Abs(first.X - last.X) == 1 && first.Y == last.Y) ||
                (Mathf.Abs(first.Y - last.Y) == 1 && first.X == last.X))
            {
                isLoop = true;
            }
        }


        int segmentCount = isLoop ? highlightedTiles.Count : highlightedTiles.Count - 1;

        for (int i = 0; i < segmentCount; i++)
        {
            Tile current = highlightedTiles[i];
            Tile prev = i > 0 ? highlightedTiles[i - 1] : (isLoop ? highlightedTiles[highlightedTiles.Count - 1] : null); // if you are not at the first tile again, then take just prev tile; if you are at the first tile again, the previous tile is just the last from the list
            Tile next = (i + 1 < highlightedTiles.Count) ? highlightedTiles[i + 1] : (isLoop ? highlightedTiles[0] : null); // if you are not at the first tile again, then take just next tile; if you are at the first tile again, the next tile is just the first tile from the list

            // get positions of tiles
            Vector3 startPos = current.transform.position;
            Vector3 endPos = next.transform.position;

            GameObject segment = null;

            // --- Straight segment ---
            if (startPos.x == endPos.x || startPos.y == endPos.y)
            {
                print("spawn rope");
                SoundFXManager.instance.PlaySoundFXClip(_ropeExtendSound, transform, 1);
                segment = Instantiate(_straightSegmentPrefab, startPos, Quaternion.identity, _ropeParent);

                // Stretch segment to cover full distance
                Vector3 direction = endPos - startPos;
                float length = direction.magnitude;
                Vector3 originalScale = segment.transform.localScale;
                segment.transform.localScale = new Vector3(originalScale.x, length, originalScale.z);

                // Rotate based on direction
                if (Mathf.Abs(startPos.x - endPos.x) > Mathf.Abs(startPos.y - endPos.y))
                    segment.transform.rotation = Quaternion.Euler(0, 0, 90); // horizontal
                else
                    segment.transform.rotation = Quaternion.Euler(0, 0, 0);  // vertical
            }

            if (segment != null)
            {
                _ropeSegments.Add(segment);
            }

            // --- Replace Corner segment ---
            if (IsCorner(prev, current, next))
            {
                segment = Instantiate(_cornerSegmentPrefab, current.transform.position, Quaternion.identity, _ropeParent);

                segment.transform.rotation = GetCornerRotation(prev, current, next);
                Destroy(_ropeSegments[_ropeSegments.Count - 1]);
                _ropeSegments.RemoveAt(_ropeSegments.Count - 1);
                _ropeSegments.Add(segment);
            }
        }

        // Draw last rope element
        Tile lastTile = highlightedTiles[highlightedTiles.Count - 1];
        Vector3 lastPos = lastTile.transform.position;

        Tile prevlastTile = highlightedTiles[highlightedTiles.Count - 2];
        Vector3 prevlastPos = prevlastTile.transform.position;

        GameObject last_segment = null;
        last_segment = Instantiate(_straightSegmentPrefab, lastPos, Quaternion.identity, _ropeParent);

        Vector3 direction_last = lastPos - prevlastPos;
        last_segment.transform.localScale = new Vector3(last_segment.transform.localScale.x, direction_last.magnitude, last_segment.transform.localScale.z);

        // Rotate last based on direction
        if (Mathf.Abs(prevlastPos.x - lastPos.x) > Mathf.Abs(prevlastPos.y - lastPos.y))
            last_segment.transform.rotation = Quaternion.Euler(0, 0, 90); // horizontal
        else
            last_segment.transform.rotation = Quaternion.Euler(0, 0, 0);  // vertical

        if (last_segment != null)
        {
            _ropeSegments.Add(last_segment);
        }

        // Remove last segment if it is a corner
        if (isLoop)
        {
            Destroy(_ropeSegments[_ropeSegments.Count - 1]);
            _ropeSegments.RemoveAt(_ropeSegments.Count - 1);
        }


        // Check for level completion
        /*        bool isLoop = false;
               if (highlightedTiles.Count > 2)
               {
                   Tile first = highlightedTiles[0];
                   Tile last = highlightedTiles[highlightedTiles.Count - 1];

                   if ((Mathf.Abs(first.X - last.X) == 1 && first.Y == last.Y) ||
                       (Mathf.Abs(first.Y - last.Y) == 1 && first.X == last.X))
                   {
                       isLoop = true;
                   }
               } */

        // If it’s a loop and all items are highlighted in order, complete the level
        if (isLoop && AreAllItemsCollectedInOrder(highlightedTiles))
        {
            _levelManager.CompleteLevel();
        }
        // if not: failure sound and delete segments
        else if (isLoop && !AreAllItemsCollectedInOrder(highlightedTiles))
        {

            // Implement Fail sound here
            SoundFXManager.instance.PlaySoundFXClip(_failedChainSound, transform, 1.5f);

            //Implement wait 1-2 seconds here (such that tiles do not get immediately deleted)
            StartCoroutine(DestroyRopeSegments());
        }

    }

    IEnumerator DestroyRopeSegments()
    {
        //Wait until SoundFX and VFX are complete
        float _waitToDelete = 10f;
        yield return new WaitForSeconds(_waitToDelete);

        foreach (GameObject segment in _ropeSegments)
        {
            Destroy(segment);
        }
        _ropeSegments.Clear();
    }

    private bool IsCorner(Tile prev, Tile current, Tile next)
    {
        if (prev == null || next == null)
            return false;

        bool prevHorizontal = prev.Y == current.Y;
        bool prevVertical = prev.X == current.X;

        bool nextHorizontal = current.Y == next.Y;
        bool nextVertical = current.X == next.X;

        // Corner happens if direction changes
        return (prevHorizontal != nextHorizontal) || (prevVertical != nextVertical);
    }

    private Quaternion GetCornerRotation(Tile prev, Tile current, Tile next)
    {
        if (prev == null || next == null)
            return Quaternion.identity;

        int dxPrev = current.X - prev.X;
        int dyPrev = current.Y - prev.Y;
        int dxNext = next.X - current.X;
        int dyNext = next.Y - current.Y;

        if ((dxPrev == 1 && dyNext == 1) || (dyPrev == -1 && dxNext == -1)) // bottom-right
            return Quaternion.Euler(0, 0, 90);
        if ((dxPrev == -1 && dyNext == -1) || (dyPrev == 1 && dxNext == 1)) // top-left
            return Quaternion.Euler(0, 0, 270);
        if ((dxPrev == 1 && dyNext == -1) || (dyPrev == 1 && dxNext == -1)) // top-right
            return Quaternion.Euler(0, 0, 180);
        if ((dxPrev == -1 && dyNext == 1) || (dyPrev == -1 && dxNext == 1)) // bottom-left
            return Quaternion.Euler(0, 0, 0);

        return Quaternion.identity;
    }

    private bool AreAllItemsCollectedInOrder(List<Tile> highlightedTiles)
    {

        // Check if all squares are highlighted (optional game element: user has to pass all fields) 
        // if (highlightedTiles.Count != _gridManager._allTiles.Count) return false;


        // Check if there are as many items highlighted as there are items on the grid
        int countHighlightedItems = highlightedTiles.Count(tile => tile.HasItem());
        if (countHighlightedItems != _gridManager.countItems) return false;

        int currentOrderIndex = 1;

        foreach (Tile tile in highlightedTiles)
        {
            if (tile.HasItem())
            {
                Item item = tile.GetItem();
                if (item.GetOrderIndex() != currentOrderIndex)
                    return false; // wrong order
                currentOrderIndex++;
            }
        }

        // all items were highlighted in the correct order

        // Bug: it is also enough to get less than 5 in the correct order

        return true;
    }

}