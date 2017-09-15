using UnityEngine;

[AddComponentMenu ("Extended/UI/Menu/Credits Screen")]
public class CreditsScreenController : MonoBehaviour
{
    public void Menu ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }
}
