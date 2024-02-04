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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Screenshake(5f);
    }

    public void Screenshake(float shakeForce) => shakeSource.GenerateImpulseWithForce(shakeForce);
}