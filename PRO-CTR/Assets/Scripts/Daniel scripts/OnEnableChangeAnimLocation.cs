using System;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableChangeAnimLocation : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameraFollowers = new List<GameObject>();
    
    [SerializeField] private Camera camera;
    Vector2 cameraPos;

    private void OnEnable()
    {
        cameraPos = new Vector2(camera.transform.position.x, camera.transform.position.y);
        foreach (var c in cameraFollowers)
        {
            c.transform.position = cameraPos;
        }
    }
}