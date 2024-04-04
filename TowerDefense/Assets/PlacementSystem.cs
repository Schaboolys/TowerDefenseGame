using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    private GameObject cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    public Vector3 cellIndRot;
    public float cellIndYRot;

    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

        if (Input.GetKeyDown("left"))
        {
            cellIndicator.transform.Rotate(0, 45, 0); // Rotate left by 45 degrees
        }
        if (Input.GetKeyDown("right"))
        {
            cellIndicator.transform.Rotate(0, -45, 0); // Rotate right by 45 degrees
        }
    }

    public void StartPlacement(int ID)
    {
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // Instantiate the cell indicator prefab
        GameObject cellIndicatorPrefab = database.objectsData[selectedObjectIndex].CellIndicatorPrefab;
        cellIndicator = Instantiate(cellIndicatorPrefab);

        // Ensure the instantiated cell indicator is a child of the PlacementSystem GameObject
        cellIndicator.transform.parent = transform;

        // Activate the cell indicator
        cellIndicator.SetActive(true);

        // Subscribe to input events
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // Get the elevation of the plane at the grid position
        float planeElevation = GetPlaneElevationAtPosition(grid.CellToWorld(gridPosition));

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);

        // Adjust the Y position to be on top of the plane
        newObject.transform.position = new Vector3(newObject.transform.position.x, planeElevation, newObject.transform.position.z);

        newObject.transform.rotation = cellIndicator.transform.rotation; // Apply the same rotation as cellIndicator
        Debug.Log("Placed!");
    }

    // Function to get the elevation of the plane at a specific position
    private float GetPlaneElevationAtPosition(Vector3 position)
    {
        // Assuming your plane's elevation doesn't change, you can directly use its Y position
        return position.y;
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;

    }
}