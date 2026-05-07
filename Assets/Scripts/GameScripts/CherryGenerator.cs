using UnityEngine;
using UnityEngine.Tilemaps;

public class CherryGenerator : MonoBehaviour
{
    public GameObject cherryPrefab;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    public Tilemap tilemap;

    void Start()
    {
        SpawnCherry();
    }

    public void SpawnCherry()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y)
        );

        //convert to tile cell
        Vector3Int cellPos = tilemap.WorldToCell(randomPos);
        //snap to tile
        Vector3 spawnPos = tilemap.GetCellCenterWorld(cellPos);

        Instantiate(cherryPrefab, spawnPos, Quaternion.identity);
    }
}