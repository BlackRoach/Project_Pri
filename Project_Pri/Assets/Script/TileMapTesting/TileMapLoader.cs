using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using LitJson;

public class TileMapLoader : MonoBehaviour {

    private static TileMapLoader instance = null;
    public static TileMapLoader Instance
    {
        get
        {
            return instance;

        }
    }
    private void Awake()
    {

        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    private GameManager game;
    private int tilemapNum;
  
    public GameObject Grid; // 타일맵을 여기에 자식으로 넣어준다.
    private GameObject currentTileMap;

    private TextAsset jsonFile;
    private JsonData loadData;
    private string tilemapName; // 찾는 타일맵 파일 이름

  
    void Start () {
        game = GameObject.Find("*Manager").GetComponent<GameManager>();
        LoadJson();

        LoadMap(7);
       
    }

    private void LoadJson()
    {
        jsonFile = Resources.Load<TextAsset>("JsonDB/tileMapList");
        loadData = JsonMapper.ToObject(jsonFile.text);
        //Debug.Log(loadData.Count);
    }

    public void IncreaseNumber()
    {
        tilemapNum++;
     
    }

    public void ReduceNumber()
    {
        if (tilemapNum == 1)
            return;
        tilemapNum--;
     
    }

    public void LoadMap(int num)
    {
        
        string id = "5500" + num; // 찾는 아이디 번호
        //Debug.Log(id);
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                tilemapName = loadData[i]["TILEMAP_NAME"].ToString();
            }
        }

        //Debug.Log(tilemapName);

        // tile은 Asset 폴더 안에 있는 원본이다.
        GameObject tile = Resources.Load<GameObject>("Prefabs/KIM_Prefab/KIM_TileMap/" + tilemapName);
        if (tile != null)
        {
            DeleteMap();
            // currentTileMap은 Hierarchy 패널에 있는 복사본이고
            // tile은 Asset 폴더안에 있는 원본이다.
            currentTileMap = Instantiate(tile, Grid.transform);
        }
        else
        {
            Debug.Log("맵이 존재하지 않습니다.");
        }

        // currentTileMap = tile; // 이러면 원본이 들어가버린다.
        // 원본 프리팹을 Destroy() 할려면 에러가 발생한다.
    }


    private void DeleteMap()
    {
        Destroy(currentTileMap);
        
    }

    public void ChangeMap(int mapID)
    {
        DeleteMap();
        game.destroyGrid();
        string id = mapID.ToString(); // 찾는 아이디 번호
        //Debug.Log(id);
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                tilemapName = loadData[i]["TILEMAP_NAME"].ToString();
            }
        }

        // tile은 Asset 폴더 안에 있는 원본이다.
        GameObject tile = Resources.Load<GameObject>("Prefabs/KIM_Prefab/KIM_TileMap/" + tilemapName);
        if (tile != null)
        {
            currentTileMap = Instantiate(tile, Grid.transform);
           
            tile.GetComponent<TileMapInformation>().SpawnPlayer(0);
          
        }
        else
        {
            Debug.Log("맵이 존재하지 않습니다.");
        }
        // 현재맵을 지운다.
        // 새맵을 불러온다.
       
    }

    // Json에서 타일맵 리스트 데이터를 불러온다. 
    // 리스트 데이터에서 tileMapNum 에 해당하는 ID를 찾는다.
    // 해당 ID에서 파일명 (TILEMAP_NAME)을 찾는다. 
    // 파일명에 해당하는 파일을 Prefabs/KIM.TileMap/에서 찾아 불러온다.
    // 로드시 기존맵을 지우고 새로운 맵을 불러온다.
    // QualitySettings에서 Anti Aliasing을 Disable 해줘서 타일사이의 공백을 안보이게 한다.

    

}
