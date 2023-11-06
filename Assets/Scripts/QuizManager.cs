using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.ComponentModel;
using Unity.Burst.Intrinsics;
using JetBrains.Annotations;

public class QuizManager : MonoBehaviour
{
    public string Pregunta;
    public string[] Respuestas;
    public int RespuestaCorrecta;


    public List<PreguntasRespuestas> PnR;
    public GameObject[] opciones;
    private int PreguntaActual = 0;

    public Text TextoPreguntas;
    public Text tip;
    public GameObject Quiz;
    public GameObject Final;
    public GameObject GameOver;
    public GameObject Inicio;
    public GameObject BotonSkip;
    public GameObject BotonPista;
    public GameObject PistaEscrita;
    public GameObject BancoDePreguntas;
    public Slider slider;

    public Slider num_preguntas;
    string textPath = "./Assets/npreguntas.txt";

    public GameObject Vida1;
    public GameObject Vida2;

    int puntuacion;
    int vidas;

    private string filePath = "./Assets/BDP.csv";
    public List<string[]> data = new List<string[]>();
    private string[][] preguntas;

    public Text score;
   
    private void Start()
    {
        Inicio.SetActive(true);
        Quiz.SetActive(false);
        Final.SetActive(false);
        GameOver.SetActive(false);


        // Reading file's data
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                // Split row using ";" as token
                string[] fields = line.Split(';');

                // Add row to data as an array of three elements
                data.Add(fields);
            }
        }

        preguntas = data.ToArray();

        num_preguntas.maxValue = preguntas.Length - 4;
        num_preguntas.minValue = 4;

        

        num_preguntas.value = int.Parse(File.ReadAllText(textPath));

        slider.maxValue = num_preguntas.value;

    MezclarMatriz(preguntas);
    }

    public void BotonInicio()
    {
        puntuacion = 0;
        slider.value = puntuacion;
        vidas = 3;
        Final.SetActive(false);
        GameOver.SetActive(false);
        Inicio.SetActive(false);
        Quiz.SetActive(true);
        generarPregunta();
        BotonSkip.SetActive(true);
        BotonPista.SetActive(true);
    }

    public void NuevoJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ganaste()
    {
       
        Quiz.SetActive(false);
        if (puntuacion == num_preguntas.value)
        {
            Final.SetActive(true);
            GameObject.Find("Score").GetComponent<Text>().text = "Score: " + puntuacion.ToString();
        }
        else if (vidas == 0)
        {
            GameOver.SetActive(true);
            GameObject.Find("Score").GetComponent<Text>().text = "Score: " + puntuacion.ToString();
        }



    }

    public void correcto()
    {
        PreguntaActual++;
        puntuacion = puntuacion + 1;
        slider.value = puntuacion;
        generarPregunta();
        
    }

    public void incorrecto()
    {
        PreguntaActual++;
        vidas = vidas - 1;
        if (vidas == 3)
        {
            Vida1.SetActive(true);
            Vida2.SetActive(true);
        }
        else if (vidas == 2)
        {
            Vida1.SetActive(false);
            Vida2.SetActive(true);
        }

        else if (vidas == 1)
        {
            Vida1.SetActive(false);
            Vida2.SetActive(false);
        }
        //Debug.Log(vidas);
        generarPregunta();

    }
    

    public void skip()
    {
        PreguntaActual++;
        puntuacion = puntuacion + 1;
        slider.value = puntuacion;
        generarPregunta();
        BotonSkip.SetActive(false);
    }

    public void pista()
    {
        BotonPista.SetActive(false);
        PistaEscrita.SetActive(true);
        
    }

    void definirRespuestas(int indiceDePregunta)
    {
        string[] respuestas = new string[4] { preguntas[indiceDePregunta][1], preguntas[indiceDePregunta][2], preguntas[indiceDePregunta][3], preguntas[indiceDePregunta][4] };
        Mezclar(respuestas);
        for (int i = 0; i < 4; i++)
        {
            opciones[i].GetComponent<Respuestas>().esCorrecta = respuestas[i] == preguntas[indiceDePregunta][1] ? true : false;
            opciones[i].transform.GetChild(0).GetComponent<Text>().text = respuestas[i];

        }
    }

    static void MezclarMatriz(string[][] matriz)
    {
        for (int i = 0; i < matriz.Length; i++)
        {
            int randomIndex = Random.Range(0, matriz.Length);
            string[] tempString = matriz[i];
            matriz[i] = matriz[randomIndex];
            matriz[randomIndex] = tempString;
        }
    }

    void Mezclar(string[] arr)
    {
        for (int i = 0; i <  arr.Length; i++)
        {
            int randomIndex = Random.Range(0, arr.Length);
            string tempString = arr[i];
            arr[i] = arr[randomIndex];
            arr[randomIndex] = tempString;
        }
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i]);
        }
    }

    void generarPregunta()
    {
        PistaEscrita.SetActive(false);

        if (vidas == 0 || puntuacion == num_preguntas.value)
        {
            Debug.Log("Fin del juego");
            ganaste();
        }

        else
        {
            TextoPreguntas.text = preguntas[PreguntaActual][0];

            tip.text = preguntas[PreguntaActual][5];

            definirRespuestas(PreguntaActual);
        }

    }

    public void b_editarpreguntas()
    {
        Inicio.SetActive(false);
        BancoDePreguntas.SetActive(true);
    }
}
