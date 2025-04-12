using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartUI : MonoBehaviour
{
    public Button _startBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddButtonListener(UnityAction callback)
    {
        _startBtn.onClick.AddListener(callback);
    }
}
