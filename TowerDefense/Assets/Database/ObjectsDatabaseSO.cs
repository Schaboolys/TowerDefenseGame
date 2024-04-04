using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    private bool _isCellIndicator;
    public bool IsCellIndicator
    {
        get { return _isCellIndicator; }
        set { _isCellIndicator = value; }
    }

    [SerializeField]
    private GameObject cellIndicatorPrefab;
    public GameObject CellIndicatorPrefab => cellIndicatorPrefab;
}