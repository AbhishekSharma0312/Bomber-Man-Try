using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bombScript : MonoBehaviour
{
    public GameObject goPlayer;
    public ExplosionManagerScript explosionManagerScript;
    public Tilemap fixedField;
    public Tilemap wallField;
    public bool bombPlanted = false;
    public int bombBlastLength = 3;

    private void OnEnable() 
    {
        SetupBomb();
    }

    private void OnDisable() 
    {
        bombPlanted = false;
    }

    public void SetupBomb()
    {
        bombPlanted = true;
        StartCoroutine(StartBombTimer());
    }

    IEnumerator StartBombTimer()
    {
        yield return new WaitForSecondsRealtime(3);
        CheckAndBlastWall();
    }

    void CheckAndBlastWall()
    {
        bool topWallBlasted = false;
        bool buttomWallBlasted = false;
        bool leftWallBlasted = false;
        bool rightWallBlasted = false;

        var bombPlantedPosition = wallField.WorldToCell(this.gameObject.transform.position);

        for(int i = 1; i <= bombBlastLength; i++)
        {
            if(fixedField.GetTile(new Vector3Int(bombPlantedPosition.x - i, bombPlantedPosition.y, 0)) == null)
            {
                if(!leftWallBlasted) CheckForPlayerInRange(new Vector3Int(bombPlantedPosition.x - i, bombPlantedPosition.y, 0));
                if(wallField.GetTile(new Vector3Int(bombPlantedPosition.x - i, bombPlantedPosition.y, 0)) != null && leftWallBlasted == false)
                {
                    leftWallBlasted = true;
                    wallField.SetTile(new Vector3Int(bombPlantedPosition.x - i, bombPlantedPosition.y, 0), null);
                }
            }
            else
            {
                leftWallBlasted = true;
            }

            if(fixedField.GetTile(new Vector3Int(bombPlantedPosition.x + i, bombPlantedPosition.y, 0)) == null)
            {
                if(!rightWallBlasted) CheckForPlayerInRange(new Vector3Int(bombPlantedPosition.x + i, bombPlantedPosition.y, 0));
                if(wallField.GetTile(new Vector3Int(bombPlantedPosition.x + i, bombPlantedPosition.y, 0)) != null && rightWallBlasted == false)
                {
                    rightWallBlasted = true;
                    wallField.SetTile(new Vector3Int(bombPlantedPosition.x + i, bombPlantedPosition.y, 0), null);
                }
            }
            else
            {
                rightWallBlasted = true;
            }

            if(fixedField.GetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y - i, 0)) == null)
            {
                if(!buttomWallBlasted) CheckForPlayerInRange(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y - i, 0));
                if(wallField.GetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y - i, 0)) != null && buttomWallBlasted == false)
                {
                    buttomWallBlasted = true;
                    wallField.SetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y - i, 0), null);
                }
            }
            else
            {
                buttomWallBlasted = true;
            }

            if(fixedField.GetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y + i, 0)) == null)
            {
                if(!topWallBlasted) CheckForPlayerInRange(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y + i, 0));
                if(wallField.GetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y + i, 0)) != null && topWallBlasted == false)
                {
                    topWallBlasted = true;
                    wallField.SetTile(new Vector3Int(bombPlantedPosition.x, bombPlantedPosition.y + i, 0), null);
                }
            }
            else
            {
                topWallBlasted = true;
            }
        }
        CheckForPlayerInRange(bombPlantedPosition);
        this.gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
    }

    void CheckForPlayerInRange(Vector3Int blastPosition)
    {
        DisplayBlastEffect(blastPosition);
        if(fixedField.WorldToCell(goPlayer.transform.position) == blastPosition)
        {
            Debug.Log("Player Died");
        }
    }

    void DisplayBlastEffect(Vector3Int blastPosition)
    {
        explosionManagerScript.DisplayExplosionEffect(blastPosition);
    }
}