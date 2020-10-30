using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	public float cooldown;
	public Image healthbar;

	private Player _player;
	private int _health;
	// Start is called before the first frame update
	void Start()
    {
		_player = GameObject.Find("Player").GetComponent<Player>();

		if (_player == null) Debug.LogError("player is null");
		_health = _player.GetCurrentHealth();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.V) && cooldown == 0)
		{
			cooldown = 2;
			_health -= 15;
			print("HP: " + (_health + 15) + " -> " + _health);
			if (_health < 0)
			{
				print("Game Over");
				healthbar.enabled = false;
			}

			else
			{
				healthbar.transform.localScale = new Vector3(_health / 100f, 1, 1);
				healthbar.transform.localPosition = new Vector2((1f - healthbar.transform.localScale.x) * -250f, healthbar.transform.localPosition.y);
			}


		}

		if (cooldown > 0)
			cooldown -= Time.deltaTime;
		if (cooldown <= 0)
			cooldown = 0;
	}
}
