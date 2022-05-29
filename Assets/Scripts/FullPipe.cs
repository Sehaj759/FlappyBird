using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPipe : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    BoxCollider2D scoreCollider;

    int maxMidPieces = 5;
    float noPipeChance = 0.25f;

    bool birdCrossed = false;
    public bool UpdateScore { get; set; }

    void Awake()
    {
        scoreCollider = GetComponent<BoxCollider2D>();
    }

    public void CreateFullPipe(Vector3 position)
    {
        if (Random.Range(0.0f, 1.0f) <= noPipeChance)
        {
            scoreCollider.size = new Vector2(0.5f, 10.0f);
            return;
        }

        int topMidPiecs = Random.Range(1, maxMidPieces - 1);
        int bottomMidPiecs = maxMidPieces - topMidPiecs;

        transform.position = position;

        float topPieceHeight;
        float bottomPieceHeight;

        Pipe top = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity, transform);
        top.Initialize(topMidPiecs, true, out topPieceHeight);

        Pipe bottom = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity, transform);
        bottom.Initialize(bottomMidPiecs, false, out bottomPieceHeight);

        top.transform.localPosition = new Vector3(0, 5.0f, 0);
        bottom.transform.localPosition = new Vector3(0, -5.0f + bottomPieceHeight, 0);

        scoreCollider.size = new Vector2(0.5f, 10.0f - topPieceHeight - bottomPieceHeight);
        scoreCollider.offset = new Vector2(0.45f, (5.0f - topPieceHeight) - (scoreCollider.size.y / 2));
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
