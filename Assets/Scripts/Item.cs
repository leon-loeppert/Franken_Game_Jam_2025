using UnityEngine;

public class Item : MonoBehaviour
{
    private int _orderIndex;
    public void SetOrderIndex(int index) => _orderIndex = index;
    public int GetOrderIndex() => _orderIndex;
/* 
    public void SetColor(Color color)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
            renderer.color = color; //replace by render texture or something like that
    } */

        public Sprite GetIconSprite()
    {
        // Use the prefab’s SpriteRenderer sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        return sr != null ? sr.sprite : null;
    }

}

