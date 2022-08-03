using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MakeTrash : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<GameObject> paperTrashPrefabs;

    //생성할 쓰레기량 입력받기
    [SerializeField] int trashCount;

    //생성할 종이쓰레기량 입력받기
    [SerializeField] int paperTrashCount;

    //생성된 쓰레기 객체 담기
    public Stack<GameObject> Trashs = new Stack<GameObject>();

    //생성된 객체 담기
    public Stack<GameObject> paperTrash = new Stack<GameObject>();

    //캐릭터 중심으로 얼마나 떨어진 곳에 쓰레기 생성할지
    public float maxDistance;

    //캐릭터 중심으로 쓰레기 생성

    private Transform playerTransform;

    // 종이쓰레기 스폰지역리스트 받기
    private Transform[] paperSpawnerTransForm;

    // 종이쓰레기 스폰지역 사용중인 부분 체크
    private List<bool> usedPosition;
    void Start()
    {
        paperSpawnerTransForm = gameObject.GetComponentsInChildren<Transform>();
        usedPosition = new List<bool>();
        for (int i = 0; i < paperSpawnerTransForm.Length; i++)
        {
            usedPosition.Add(false);
        }
    }




    private void Update()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        }
        if (Trashs.Count < trashCount && playerTransform != null)
        {
            Trashs.Push(Spawn());
        }
        if (paperTrash.Count < paperTrashCount && playerTransform != null)
        {
            GameObject pTrashObject = SpanwPaper();
            if (pTrashObject != null)
            {
                paperTrash.Push(pTrashObject);
            }
        }
    }
    private GameObject Spawn()
    {
        var spawnPosition = GetRandomPointOnNavMesh(maxDistance, NavMesh.AllAreas);
        var item = Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPosition, Quaternion.identity);
        return item;
    }

    //랜덤 위치 생성 - NavMesh로 생성 - 캐릭터 중심 50f거리만큼 이내에서 새성ㅇ함
    public Vector3 GetRandomPointOnNavMesh(float distance, int areaMask)
    {
        Vector3 checkposition = Vector3.zero;
        NavMeshHit hit;
        do
        {
            var randomCenter = new Vector3(Random.Range(-150, 100), 0, Random.Range(-100, 40));
            var randomPos = Random.insideUnitSphere * distance + randomCenter;
            NavMesh.SamplePosition(randomPos, out hit, distance, areaMask);
            checkposition = hit.position;
        } while (float.IsInfinity(checkposition.x));
        return checkposition;
    }
    private GameObject SpanwPaper()
    {
        if (GetSpawnedPaperNum() >= paperSpawnerTransForm.Length - 1) return null;
        int randnum = 0;
        do
        {
            randnum = Random.Range(1, paperSpawnerTransForm.Length);
        } while (usedPosition[randnum] != false);

        usedPosition[randnum] = true;
        var randTransform = paperSpawnerTransForm[randnum];

        var result = Instantiate(paperTrashPrefabs[Random.Range(0, paperTrashPrefabs.Count)], randTransform.position, randTransform.rotation);
        return result;
    }

    private int GetSpawnedPaperNum()
    {
        int num = 0;
        for (int i = 0; i < paperSpawnerTransForm.Length; i++)
        {
            if (usedPosition[i] == true) num++;
        }
        return num;
    }
}
