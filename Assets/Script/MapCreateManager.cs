using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreateManager : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile; // CSVファイル
    [SerializeField] private GameObject[] createType; //マスのタイプ

    private List<GameObject> createMaps = new List<GameObject>();

    private SystemManager systemManager = null;
    private AudioManager audioManager = null;

    // Use this for initialization
    void Start()
    {
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audioManager.SoundPlaySE(0);

        MapCreate();
    }

    /// <summary>
    /// マップの生成
    /// </summary>
    void MapCreate()
    {
        StringReader reader = new StringReader(csvFile.text);

        int rowScale = 0; //横列
        int columnScale = 0; //縦列
        int mapScale = 0; //全体のマス数

        //各マスのタイプが格納される
        List<string> mathTypes = new List<string>();

        //マスの数を取得
        while (reader.Peek() > -1)
        {
            string row = reader.ReadLine();
            rowScale++;

            string[] maths = row.Split(',');
            foreach (var math in maths)
            {
                mathTypes.Add(math);
            }
            mapScale += maths.Length;
        }
        columnScale = mapScale / rowScale;

        //各マスごとに指定されたタイプのものを生成
        for (int y = 0; y < rowScale; y++)
        {
            for (int x = 0; x < columnScale; x++)
            {
                int type = int.Parse(mathTypes[x + (y * columnScale)]);

                //中心がズレないように座標を設定する
                float posX = (-(columnScale - 1) / 2.0f) + x;
                float posY = (rowScale / 2.0f) - y;

                //生成
                GameObject math = Instantiate(createType[type], new Vector3(posX, posY, 0.0f), Quaternion.identity);
                math.name = type.ToString();
                createMaps.Add(math);
            }
        }

        //鍵がマップになければ、無条件でtrueにする
        systemManager.IsKey = CheckMathTypeKey();

        SetCameraSize();
    }

    /// <summary>
    /// Camera.mainのsizeを調整
    /// </summary>
    void SetCameraSize()
    {
        //アスペクト比
        int width = 16;
        int height = 9;

        //一番端に生成されたマスの座標から大きい方を取得
        Vector3 mapOutPos = createMaps[0].transform.localPosition;
        float mapOutMaxPos = Mathf.Max(mapOutPos.x, mapOutPos.y);

        //xが大きい場合
        if (mapOutMaxPos == mapOutPos.x)
        {
            while (Mathf.Abs(mapOutMaxPos) > width)
            {
                Camera.main.orthographicSize += 2;
                width += 2;
            }
        }
        //yが大きい場合
        if (mapOutMaxPos == mapOutPos.y)
        {
            while (Mathf.Abs(mapOutMaxPos) > height)
            {
                Camera.main.orthographicSize += 2;
                height += 2;
            }
        }

        //UI分の横幅
        int widthUI = 9;

        //UIに被らないようにさらに拡大
        while (Mathf.Abs(mapOutPos.x) > widthUI)
        {
            Camera.main.orthographicSize += 2;
            widthUI += 2;
        }
    }

    public int CheckNextGoToMath(Vector3 pos)
    {
        for (int i = 0; i < createMaps.Count; i++)
        {
            if(createMaps[i].transform.localPosition == pos)
            {
                return int.Parse(createMaps[i].name);
            }
        }
        return 0;
    }

    public bool CheckMathTypeKey()
    {
        for (int i = 0; i < createMaps.Count; i++)
        {
            if (createMaps[i].tag == "Key")
            {
                return false;
            }
        }
        return true;
    }
}
