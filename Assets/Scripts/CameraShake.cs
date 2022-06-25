using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    
    public static CameraShake Instance { get; private set; }

    //private CinemachineVirtualCamera cineCamera;
    //private float shakeTimer;
    private void Awake()
    {
        Instance = this;
        //cinemachineImpulseSource = transform.GetComponent<CinemachineImpulseSource>();
        //cineCamera = GetComponent<CinemachineVirtualCamera>();
    }
    /*
    public void shakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    } 
    */
    public CinemachineImpulseSource cinemachineImpulseSource;


    public void shakeCamera(float intensity, float time)
    {
        cinemachineImpulseSource.GenerateImpulse(intensity);
    }
}
