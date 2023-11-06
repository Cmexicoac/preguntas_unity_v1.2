using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour
{
    // Start is called before the first frame update
    public void MuteToggle(bool muted) 
    {
    if (muted)
        {
            AudioListener.volume = 1;
        }
    else
        {
            AudioListener.volume = 0;
        }
    }
}
