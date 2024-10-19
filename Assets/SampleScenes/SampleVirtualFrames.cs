// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections.Generic;
using UnityEngine;

public class SampleVirtualFrames : MonoBehaviour
{
    public OVRSceneManager _sceneManager;

    // All virtual content is a child of this
    public Transform _envRoot;

    // Drop the virtual world this far below the floor anchor
    const float _groundDelta = 0.02f;

    //public SimpleResizable _doorPrefab;

    void Awake()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
        OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
#endif

        // Hide the environment initially so app starts in Passthrough
        _envRoot.gameObject.SetActive(false);

        _sceneManager.SceneModelLoadedSuccessfully += InitializeRoom;
    }

    void InitializeRoom()
    {
        OVRSceneAnchor[] sceneAnchors = FindObjectsOfType<OVRSceneAnchor>();
        if (sceneAnchors != null)
        {
            List<SampleDoorDebris> doorScripts = new List<SampleDoorDebris>();
            float floorHeight = 0.0f;
            bool butterflyAdded = false;
            // Enable the environment root regardless of windows or doors presence
            _envRoot.gameObject.SetActive(true);
            for (int i = 0; i < sceneAnchors.Length; i++)
            {
                OVRSceneAnchor instance = sceneAnchors[i];
                OVRSemanticClassification classification = instance.GetComponent<OVRSemanticClassification>();

                if (classification.Contains(OVRSceneManager.Classification.Floor))
                {
                    // Move the world slightly below the ground floor to avoid Z-fighting
                    if (_envRoot)
                    {
                        Vector3 envPos = _envRoot.transform.position;
                        float groundHeight = instance.transform.position.y - _groundDelta;
                        _envRoot.transform.position = new Vector3(envPos.x, groundHeight, envPos.z);
                        floorHeight = instance.transform.position.y;
                    }
                }
                else if (classification.Contains(OVRSceneManager.Classification.WindowFrame) ||
                         classification.Contains(OVRSceneManager.Classification.DoorFrame))
                {
                    // Cache all the door objects for correct floor fade positioning
                    if (instance.GetComponent<SampleDoorDebris>())
                    {
                        doorScripts.Add(instance.GetComponent<SampleDoorDebris>());
                    }
                }
            }

            // Adjust floor fades for each door
            foreach (SampleDoorDebris door in doorScripts)
            {
                door.AdjustFloorFade(floorHeight);
            }
        }
    }
}
