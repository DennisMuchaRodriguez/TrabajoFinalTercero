using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextoLoreController : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public Button nextButton;
    public Button previousButton;
    public Button closeButton;
    public Button openMenuButton;
    public GameObject menuPanel;

    private DoubleList<TextMeshProUGUI> textList;
    private int currentPosition;

    void Start()
    {
        textList = new DoubleList<TextMeshProUGUI>();

       
        textList.InsertNodeAtEnd(text1);
        textList.InsertNodeAtEnd(text2);
        textList.InsertNodeAtEnd(text3);

        currentPosition = 0;

       
        UpdateTextDisplay();

       
        nextButton.onClick.AddListener(NextText);
        previousButton.onClick.AddListener(PreviousText);
        closeButton.onClick.AddListener(CloseMenu);
        openMenuButton.onClick.AddListener(OpenMenu);

      
        menuPanel.SetActive(false);
    }

    private void NextText()
    {
        currentPosition++;
        UpdateTextDisplay();
    }

    private void PreviousText()
    {
        currentPosition--;
        UpdateTextDisplay();
    }

    private void UpdateTextDisplay()
    {
        
        for (int i = 0; i < textList.Count; i++)
        {
            var text = textList.GetNodeAtPosition(i);
            text.gameObject.SetActive(i == currentPosition);
        }

    
        nextButton.gameObject.SetActive(currentPosition < textList.Count - 1);
        previousButton.gameObject.SetActive(currentPosition > 0);
    }

    private void OpenMenu()
    {
        menuPanel.SetActive(true);
        UpdateTextDisplay();
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

}
