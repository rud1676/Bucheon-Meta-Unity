using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] _case;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _case.Length; i++)
        {
            _case[i].SetActive(false);
        }
        int caseNumber = Random.Range(0, _case.Length);
        _case[caseNumber].SetActive(true);
        int case2Number = caseNumber;
        while (case2Number == caseNumber)
        {
            case2Number = Random.Range(0, _case.Length);
        }
        _case[case2Number].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
