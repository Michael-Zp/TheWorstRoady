using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    public enum MenuPageType
    {
        MainMenu,
        LevelSelect,
        Highscores
    }

    [Serializable]
    public struct MenuPage
    {
        [SerializeField]
        public MenuPageType Type;
        public GameObject RootGameObject;
    }


    public MenuPage[] MenuPages;
    public MenuPageType CurrentShownMenu = MenuPageType.MainMenu;


    private void Start()
    {
        ShowMenu(CurrentShownMenu);
    }

    public void ShowMenu(MenuPageType type)
    {
        foreach (MenuPage page in MenuPages)
        {
            if(page.Type != type)
            {
                page.RootGameObject.SetActive(false);
            }
            else
            {
                page.RootGameObject.SetActive(true);
            }
        }
    }
}
