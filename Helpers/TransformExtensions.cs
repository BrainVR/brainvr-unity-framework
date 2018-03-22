using UnityEngine;
using System.Collections;

public static class TransformExtensions{

    // Set the layer of this GameObject and all of its children.
    public static void SetLayerRecursively(this Transform transform, int layer) {
        transform.gameObject.layer = layer;
        foreach (Transform t in transform)
            t.SetLayerRecursively(layer);
    }
}
