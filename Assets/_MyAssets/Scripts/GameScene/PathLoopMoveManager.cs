using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathLoopMoveManager : MonoBehaviour
{
    [SerializeField] PathLoopMover_1[] pathLoopMovers;
    [SerializeField] MoveData moveData;
    private void Awake()
    {
        StartMove();
    }
    public void StartMove()
    {
        List<Transform> pathTransforms = pathLoopMovers.Select(plm => plm.transform).ToList();

        for(int i = 0; i < pathLoopMovers.Length; i++)
        {
            List<Transform> path = new List<Transform>(pathTransforms);
            for(int l = 0; l  < i+1; l++)
            {
                Transform buf = path[0];
                path.RemoveAt(0);
                path.Add(buf);
            }
            pathLoopMovers[i].StartPathLoop(path.ToArray(), moveData);
        }
    }
}
