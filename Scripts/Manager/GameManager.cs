using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletone<GamManager>
{
    UnityAction showLog;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //showLog = TestFunction;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            //showLog();
        }

    }

    public void SceneLoader()
    {
        showLog = SceneManage.Instance.TestFunction;
        showLog();
    }
}

