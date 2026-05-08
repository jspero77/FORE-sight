using UnityEngine;

public class Golf : MonoBehaviour
{
    public int playerCount = 4;
    public int curPlayer = 1;
    public GameObject[] golf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golf[0].GetComponent<Ball>().turn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (golf[curPlayer-1].GetComponent<Ball>().turn == false)
        {
            curPlayer++;
            if (curPlayer >= golf.Length) { curPlayer = 1; }
            golf[curPlayer-1].GetComponent<Ball>().turn = true;
        }
    }
}
