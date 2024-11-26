using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMesh{
    public List<Vector3> Vertices = new List<Vector3>();
    public List<int> Indices = new List<int>(); 
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class EnemyFOV : EnemyAI
{
    private GameObject fovObject;
    public Material mat;
    public int triangleSegments = 20;

    private MeshFilter mf;
    private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        // Create a new GameObject for the FOV
        fovObject = new GameObject("EnemyFOV");

        // Add components to the FOV GameObject
        mf = fovObject.AddComponent<MeshFilter>();
        mr = fovObject.AddComponent<MeshRenderer>();

        // Assign the FOV material
        mr.material = mat;

        // Generate the FOV mesh
        GenerateFOVMesh(mf);
    }

    void OnDestroy()
    {
        if (fovObject != null)
        {
            Destroy(fovObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fovObject != null)
        {
            GenerateFOVMesh(mf);
        }
        
    }

    void GenerateFOVMesh(MeshFilter mf)
    {
        Vector3 enemyPos = transform.position;

        TriangleMesh mesh = new TriangleMesh();
        mesh.Vertices.Add(enemyPos); // Center (enemy position)

        float angleStep = aiFieldOfViewAngle / triangleSegments;
        float startAngle = -aiFieldOfViewAngle / 2;

        for(int i = 0; i <= triangleSegments; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            Vector3 vertex = enemyPos + direction * aiSightRange;
            mesh.Vertices.Add(vertex);

            if (i < triangleSegments)
            {
                mesh.Indices.Add(0);
                mesh.Indices.Add(i + 1);
                mesh.Indices.Add(i + 2);
            }
        }

        // Create a Unity Mesh and assign vertices and indices
        Mesh unityMesh = mf.mesh == null ? new Mesh() : mf.mesh;
        unityMesh.Clear(); // Clear previous data
        unityMesh.SetVertices(mesh.Vertices);
        unityMesh.SetIndices(mesh.Indices, MeshTopology.Triangles, 0);

        mf.mesh = unityMesh; 
    }
}
