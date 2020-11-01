using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleToggle : MonoBehaviour
{
	[SerializeField]
	private float _delta = 3f;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(ToggleCoroutine());
	}

    // Update is called once per frame
    void Update()
    {
		
	}
	IEnumerator ToggleCoroutine()
	{
		while(true)
		{
			gameObject.SetActive(true);
			yield return new WaitForSeconds(_delta);
			gameObject.SetActive(false);
			yield return new WaitForSeconds(_delta);
		}
		
	}
}
