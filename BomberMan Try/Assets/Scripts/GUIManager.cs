using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public GameObject InGameUIPanel;
    public GameObject ResultPanel;
    public TextMeshProUGUI InGameScoreTxt;
    public TextMeshProUGUI ResultTxt;
    public TextMeshProUGUI ResultScoreValueTxt;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameUIPanel.SetActive(true);
        ResultPanel.SetActive(false);
    }

    public void GameEnd(bool playerWon)
    {
        InGameUIPanel.SetActive(false);
        ResultPanel.SetActive(true);
        ResultScoreValueTxt.text = InGameScoreTxt.text;

        if(playerWon)
        {
            ResultTxt.text = "You won";
        }
        else
        {
            ResultTxt.text = "You Lost";
        }
    }

    public void OnBtnRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
