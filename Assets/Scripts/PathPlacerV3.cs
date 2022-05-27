using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathPlacerV3 : MonoBehaviour
{
    public PathCreator pathCreator;
    public const float spacing = 3;
    public const float minSpacing = 0.1f;
    private List<Vector3> points = new List<Vector3>();
    private List<Vector3> pointDirection = new List<Vector3>();

    //private Vector3[] points;
    void Start()
    {
        VertexPath path = pathCreator.path;
        points.Clear();
        pointDirection.Clear();

        for (float dst = 0; dst < path.length; dst += spacing)
        {
            Vector3 point = path.GetPointAtDistance(dst);
            points.Add(point);
            pointDirection.Add(path.GetDirection(dst));
        }
        //foreach (Vector3 point in points)
        //{
        //    Debug.Log(point);
        //}
        Debug.Log(points[2]+" " +pointDirection[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
