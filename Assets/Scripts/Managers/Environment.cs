using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    EnvironmentGrid environmentGridPrefab;
    [SerializeField]
    Explorer explorer;

    Biome[] biomes;
    Vector2Int currentPosition;
    Dictionary<Vector2Int, EnvironmentGrid> allEnvironments = new Dictionary<Vector2Int, EnvironmentGrid>();

    void Awake() {
        biomes = Resources.LoadAll<Biome>("Biomes");
    }

    void Start() {
        LoadEnvironment(Vector2Int.zero);
    }

    public void MoveUp() {
        LoadEnvironment(new Vector2Int(currentPosition.x, currentPosition.y - 1));
    }

    public void MoveDown() {
        LoadEnvironment(new Vector2Int(currentPosition.x, currentPosition.y + 1));
    }

    public void MoveRight() {
        LoadEnvironment(new Vector2Int(currentPosition.x + 1, currentPosition.y));
    }

    public void MoveLeft() {
        LoadEnvironment(new Vector2Int(currentPosition.x - 1, currentPosition.y));
    }

    void LoadEnvironment(Vector2Int mapPosition) {
        if (!allEnvironments.ContainsKey(mapPosition)) {
            allEnvironments.Add(mapPosition, CreateEnvironment(mapPosition));
        }

        ChangeEnvironment(mapPosition);
    }

    EnvironmentGrid CreateEnvironment(Vector2Int mapPosition) {
        var biome = biomes[Random.Range(0, biomes.Length)];

        var newGrid = Instantiate(environmentGridPrefab);
        newGrid.transform.SetParent(transform);
        newGrid.SetBiome(biome);
        newGrid.name = $"Environment ({mapPosition.x}, {mapPosition.y})";

        return newGrid;
    }

    void ChangeEnvironment(Vector2Int mapPosition) {
        if (allEnvironments.ContainsKey(currentPosition)) {
            allEnvironments[currentPosition].Disable();
        }

        var newEnvironment = allEnvironments[mapPosition];
        newEnvironment.Enable();

        explorer.SetBiome(newEnvironment.biome);
        currentPosition = mapPosition;
    }
}
