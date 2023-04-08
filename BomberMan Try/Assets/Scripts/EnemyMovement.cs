using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    enum EnMoveDirection
    {
        left,
        right,
        up,
        down
    }
    public Tilemap wallField, fixedField;
    Vector3Int startPoint;
    Vector3Int prevPoint;
    Vector3Int endPoint;
    EnMoveDirection moveDirection;
    public Transform bombPosition;

    int pathPointCount;
    bool findPoint = false;

    public void SetupEnemy(Vector3Int startPos)
    {
        startPoint = startPos;
        endPoint = startPoint;
        prevPoint = startPoint;
        GetNewDirection();
        MoveEnemy();
    }

    void GetNewDirection()
    {
        if(fixedField.GetTile(new Vector3Int(endPoint.x - 1, endPoint.y, 0)) == null)
        {
            if(wallField.GetTile(new Vector3Int(endPoint.x - 1, endPoint.y, 0)) == null)
            {
                if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(startPoint.x - 1, startPoint.y, 0))
                {
                    moveDirection = EnMoveDirection.left;
                }
            }
        }

        if(fixedField.GetTile(new Vector3Int(endPoint.x + 1, endPoint.y, 0)) == null)
        {
            if(wallField.GetTile(new Vector3Int(endPoint.x + 1, endPoint.y, 0)) == null)
            {
                if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(endPoint.x + 1, endPoint.y, 0))
                {
                    moveDirection = EnMoveDirection.right;
                }
            }
        }

        if(fixedField.GetTile(new Vector3Int(endPoint.x, endPoint.y  - 1, 0)) == null)
        {
            if(wallField.GetTile(new Vector3Int(endPoint.x, endPoint.y - 1, 0)) == null)
            {
                if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(endPoint.x, endPoint.y - 1, 0))
                {
                    moveDirection = EnMoveDirection.down;
                }
            }
        }

        if(fixedField.GetTile(new Vector3Int(endPoint.x, endPoint.y  + 1, 0)) == null)
        {
            if(wallField.GetTile(new Vector3Int(endPoint.x, endPoint.y + 1, 0)) == null)
            {
                if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(endPoint.x, endPoint.y + 1, 0))
                {
                    moveDirection = EnMoveDirection.up;
                }
            }
        } 
        FindNextPosition();
    }

    public void FindNextPosition()
    {
        switch(moveDirection)
        {
            case EnMoveDirection.left:
                    if(fixedField.GetTile(new Vector3Int(startPoint.x - 1, startPoint.y, 0)) == null)
                    {
                        if(wallField.GetTile(new Vector3Int(startPoint.x - 1, startPoint.y, 0)) == null)
                        {
                            if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(startPoint.x - 1, startPoint.y, 0))
                            {
                                endPoint = new Vector3Int(startPoint.x - 1, startPoint.y, 0);
                                startPoint = endPoint;
                            }
                            else
                            {
                                GetNewDirection();
                            }
                        }
                        else
                        {
                            GetNewDirection();
                        }
                    }
                    else
                    {
                        GetNewDirection();
                    }
                    break;
            case EnMoveDirection.right:
                    if(fixedField.GetTile(new Vector3Int(startPoint.x + 1, startPoint.y, 0)) == null)
                    {
                        if(wallField.GetTile(new Vector3Int(startPoint.x + 1, startPoint.y, 0)) == null)
                        {
                            if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(startPoint.x + 1, startPoint.y, 0))
                            {
                                endPoint = new Vector3Int(startPoint.x + 1, startPoint.y, 0);
                                startPoint = endPoint;
                            }
                            else
                            {
                                GetNewDirection();
                            }
                        }
                        else
                        {
                            GetNewDirection();
                        }
                    }
                    else
                    {
                        GetNewDirection();
                    }
                    break;
            case EnMoveDirection.up:
                    if(fixedField.GetTile(new Vector3Int(startPoint.x, startPoint.y + 1, 0)) == null)
                    {
                        if(wallField.GetTile(new Vector3Int(startPoint.x, startPoint.y + 1, 0)) == null)
                        {
                            if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(startPoint.x, startPoint.y + 1, 0))
                            {
                                endPoint = new Vector3Int(startPoint.x, startPoint.y + 1, 0);
                                startPoint = endPoint;
                            }
                            else
                            {
                                GetNewDirection();
                            }
                            
                        }
                        else
                        {
                            GetNewDirection();
                        }
                    }
                    else
                    {
                        GetNewDirection();
                    }
                    break;
            case EnMoveDirection.down:
                    if(fixedField.GetTile(new Vector3Int(startPoint.x, startPoint.y - 1, 0)) == null)
                    {
                        if(wallField.GetTile(new Vector3Int(startPoint.x, startPoint.y - 1, 0)) == null)
                        {
                            if(wallField.WorldToCell(bombPosition.position) != new Vector3Int(startPoint.x, startPoint.y - 1, 0))
                            {
                                endPoint = new Vector3Int(startPoint.x, startPoint.y - 1, 0);
                                startPoint = endPoint;
                            }
                            else
                            {
                                GetNewDirection();
                            }
                        }
                        else
                        {
                            GetNewDirection();
                        }
                    }
                    else
                    {
                        GetNewDirection();
                    }
                    break;
        }
    }

    void MoveEnemy()
    {
        var seq = DOTween.Sequence();
        var posi = wallField.CellToWorld(endPoint);
        posi.x += 0.5f;
        posi.y += 0.5f;

        seq.Append(gameObject.transform.DOLocalMove(posi, 0.7f).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        { 
            FindNextPosition();
        });
        seq.AppendCallback(() => MoveEnemy());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Killed");
            other.gameObject.SetActive(false);
            GUIManager.Instance.GameEnd(false);
        }    
    }
}
