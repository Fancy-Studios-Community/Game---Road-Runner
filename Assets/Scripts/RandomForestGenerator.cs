//Code used from Code used https://www.youtube.com/watch?v=604lmtHhcQsS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForestGenerator : MonoBehaviour
{

    public int forestSize = 25; // Overall size of the forest (a square of forestSize X forestSize).
    public int elementSpacing = 3; // The spacing between element placements. Basically grid size.

    //public GameObject forest_contrainer;
    public Element[] elements;
    public LayerMask track;
    public GameObject _player;
    private Vector3 playerLastPosition;
    private List<MeshFilter> trees = new List<MeshFilter>();
    private CombineMesh CombineTreeMesh;


    private void Awake()
    {
        CombineTreeMesh = GameObject.FindObjectOfType<CombineMesh>();
    }
    private void Start()
    {
        playerLastPosition = new Vector3(170,10,192);
        // Loop through all the positions within our forest boundary.
        

    }
    private void Update()
    {

        Vector3 temp = _player.transform.position - playerLastPosition;

        if (temp.magnitude > 50)
        {
            //Debug.Log("away");
            playerLastPosition = _player.transform.position;
            spawnTrees(_player.transform.position);
        }
    }

    private void spawnTrees(Vector3 playerPosition)
    {
        for (int x = 0; x < forestSize; x += elementSpacing)
        {
            for (int z = 0; z < forestSize; z += elementSpacing)
            {

                // For each position, loop through each element...
                for (int i = 0; i < elements.Length; i++)
                {

                    // Get the current element.
                    Element element = elements[i];

                    // Check if the element can be placed.
                    if (element.CanPlace())
                    {

                        // Add random elements to element placement.
                        Vector3 position = new Vector3(x, 0f, z);
                        Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
                        Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
                        Vector3 scale = Vector3.one * Random.Range(0.75f, 1.25f);

                        //Moved and edited the code to make more sence for your game
                        // Instantiate and place element in world.
                        //GameObject newElement = Instantiate(element.GetRandom());
                        //newElement.transform.SetParent(forest_contrainer.transform);
                        //newElement.transform.position = position + offset;
                        //newElement.transform.position = forest_contrainer.transform.position + position + offset;
                        //newElement.transform.eulerAngles = rotation;
                        //newElement.transform.localScale = scale;



                        Vector3 instantiateAtPosition = playerPosition + position + offset - new Vector3(0,0,25);
                        Ray ray = new Ray(new Vector3(instantiateAtPosition.x, 10f, instantiateAtPosition.z), -Vector3.up);


                        if (!Physics.SphereCast(ray, 2f, Mathf.Infinity, track))
                        {

                            // Instantiate and place element in world.
                            GameObject newElement = Instantiate(element.GetRandom(), instantiateAtPosition, Quaternion.Euler(rotation));
                            //newElement.transform.SetParent(forest_contrainer.transform);
                            newElement.transform.localScale = scale;
                            trees.Add(newElement.GetComponent<MeshFilter>());
                           

                        }
                        // Break out of this for loop to ensure we don't place another element at this position.
                        break;

                    }

                }
            }
        }
        CombineTreeMesh.assignAllMesh(trees);
        trees.Clear();
        
    }

}

[System.Serializable]
public class Element
{

    public string name;
    [Range(1, 10)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace()
    {

        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < density)
            return true;
        else
            return false;

    }

    public GameObject GetRandom()
    {

        // Return a random GameObject prefab from the prefabs array.

        return prefabs[Random.Range(0, prefabs.Length)];

    }

}
