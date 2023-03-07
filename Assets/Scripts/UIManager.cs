using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    //Menu UI
    [SerializeField] private GameObject controlsPanel;

    //Game UI
    [SerializeField] private Image ammuBar;
    [SerializeField] private Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void UpdateAmmuBarHud(float magazineSize, float currentAmmu)
    {
        ammuBar.fillAmount = Mathf.InverseLerp(0,magazineSize,currentAmmu);
    }

    public void UpdateHealthBarHud(float maxHealthValue, float currentHealth)
    {
        healthBar.fillAmount = Mathf.InverseLerp(0, maxHealthValue, currentHealth);
    }

    public void DisplayConrtolsPanel(bool value)
    {
        controlsPanel.SetActive(value);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
