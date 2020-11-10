using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneLights : MonoBehaviour
{
    [SerializeField]
    private GameObject DomeBridge;
    [SerializeField]
    private GameObject DomePlatform;

    private GameManager _gm;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gm == null) Debug.LogError("Gm null");
        if(DomePlatform.activeSelf)
        {
            DomePlatform.SetActive(false);
        }
        if(!DomeBridge.activeSelf)
        {
            DomeBridge.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DomeBridge.SetActive(false);
            DomePlatform.SetActive(true);

            StartCoroutine(EndCoroutine());
        }
    }
    IEnumerator EndCoroutine()
	{
        yield return new WaitForSeconds(60f);
        _gm.OnPlayerDeath();
	}
}
