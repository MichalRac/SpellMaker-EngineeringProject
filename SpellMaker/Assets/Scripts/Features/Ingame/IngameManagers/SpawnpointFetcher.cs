using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointFetcher : MonoBehaviour
{
    [SerializeField] private List<Transform> PlayerSpawnPoints;
    [SerializeField] private List<Transform> EnemySpawnPoints;

    public Vector3 GetNextPlayerStartPosition()
    {
        var pos = PlayerSpawnPoints[0].position;
        PlayerSpawnPoints.RemoveAt(0);
        return pos;
    }

    public Vector3 GetNextEnemyStartPosition()
    {
        var pos = EnemySpawnPoints[0].position;
        EnemySpawnPoints.RemoveAt(0);
        return pos;
    }
}
