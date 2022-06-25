using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSun : MonoBehaviour
{

    [SerializeField] float theta = 0.01f;
    private void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, theta * Time.deltaTime);
    }
}
