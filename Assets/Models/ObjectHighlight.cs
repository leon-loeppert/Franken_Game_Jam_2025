using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectHighlight : MonoBehaviour
{
    private Renderer[] renderers;
    [SerializeField] private Material overlayMaterial;

    private void Awake()
    {
        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnMouseEnter()
    {
        print("hover!");

        foreach (var renderer in renderers)
        {

            // Append outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Add(overlayMaterial);

            renderer.materials = materials.ToArray();
        }
    }

    private void OnMouseExit()
    {
        print("no more hover!");

        foreach (var renderer in renderers)
        {

            // Remove outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Remove(overlayMaterial);

            renderer.materials = materials.ToArray();
        }
    }
}
