using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    CinemachineImpulseSource shakeSource;
    
    void Awake()
    {
        if (instance == null) instance = this;
        shakeSource = GetComponent<CinemachineImpulseSource>();
    }


    public void Screenshake(float shakeForce) => shakeSource.GenerateImpulseWithForce(shakeForce);
}