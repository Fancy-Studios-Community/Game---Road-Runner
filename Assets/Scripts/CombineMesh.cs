using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMesh : MonoBehaviour
{
    
    private List<MeshFilter> sourceMeshFilters;
    [SerializeField] public Material forestMaterial;
    [SerializeField] public GameObject ForestContainer;
    

    //[ContextMenu("Combine Meshes")]
    private void CombineMeshes()
    {
        CombineInstance[] combine = new CombineInstance[sourceMeshFilters.Count];

        for (int i = 0; i < sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = sourceMeshFilters[i].sharedMesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
            sourceMeshFilters[i].gameObject.SetActive(false);
            //Destroy(sourceMeshFilters[i].gameObject);
        }

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.CombineMeshes(combine);
        GameObject placeholder = new GameObject();
        placeholder.AddComponent<MeshRenderer>();
        placeholder.AddComponent<MeshFilter>();
        placeholder.GetComponent<MeshRenderer>().material = forestMaterial;
        placeholder.GetComponent<MeshFilter>().mesh = mesh;
        placeholder.gameObject.SetActive(true);
        //placeholder.transform.position = new Vector3(placeholder.transform.position.x, -0.75f, placeholder.transform.position.z);
        placeholder.transform.SetParent(ForestContainer.transform);
    }
    public void assignAllMesh(List<MeshFilter> meshList)
    {
        sourceMeshFilters = meshList;
        CombineMeshes();
        
        //Debug.Log("call pass" + meshList.Count);
    }
}
