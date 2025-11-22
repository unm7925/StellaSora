using UnityEngine;

public class FullScreenToggle : MonoBehaviour
{
    [SerializeField] private GameObject clickPanel;

    private bool isFullScreen = false;

    void Start()
    {
        clickPanel.SetActive(false);
    }
    
    public void OnFullScreenButtonClick()
    {
        if (isFullScreen) return;
        isFullScreen = true;

        UIManager.Instance.HideUI(() => clickPanel.SetActive(true));
    }
    
    public void OnClickPanelClick()
    {
        if (!isFullScreen) return;
        isFullScreen = false;

        clickPanel.SetActive(false);
        UIManager.Instance.ShowUI();
    }
}