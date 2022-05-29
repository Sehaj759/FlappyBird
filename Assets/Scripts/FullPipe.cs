using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPipe : MonoBehaviour
{
    BoxCollider2D scoreCollider;
    BoxCollider2D[] hitColliders;

    [SerializeField]
    GameObject sprites;

    bool disabled = false;
    Vector2 scoreColDimens;

    float noPipeChance = 0.25f;

    bool birdCrossed = false;
    public bool UpdateScore { get; set; }

    void Awake()
    {
        int curHitColIndex = 0;
        hitColliders = new BoxCollider2D[2];

        BoxCollider2D[] allColliders = GetComponents<BoxCollider2D>();
        for(int i = 0; i < allColliders.Length; ++i)
        {
            if (allColliders[i].isTrigger)
                scoreCollider = allColliders[i];
            else
                hitColliders[curHitColIndex++] = allColliders[i];
        }

        scoreColDimens = new Vector2(scoreCollider.size.x, scoreCollider.size.y);
    }

    public void OffsetOrDisableFullPipe()
    {
        if(Random.Range(0.0f, 1.0f) < noPipeChance)
        {
            sprites.SetActive(false);
            for(int i = 0; i < hitColliders.Length; ++i)
            {
                hitColliders[i].enabled = false;
            }
            scoreCollider.size = new Vector2(scoreCollider.size.x, 10.0f);
            disabled = true;
        }
        else
        {
            if (disabled)
            {
                sprites.SetActive(true);
                for (int i = 0; i < hitColliders.Length; ++i)
                {
                    hitColliders[i].enabled = true;
                }
                scoreCollider.size = scoreColDimens;
                disabled = true;
            }
            float offset = Random.Range(-3.0f, 3.0f);
            transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
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
