using System;
using Unity.VisualScripting;
using UnityEngine;

public class Golf : MonoBehaviour
{
    public int playerCount = 4;
    public int curPlayer = 0;
    public GameObject[] golf;
    public GameObject start;
    public GameObject end;
    public GameObject curplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golf[0].GetComponent<Ball>().turn = true;
        for (int i = 0; i < golf.Length; i++)
        {
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
        }
        curplay.GetComponent<SpriteRenderer>().color = golf[0].GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (golf[curPlayer].GetComponent<Ball>().turn == false)
        {


            calculatedist();
            if (curPlayer > playerCount - 2)
            {


                curPlayer = 0;
                WhoOut();
                for (int i = 0; i < golf.Length; i++)
                {
                    golf[i].GetComponent<CircleCollider2D>().enabled = false;
                }
                golf[0].GetComponent<Ball>().turn = true;
            }


            curPlayer++;
            curplay.GetComponent<SpriteRenderer>().color = golf[curPlayer].GetComponent<SpriteRenderer>().color;
            golf[curPlayer].GetComponent<Ball>().turn = true;
        }

    }
    public void calculatedist()
    {
        for (int i = 0;i < playerCount; i++)
        {
            golf[i].GetComponent<Ball>().distances = Vector2.Distance(golf[i].transform.position, end.transform.position);
            if (golf[i].GetComponent<Ball>().inHole)
            {
                golf[i].GetComponent<Ball>().distances = 0;
                    }
        }
    }
    public void WhoOut()
    {
        int roundOut = 0;
        int remain = playerCount;
        for (int i = 0; i < playerCount; i++)
        {
            
            if (golf[i].GetComponent<Ball>().distances == 0)
            {
                roundOut++;
                playerCount--;
            }
            

        }
        if (roundOut == 0)
        {
            playerCount--;
        }
        GameObject[] tem = new GameObject[playerCount];
        for (int i = 0; i < remain; i++)
        {
            int j = playerCount;
            for (int j2 = 0; j2 < remain; j2++)
            {

                if (golf[i].GetComponent<Ball>().distances > golf[j2].GetComponent<Ball>().distances && i != j2)
                {
                    j--;
                }

            }
            if (j > 0 && roundOut > 0)
            {
                tem[j - 1] = golf[i];
            }
            else if (j > 0 && j < playerCount && roundOut < 1)
            {
            
                    tem[j-1] = golf[i];
                
            }
        }
        for (int i = 0;i < playerCount; i++)
        {
            golf[i] = tem[i];
        }
    }

}
