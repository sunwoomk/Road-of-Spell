using UnityEngine;
using UnityEngine.Events;

public class GamManager : MonoBehaviour
{
    public Transform _canvasTran;
    private int _number;

    
    UnityAction showLog;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject res = Resources.Load<GameObject>("Prefab/UI/StartUI");
        GameObject go = Instantiate(res, _canvasTran, false);
        StartUI comp = go.GetComponent<StartUI>();
        comp.AddButtonListener(TestFunction);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            showLog = TestFunction;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            showLog();
        }

    }

    private void TestFunction()
    {
        Debug.Log("Scene ÀüÈ¯ ");
    }
}

