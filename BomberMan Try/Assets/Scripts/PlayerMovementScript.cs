using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 0.025f;
    public GameObject[] playerFacingAnims;
    public GameObject goBomb;
    public Tilemap playGround;

    private void Start() {
        goBomb.SetActive(false);
    }

    void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");

        if(Input.GetAxis("Horizontal") < 0)
        {
            PlayPlayerLeftFacingAnim();
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            PlayPlayerRightFacingAnim();
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            PlayPlayerDownFacingAnim();
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            PlayPlayerUpFacingAnim();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }

        Vector3 moveDirection = new Vector3(xDirection, yDirection, 0.0f);
        transform.position += moveDirection * speed;
    }

    void PlaceBomb()
    {
        if(goBomb.activeInHierarchy)
        {
            return;
        }
        
        var playerPosition = playGround.WorldToCell(this.transform.position);
        var bombPosition = playGround.CellToWorld(playerPosition);

        bombPosition.x += 0.5f;
        bombPosition.y += 0.5f;

        goBomb.transform.position = bombPosition;
        goBomb.SetActive(true);
    }

    void resetFacingAnimation()
    {
        for (int i = 0; i < playerFacingAnims.Length; i++)
        {
            playerFacingAnims[i].SetActive(false);
        }
    }

    void PlayPlayerUpFacingAnim()
    {
        resetFacingAnimation();
        playerFacingAnims[0].SetActive(true);
    }

    void PlayPlayerDownFacingAnim()
    {
        resetFacingAnimation();
        playerFacingAnims[1].SetActive(true);
    }

    void PlayPlayerLeftFacingAnim()
    {
        resetFacingAnimation();
        playerFacingAnims[2].SetActive(true);
    }

    void PlayPlayerRightFacingAnim()
    {
        resetFacingAnimation();
        playerFacingAnims[3].SetActive(true);
    }
}