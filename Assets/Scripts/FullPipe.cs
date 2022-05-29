using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPipe : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    [SerializeField]
    BoxCollider2D scoreCollider;

    float noPipeChance = 0.25f;

    bool birdCrossed = false;
    public bool UpdateScore { get; set; }

    public void OffsetOrDisableFullPipe(float offset)
    {
        if(Random.Range(0.0f, 1.0f) < noPipeChance)
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!birdCrossed)
        {
            birdCrossed = true;
            UpdateScore = true;
        }
    }
}
