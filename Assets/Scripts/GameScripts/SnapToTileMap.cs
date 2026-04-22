using UnityEngine;
using UnityEngine.Tilemaps;

public class SnapToTilemap : MonoBehaviour
{
    public Tilemap tilemap;

    void Awake()
    {
        //to snap the snake following tilemap grid
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.GetCellCenterWorld(cellPos);
    }
}