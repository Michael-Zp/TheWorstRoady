using UnityEngine;

public class ShowMenuOnKeyPress : MonoBehaviour
{
    public MenuManager.MenuPageType PageToShow;

    public KeyCode KeyToPress;


    private MenuManager MenuManager;

    private void Start()
    {
        MenuManager = FindObjectOfType<MenuManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyToPress))
        {
            MenuManager.ShowMenu(PageToShow);
        }
    }

}
