using UnityEngine;

public class UI : MonoBehaviour
{
    int score = 0;

    public TMPro.TextMeshProUGUI Scoretext;

    public GameObject MenuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scoretext.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        score += 1;
        Scoretext.text = $"{score}";
    }

    void OnMenu()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    } 
}
