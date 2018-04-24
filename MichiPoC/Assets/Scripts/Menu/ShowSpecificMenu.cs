using UnityEngine;

public class ShowSpecificMenu : MonoBehaviour
{
    public MenuManager.MenuPageType TypeToShow = MenuManager.MenuPageType.MainMenu;

    public void ShowMenu()
    {
        FindObjectOfType<MenuManager>().ShowMenu(TypeToShow);
    }

}
