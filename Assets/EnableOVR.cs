using OculusSampleFramework;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableOVR : MonoBehaviourPunCallbacks
{
    public GameObject ovrCR;
    //public HandsManager ovrManager;
    int hackId;

    private void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        //if (photonView.IsMine)
        //{
        //    ovrManager.enabled = true;
        //    ovrCR.SetActive(true);
        //    Debug.Log("ENABLING VR PLAYER");
        //}
        //else
        if (SceneManager.GetActiveScene().name == "BattleRoomClean")
        {
            if (photonView.IsMine)
            {
                ovrCR.SetActive(true);
                Debug.Log("ENABLING VR PLAYER: " + photonView.IsOwnerActive);
            }
            else
            {
                ovrCR.SetActive(false);
                Debug.Log("DISABLING VR PLAYER: " + photonView.IsOwnerActive);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //if (photonView.IsMine)
        //{
        //    ovrManager.enabled = true;
        //    ovrCR.SetActive(true);
        //    Debug.Log("ENABLING VR PLAYER ON START");
        //}
        //else
        //{
        //    ovrManager.enabled = false;
        //    ovrCR.SetActive(false);
        //    Debug.Log("DISABLING VR PLAYER ON START");
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
