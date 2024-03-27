using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetris : MonoBehaviour
{

    public GameObject[] Tetriss;

    // Start is called before the first frame update
    void Start()
    {
        NewTetris();
    }

    public void NewTetris(){
        Instantiate(Tetriss[Random.Range(0, Tetriss.Length)], transform.position, Quaternion.identity);
    }

}
