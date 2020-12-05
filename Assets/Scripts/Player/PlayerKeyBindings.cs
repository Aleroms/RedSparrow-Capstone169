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
        public string reload;
        public string switchGuns;
        public string zoom;
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
        return PlayerPrefs.GetString("shoot", keyBindings.shoot);
    }
    public string getPickUp() {
        return PlayerPrefs.GetString("pickUp", keyBindings.pickUp);
    }
    public string getDrop() {
        return PlayerPrefs.GetString("drop", keyBindings.drop);
    }
    public string getSwitchFireType()
    {
        return PlayerPrefs.GetString("switchFireType", keyBindings.switchFireType);
    }
    public string getReloadKey() {
        return PlayerPrefs.GetString("reload", keyBindings.reload);
    }
    public string getSwitchGunsKey() {
        return PlayerPrefs.GetString("switchGuns", keyBindings.switchGuns);
    }
    public string getZoomKey()
    {
        return PlayerPrefs.GetString("zoom", keyBindings.zoom);
    }


    public void setshootGun(string input) {
        PlayerPrefs.SetString("shoot", input);
    }
    public void setPickUp(string input)
    {
        PlayerPrefs.SetString("pickUp", input);
    }
    public void setDrop(string input)
    {
        PlayerPrefs.SetString("drop", input);
    }
    public void setSwitchFireType(string input) {
        PlayerPrefs.SetString("switchFireType", input);
    }
    public void setReloadKey(string input) {
        PlayerPrefs.SetString("reload", input);
    }
    public void setSwitchGunsKey(string input)
    {
        PlayerPrefs.SetString("switchGuns", input);
    }
    public void setZoomKey(string input)
    {
        PlayerPrefs.SetString("zoom", input);
    }

    public void resetKeyBindings() {
        PlayerPrefs.SetString("shoot", keyBindings.shoot);
        PlayerPrefs.SetString("pickUp", keyBindings.pickUp);
        PlayerPrefs.SetString("drop", keyBindings.drop);
        PlayerPrefs.SetString("switchFireType", keyBindings.switchFireType);
        PlayerPrefs.SetString("reload", keyBindings.reload);
        PlayerPrefs.SetString("switchGuns", keyBindings.switchGuns);
        PlayerPrefs.SetString("zoom", keyBindings.zoom);
    }
}
