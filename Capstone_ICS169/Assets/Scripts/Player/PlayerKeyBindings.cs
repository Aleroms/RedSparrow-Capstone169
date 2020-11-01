using UnityEngine;

//Keeps track of the player's keybindings
//Hopefully this makes it eaisier to create a settings menu in the future
public class PlayerKeyBindings : MonoBehaviour
{

    [System.Serializable]
    private class Bindings {
        public string shoot;
        public string pickUp;
        public string drop;
        public string switchFireType;
    }

    [SerializeField]
    private Bindings keyBindings = new Bindings();
    /*
     * note for Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), player.GetComponent<PlayerKeyBindings>().getshootGun())) cause you're gonna see it in the shooting scripts
     * The keycode for "left click" is called "Mouse0" in unity however we cant simply say  Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getshootGun()) like we normaly would
     * This is because for some reason the string "Mouse0" is and unknown input name. In Unity only single letters are known input names.
     * This is why Input.GetKeyDown(player.GetComponent<PlayerKeyBindings>().getPickUp()) is valid
     * Anyway's long story short we need to turn the string "Mouse0" into the Keycode "Mouse0" which is why the "(KeyCode)System.Enum.Parse(typeof(KeyCode),"  is needed
    */
    public string getshootGun() {
        return keyBindings.shoot;
    }
    public string getPickUp() {
        return keyBindings.pickUp;
    }
    public string getDrop() {
        return keyBindings.drop;
    }
    public string getSwitchFireType()
    {
        return keyBindings.switchFireType;
    }
    public void setshootGun(string input) {
        keyBindings.shoot = input;
    }
    public void setPickUp(string input)
    {
        keyBindings.pickUp = input;
    }
    public void setDrop(string input)
    {
        keyBindings.drop = input;
    }
    public void setSwitchFireType(string input) {
        keyBindings.switchFireType = input;
    }
}
