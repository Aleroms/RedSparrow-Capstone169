using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public Camera mainCamera;
    public bool isZoomed, equipped;
    public Sprite unzoom, zoom;
    public Image reticle;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //reticle = GameObject.Find("player_crosshair (new)").GetComponent<Image>();
        reticle = GameObject.Find("Reticle").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), player.GetComponent<PlayerKeyBindings>().getZoomKey())) && GetComponent<GunController>().isEquipped) //(Input.GetMouseButtonDown(2))
        {
            isZoomed = !isZoomed;
            if (isZoomed && reticle.GetComponent<Image>().sprite != zoom)
                ZoomIn();
            else if (!isZoomed && reticle.GetComponent<Image>().sprite != unzoom)
                ZoomOut();
        }
    }

    public void ZoomIn()
    {
        isZoomed = true;
        GetComponent<GunController>().reticleGun = zoom;
        mainCamera.fieldOfView = 15;
    }

    public void ZoomOut()
    {
        isZoomed = false;
        GetComponent<GunController>().reticleGun = unzoom;
        mainCamera.fieldOfView = 60;
    }
}
