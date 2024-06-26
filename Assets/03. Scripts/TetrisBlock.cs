using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 Rttpoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];

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
            transform.RotateAround(transform.TransformPoint(Rttpoint), new Vector3(0, 0, 1), 90);
            if(!VaildMove())
                transform.RotateAround(transform.TransformPoint(Rttpoint), new Vector3(0, 0, 1), -90);
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)){
            transform.position += new Vector3(0, -1, 0);
            if(!VaildMove()){
                transform.position -= new Vector3(0, -1 , 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetris>().NewTetris();
            }
            previousTime = Time.time;
        }
    }

    // 라인 확인 + 삭제 + 라인 내리기
    void CheckForLines()
    {
        for (int i = height -1 ; i >= 0 ; i--){
            if(HasLine(i)){
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    // 라인 확인
    bool HasLine(int i){
        for(int j = 0; j< width; j++){
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i){
        for(int j = 0; j< width; j++){
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i){
        for (int y = i; y < height; y++){
            for (int j = 0; j < width; j++){
                if(grid[j, y] != null){
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid(){
        foreach(Transform i in transform){
            int roundedX = Mathf.RoundToInt(i.transform.position.x);
            int roundedY = Mathf.RoundToInt(i.transform.position.y);
            grid[roundedX, roundedY] = i;
            Debug.Log(grid);
        }
    }

    bool VaildMove(){
        foreach(Transform i in transform){
            int roundedX = Mathf.RoundToInt(i.transform.position.x);
            int roundedY = Mathf.RoundToInt(i.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
        
    }
}
