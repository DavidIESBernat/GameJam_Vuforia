using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation; 

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        BakeNavMesh();
    }

    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}

