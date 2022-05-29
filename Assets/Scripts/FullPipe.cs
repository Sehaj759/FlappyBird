using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPipe : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    int maxMidPieces = 5;
    float noPipeChance = 0.25f;
    public void CreateFullPipe(Vector3 position)
    {
        if (Random.Range(0.0f, 1.0f) <= noPipeChance)
            return;

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
    }
}
