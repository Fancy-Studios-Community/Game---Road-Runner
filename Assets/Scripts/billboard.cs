using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class billboard : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform birdSprite;

    
    private void LateUpdate()
    {
        birdSprite.LookAt(cameraTransform.position);
    }


}
