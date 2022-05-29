using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    GameObject TopHalfEnd;

    [SerializeField]
    GameObject MidPiece;

    [SerializeField]
    GameObject BottomHalfEnd;

    BoxCollider2D boxCollider;

    Vector3 localTopLeft = Vector3.zero;
    float scale = 3.0f;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    public void Initialize(int nMidPieces, bool isTopHalf, out float height)
    {
        if (nMidPieces == 0)
        {
            height = 0;
            return;
        }

        float endPieceWidth; 
        float endPieceHeight;
        float midPieceWidth; 
        float midPieceHeight;
        Vector3 position = localTopLeft;

        GameObject endPiece;
        if (isTopHalf)
        {
            endPiece = Instantiate(TopHalfEnd, position, Quaternion.identity, transform);
        }
        else
        {
            endPiece = Instantiate(BottomHalfEnd, position, Quaternion.identity, transform);
        }
        SpriteRenderer endPieceRenderer = endPiece.GetComponent<SpriteRenderer>();
        endPieceWidth = endPieceRenderer.bounds.size.x;
        endPieceHeight = endPieceRenderer.bounds.size.y;

        if (!isTopHalf)
        {
            endPiece.transform.localPosition = new Vector3(position.x + endPieceWidth / 2, position.y - endPieceHeight / 2, 0);
            position.y -= endPieceHeight;
        }

        // Initialize first mid piece
        GameObject mp = Instantiate(MidPiece, position, Quaternion.identity, transform);
        SpriteRenderer mpRenderer = mp.GetComponent<SpriteRenderer>();
        midPieceWidth = mpRenderer.bounds.size.x;
        midPieceHeight = mpRenderer.bounds.size.y;

        float pieceOffset = (endPieceWidth - midPieceWidth) / 2;
        position.x += pieceOffset;
        mp.transform.localPosition = new Vector3(position.x + midPieceWidth / 2, position.y - midPieceHeight / 2, 0);
        position.y -= midPieceHeight;

        // Initialize remaining mid pieces
        for (int i = 1; i < nMidPieces; i++)
        {
            mp = Instantiate(MidPiece, position, Quaternion.identity, transform);
            mp.transform.localPosition = new Vector3(position.x + midPieceWidth / 2, position.y - midPieceHeight / 2, 0);
            position.y -= midPieceHeight;
        }

        if (isTopHalf)
        {
            position.x -= pieceOffset;
            endPiece.transform.localPosition = new Vector3(position.x + endPieceWidth / 2, position.y - endPieceHeight / 2, 0);
        }

        float colliderHeight = endPieceHeight + nMidPieces * midPieceHeight;
        float colliderWidth = endPieceWidth;

        height = scale * colliderHeight;

        boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        boxCollider.offset = new Vector2(localTopLeft.x + colliderWidth / 2, localTopLeft .y - colliderHeight / 2);

        transform.localScale = scale * Vector3.one;
    }
}
