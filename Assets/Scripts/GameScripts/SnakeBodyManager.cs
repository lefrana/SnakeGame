using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyManager : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public GameObject bodyPrefab;

    void Start()
    {
        //show first body
        Grow(transform.position - new Vector3(-10.0f, 0, 0));
    }

    public void MoveBody(Vector3 prevHeadPos)
    {
        Vector3 prev = prevHeadPos;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 temp = bodyParts[i].position;
            bodyParts[i].position = prev;
            prev = temp;
        }
    }

    public void Grow(Vector3 spawnPos)
    {
        GameObject part = Instantiate(bodyPrefab, spawnPos, Quaternion.identity);
        bodyParts.Add(part.transform);
    }
}