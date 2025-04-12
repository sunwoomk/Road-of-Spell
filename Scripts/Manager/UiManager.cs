using UnityEngine;
using UnityEngine.Events;

public class UiManager : MonoSingletone<UiManager>
{
    public Transform _canvasTran;

    void Start()
    {
        GameObject res = Resources.Load<GameObject>("Prefab/UI/StartUI");
        GameObject go = Instantiate(res, _canvasTran, false);
        StartUI comp = go.GetComponent<StartUI>();
        comp.AddButtonListener(GamManager.Instance.SceneLoader);
    }
}
