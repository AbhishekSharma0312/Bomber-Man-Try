using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExplosionManagerScript : MonoBehaviour
{
    public Tilemap fixedField;
    public GameObject ExplosionEffectPrefab;
    List<GameObject> ExpPool = new List<GameObject>();
    public wallFieldCreator sWallFieldCreator;
    public GameObject player;

    public int EnemyKillCount = 0;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemyKillCount = 0;
        CreateExpPool();
    }

    void CreateExpPool()
    {
        for(int i = 0; i < 20; i++)
        {
            var expObj = Instantiate(ExplosionEffectPrefab, this.transform);
            ExpPool.Add(expObj);
        }
    }

    GameObject GetExpObjFromPool()
    {
        for(int i = 0; i < ExpPool.Count; i++)
        {
            if(ExpPool[i].activeInHierarchy == false) 
            {
                return ExpPool[i];
            }
        }
        return null;
    }

    public void DisplayExplosionEffect(Vector3Int blastPosition)
    {
        var blastEffect = GetExpObjFromPool();
        var position = fixedField.CellToWorld(blastPosition);
        position.x += 0.5f;
        position.y += 0.5f;

        blastEffect.transform.position = position;
        blastEffect.SetActive(true);
        if(fixedField.WorldToCell(player.transform.position) == blastPosition)
        {
            Debug.Log("player Killed");
            player.SetActive(false);
            GUIManager.Instance.GameEnd(false);
        }
        CheckForEnemyKill(blastPosition);
    }

    public void CheckForEnemyKill(Vector3Int blastPosition)
    {
        for(int i=0; i < sWallFieldCreator.enemy.Length; i++)
        {
            if(sWallFieldCreator.enemy[i].activeInHierarchy)
            {
                if(fixedField.WorldToCell(sWallFieldCreator.enemy[i].transform.position) == blastPosition)
                {
                    Debug.Log("Enemy Killed");
                    score += 100;
                    GUIManager.Instance.InGameScoreTxt.text = score.ToString();
                    EnemyKillCount++;
                    sWallFieldCreator.enemy[i].SetActive(false);
                    // Destroy(sWallFieldCreator.enemy[i]);
                }
            }
        }

        if(EnemyKillCount == 5)
        {
            GUIManager.Instance.GameEnd(true);
        }
    }
}