using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private Button levelBtn;
    public int levelReg;

    void Start()
    {
        levelBtn = GetComponent<Button>();
        if (PlayerPrefs.GetInt("Level",1)>=levelReg)
        {
            levelBtn.onClick.AddListener(() => LoadLevel());
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = .5f;
        }
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
