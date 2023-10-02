using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respuestas : MonoBehaviour
{
    public bool esCorrecta = false;
    public QuizManager QuizManager;
    public void Respuesta()
    {
        if (esCorrecta)
        {
            Debug.Log("Respuesta correcta");
            QuizManager.correcto();
        }

        else
        {
            Debug.Log("Respuesta Incorrecta");
            QuizManager.incorrecto();
        }
    }
}
