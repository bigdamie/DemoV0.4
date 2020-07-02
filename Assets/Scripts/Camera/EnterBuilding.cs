using Cinemachine;
using UnityEngine;

public class EnterBuilding : MonoBehaviour
{
    Collider theTrigger;
    [SerializeField] CinemachineVirtualCamera vmCam;
    Camera mainCam;

    [SerializeField] GameObject roof;

    void Start()
    {
        theTrigger = GetComponent<Collider>();
        vmCam = GetComponentInChildren<CinemachineVirtualCamera>();
        mainCam = FindObjectOfType<Camera>();

        vmCam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mainCam.orthographic = false;
            vmCam.gameObject.SetActive(true);

            roof.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCam.orthographic = true;
            vmCam.gameObject.SetActive(false);

            roof.SetActive(true);
        }
    }
}
