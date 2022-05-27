using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshHelper : MonoBehaviour
{

    public List<MeshFilter> trees;

   [ContextMenu("Combine Mesh")]
   private void callMeshFuntion()
    {
        Debug.Log("It called me");
        FindObjectOfType<CombineMesh>().assignAllMesh(trees);
    }
}
