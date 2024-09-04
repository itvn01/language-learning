using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M1Game
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        void Start()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
