using System.Collections.Generic;
using UnityEngine;
using Assets.GeneralScripts;

public class ExperimentAssetHolder : Singleton<ExperimentAssetHolder>
{
    private Dictionary<string, GameObject> ExperimentAssets = new Dictionary<string, GameObject>();

	void Start ()
    {

	    foreach (Transform child in transform)
	    {
	        ExperimentAssets.Add(child.name, child.gameObject);
	    }
	}
    public GameObject FindPrefab(string assetName)
    {
        return ExperimentAssets[assetName];
    }
}
