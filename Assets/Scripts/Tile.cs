using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private bool _isHighlighted = false;

    void OnMouseDown()
    {
        Highlight();
    }

    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            Highlight();
        }
    }

    private void OnMouseExit()
    {
        //nothing
    }

        private void OnMouseOver()
    {
        _highlight.SetActive(Input.GetMouseButton(0)); //set active because we do not click in (but only when mouse is clicked)
    }

    private void Highlight()
    {
        _highlight.SetActive(true);
        _isHighlighted = true;
    }

    public void ResetHighlight()
    {
        _highlight.SetActive(false);
        _isHighlighted = false;
    }

    

        private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            _highlight.SetActive(false);
        }
    }
}
