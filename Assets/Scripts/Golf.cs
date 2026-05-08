using Unity.VisualScripting;
using UnityEngine;

public class Golf : MonoBehaviour
{
    public int playerCount = 4;
    public int curPlayer = 1;
    public GameObject[] golf;
    public GameObject start;
    public GameObject end;
    public float[] distances;
    public GameObject curplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golf[0].GetComponent<Ball>().turn = true;
        for (int i = 0; i < golf.Length; i++)
        {
            distances[i] = Vector2.Distance(golf[i].transform.position, end.transform.position);
        }
        curplay.GetComponent<SpriteRenderer>().color = golf[0].GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (golf[curPlayer-1].GetComponent<Ball>().turn == false)
        {
            distances[curPlayer-1] = Vector2.Distance(golf[curPlayer-1].transform.position, end.transform.position);
            curPlayer++;
            calculatedist();
            if (curPlayer > golf.Length) {
                golf[0].GetComponent<Ball>().turn = true;
                curPlayer = 1; 
            }
            curplay.GetComponent<SpriteRenderer>().color = golf[curPlayer-1].GetComponent<SpriteRenderer>().color;
            golf[curPlayer-1].GetComponent<Ball>().turn = true;
        }

    }
    public void calculatedist()
    {
        for (int i = 0;i < golf.Length; i++)
        {
            distances[i] = Vector2.Distance(golf[i].transform.position, end.transform.position);
        }
    }
}
