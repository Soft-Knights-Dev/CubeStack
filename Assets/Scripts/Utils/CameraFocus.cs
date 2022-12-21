using Cinemachine;
using GameLogic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float min;
    [SerializeField] private float max;

    void Update()
    {
        var cw = GameManager.Instance.CubeWidth;
        
        if(!float.IsNaN(cw))
            cam.m_Lens.FieldOfView =  min + cw * (max - min);
    }
}
