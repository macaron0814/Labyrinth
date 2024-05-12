using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private MapCreateManager mapCreateManager = null;
    private SystemManager systemManager = null;
    private AudioManager audioManager = null;

    private void Start()
    {
        mapCreateManager = GameObject.Find("MapCreateManager").GetComponent<MapCreateManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        //ゴールした後は動けないようにする
        if (systemManager.IsGoal) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 nextPos = transform.localPosition;
            nextPos.y += 1.0f;
            int mathType = mapCreateManager.CheckNextGoToMath(nextPos);
            if(mathType != 0) MathTypeAction(mathType, nextPos);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 nextPos = transform.localPosition;
            nextPos.y -= 1.0f;
            int mathType = mapCreateManager.CheckNextGoToMath(nextPos);
            if (mathType != 0) MathTypeAction(mathType, nextPos);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 nextPos = transform.localPosition;
            nextPos.x-= 1.0f;
            int mathType = mapCreateManager.CheckNextGoToMath(nextPos);
            if (mathType != 0) MathTypeAction(mathType, nextPos);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 nextPos = transform.localPosition;
            nextPos.x += 1.0f;
            int mathType = mapCreateManager.CheckNextGoToMath(nextPos);
            if (mathType != 0) MathTypeAction(mathType, nextPos);
        }
    }

    void MathTypeAction(int mathType, Vector3 nextPos)
    {
        switch (mathType)
        {
            //道
            case 1:
                transform.localPosition = nextPos;
                break;
            //鍵
            case 2:
                systemManager.IsKey = true;
                transform.localPosition = nextPos;
                GameObject.FindGameObjectWithTag("Key").GetComponent<SpriteRenderer>().enabled = false;
                audioManager.SoundPlaySE(1);
                break;
            //ゴール
            case 3:
                transform.localPosition = nextPos;
                if (systemManager.IsKey)
                {
                    systemManager.Goal();
                }
                break;
        }
    }
}