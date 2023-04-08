using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class wallFieldCreator : MonoBehaviour
{
    public Tilemap wallField;
    public Tile wallTile;
    public Vector3Int[] unusableTiles;
    List<Vector3Int> emptyTiles = new List<Vector3Int>();
    public GameObject[] enemy;


    // Start is called before the first frame update
    void Start()
    {
        CreateWallField();
        CreateEnemy();
    }

    public void CreateWallField()
    {
        for (int x = 1; x < 16; x++)       
        {
            for(int y = 0; y < 11; y++)
            {
                if(y % 2 == 0)
                {
                    CreateWallTile(new Vector3Int(x,y,0));
                }
                else if(y % 2 == 1)
                {
                    if(x % 2 == 1)
                    {
                        CreateWallTile(new Vector3Int(x,y,0));
                    }
                }
            }
        }
    }

    void CreateWallTile(Vector3Int tilePosition)
    {
        for(int i = 0; i < unusableTiles.Length; i++)
        {
            if(tilePosition == unusableTiles[i])
            {
                return;
            }
        }

        int randomProb = Random.Range(1, 100);
        if(randomProb <= 40)                    // 40% chances that a wall will be drawn.
        {
            wallField.SetTile(tilePosition, wallTile);
        }
        else
        {
            emptyTiles.Add(tilePosition);
        }
    }

    void CreateEnemy()
    {
        for(int i = 0; i < 5; i++)
        {
            var randomPosi = wallField.CellToWorld(emptyTiles[Random.Range(1, emptyTiles.Count)]);
            randomPosi.x += 0.5f;
            randomPosi.y += 0.5f;

            enemy[i].transform.position = randomPosi;
            enemy[i].SetActive(true);
            enemy[i].GetComponent<EnemyMovement>().SetupEnemy(wallField.WorldToCell(randomPosi));
        }
    }
}