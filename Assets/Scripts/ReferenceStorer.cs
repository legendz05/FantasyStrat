using UnityEngine;
using System;

public class ReferenceStorer : MonoBehaviour
{
    public static ReferenceStorer Instance;

    [NonSerialized] public CameraController cameraController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        FindReferences();
    }

    private void FindReferences()
    {
        cameraController = FindAnyObjectByType<CameraController>();
    }
}
