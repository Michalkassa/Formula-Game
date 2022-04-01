using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public GameObject localCam;

    void Start()
    {
        if (!photonView.isMine)
        {
            localCam.SetActive(false);

            //turn off all player related scripts:
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i] is InputManager) scripts[i].enabled = false; ;
                // if (scripts[i] is NetworkPlayer) continue;
                // if (scripts[i] is NetworkManagement) continue;
                // if (scripts[i] is PhotonView) continue;
                // if (scripts[i] is CarController) continue;

                // scripts[i].enabled = false;
            }
        }
    }
}
