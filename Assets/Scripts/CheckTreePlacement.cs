using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTreePlacement : MonoBehaviour
{
    public GameObject blackcurtain;
    
    private void OnCollisionEnter(Collision collision)
    {
        blackcurtain.SetActive(false);
    }
}
