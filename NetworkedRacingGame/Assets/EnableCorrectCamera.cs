using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnableCorrectCamera : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private PhotonView photonView;
    
    private void Start()
    {
        if (photonView.IsMine)
        {
            Camera[] cameras = FindObjectsOfType<Camera>();

            foreach (Camera cam in cameras)
            {
                cam.gameObject.SetActive(false);
            }
            
            myCamera.gameObject.SetActive(true);
        }
        else
        {
            myCamera.gameObject.SetActive(false);
        }


    }
}
