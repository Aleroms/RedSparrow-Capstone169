using System.Collections;
using UnityEngine;

//Original Deadzone Script
public class DestroyZone : MonoBehaviour
{
	public Transform respawnPt;
    
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			CharacterController cc = other.GetComponent<CharacterController>();

			if (cc != null)
				cc.enabled = false;

			other.transform.position = respawnPt.position;
			StartCoroutine(CCEnableRoutine(cc));
		}
        else
        {
            Destroy(other.gameObject);//If the object that falls into the deadzone is anything other than a player, it gets destroyed
        }
        IEnumerator CCEnableRoutine(CharacterController cc)
        {
            yield return new WaitForSeconds(0.25f);

            if (cc != null)
                cc.enabled = true;
        }
	}
}
