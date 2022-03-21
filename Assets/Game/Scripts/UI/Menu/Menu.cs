using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] protected List<GameObject> DisableOnOpen;

   public virtual void OpenPanel(GameObject panel)
    {
        foreach (var gameObject in DisableOnOpen)
            gameObject.SetActive(false);

        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public virtual void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;

        foreach (var gameObject in DisableOnOpen)
            gameObject.SetActive(true);
    }

    public virtual void Exit()
    {
        Application.Quit();
    }
}