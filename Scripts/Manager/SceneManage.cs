using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoSingletone<SceneManage>
{
    public void TestFunction()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
