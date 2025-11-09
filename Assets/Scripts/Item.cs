using UnityEngine;

public class Item : MonoBehaviour
{
    private int _orderIndex;

    public void SetOrderIndex(int index) => _orderIndex = index;
    public int GetOrderIndex() => _orderIndex;

    public Sprite GetIconSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        return sr != null ? sr.sprite : null;
    }
}