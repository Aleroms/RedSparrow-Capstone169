using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script opens the door from section b to c after
//player has killed all targets.
public class OpensDoor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _LockedDoors;

    [SerializeField]
    private List<GameObject> _targets;

	private int _targetListLength;

	private void Start()
	{
		_targetListLength = _targets.Count;
	}
	private void Update()
	{
		int count = 0;
		for(int i=0; i < _targets.Count; i++)
		{
			if(_targets[i] == null)
			{
				count++;

				//all targets have been destroyed
				if(count == _targets.Count)
				{
					foreach (GameObject door in _LockedDoors)
						Destroy(door);
				}
			}
		}
	}
}
