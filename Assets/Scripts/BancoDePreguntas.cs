using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.NCalc;

public class BancoDePreguntas : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject GridPrefab;
    //private string filePath = "./Assets/BDP.csv";
    public List<string[]> data;

    // Start is called before the first frame update
    private void Start()
    {
        data = GameObject.Find("Quiz Manager").GetComponent<QuizManager>().data;

        spawnGrid();
    }

    public void WriteToCSV()
    {
        GameObject[] grids = GameObject.FindGameObjectsWithTag("Grid");
        List<string> data = new List<string>();

        for (int i = 0; i <  grids.Length; i++)
        {
            TMP_InputField[] input = grids[i].GetComponentsInChildren<TMP_InputField>();
            string[] text = new string[6];
            
            for (int j = 0; j < input.Length; j++)
            {
                text[j] = input[j].text;
            }
            data.Add(string.Join(";", text));
        }

        using (StreamWriter writer = new StreamWriter("./Assets/BDP.csv"))//File.AppendText(filePath))
        {
            string line = string.Join("\n", data);
            Debug.Log(line);
            writer.WriteLine(line);
        }
    }

    public void AddCell()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, (GameObject.FindGameObjectsWithTag("Grid").Length + 1) * 75);
        GameObject Grid = Instantiate(GridPrefab);
        RectTransform GridRT = Grid.GetComponent<RectTransform>();
        GridRT.SetParent(gameObject.GetComponent<RectTransform>(), false);

        GridRT.anchoredPosition = new Vector2(0, GridRT.anchoredPosition.y - (75 * (GameObject.FindGameObjectsWithTag("Grid").Length - 1)));
    }

    public void RemoveCell()
    {
        Destroy(GameObject.FindGameObjectsWithTag("Grid")[GameObject.FindGameObjectsWithTag("Grid").Length - 1]);
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, (GameObject.FindGameObjectsWithTag("Grid").Length) * 75);
    }

    private void spawnGrid()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, (data.Count) * 75);
        Debug.Log(data[0][0]);
        if (string.Join("", data[0]) != "")
        {
            for (int i = 0; i < data.Count; i++)
            {
                GameObject Grid = Instantiate(GridPrefab);

                RectTransform GridRT = Grid.GetComponent<RectTransform>();
                GridRT.SetParent(gameObject.GetComponent<RectTransform>(), false);

                GridRT.anchoredPosition = new Vector2(0, GridRT.anchoredPosition.y - (75 * i));

                if (i != data.Count)
                {
                    TMP_InputField[] text = Grid.GetComponentsInChildren<TMP_InputField>();
                    int index = 0;

                    foreach (TMP_InputField texts in text)
                    {
                        texts.text = data[i][index];
                        index++;
                    }
                }
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
   
    }
}
