using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamManager : MonoSingletone<GamManager>
{
    UnityAction showLog;

    public void SceneLoader()
    {
        showLog = SceneManage.Instance.TestFunction;
        showLog();
    }
}

