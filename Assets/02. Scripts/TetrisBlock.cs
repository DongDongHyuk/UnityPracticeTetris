using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 Rttpoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position += new Vector3(-1, 0 , 0);
            if(!VaildMove())
                transform.position -= new Vector3(-1, 0 , 0);
        }        
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position += new Vector3(1, 0 , 0);
            if(!VaildMove())
                transform.position -= new Vector3(1, 0 , 0);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(Rttpoint), new Vector3(0, 0, 1), -90);
            if(!VaildMove())
                transform.RotateAround(transform.TransformPoint(Rttpoint), new Vector3(0, 0, 1), 90);
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)){
            transform.position += new Vector3(0, -1, 0);
            if(!VaildMove()){
                transform.position -= new Vector3(0, -1 , 0);
                this.enabled = false;
                FindObjectOfType<SpawnTetris>().NewTetris();
            }
            previousTime = Time.time;
        }
    }

    bool VaildMove(){
        foreach(Transform i in transform){
            int roundedX = Mathf.RoundToInt(i.transform.position.x);
            int roundedY = Mathf.RoundToInt(i.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }
        }

        return true;
        
    }
}
