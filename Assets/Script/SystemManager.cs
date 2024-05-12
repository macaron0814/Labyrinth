using System;
using System.Threading.Tasks;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private GameObject goalUI;

    private AudioManager audioManager;
    private SceneConfig sceneConfig;

    private bool isKey = false;
    public bool IsKey
    {
        get { return isKey; }
        set
        {
            isKey = value;
            if(isKey) UnLockGoal();
        }
    }
    private bool isGoal = false;
    public bool IsGoal
    {
        get { return isGoal; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sceneConfig = GameObject.Find("SceneConfig").GetComponent<SceneConfig>();
    }

    private void UnLockGoal()
    {
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        goal.transform.GetComponent<SpriteRenderer>().enabled = false;
        goal.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        goal.transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.red;
    }

    public async void Goal()
    {
        isGoal = true;

        int seNum = 2;
        audioManager.SoundPlaySE(seNum);

        goalUI.SetActive(true);

        // 音声の長さ分 + 1秒待機
        TimeSpan delay = TimeSpan.FromSeconds(audioManager.se[seNum].length);
        await Task.Delay(delay);

        //タイトル画面へ
        sceneConfig.UpdateScene("Title");
    }
}
