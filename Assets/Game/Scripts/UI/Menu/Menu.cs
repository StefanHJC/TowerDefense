using UnityEngine;

public class Menu : MonoBehaviour
{
   public virtual void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public virtual void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public virtual void Exit()
    {
        Application.Quit();
    }
}
