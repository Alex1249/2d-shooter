
using UnityEngine; 

using System.Collections;

 

[RequireComponent (typeof (AudioSource))]

 

public class SingleMicrophoneCapture : MonoBehaviour 

{

    private bool micConnected = true;

    

    private int minFreq;

    private int maxFreq;

    

    private AudioSource goAudioSource;

    

    void Start() 

    {

        if(Microphone.devices.Length <= 0)

        {

            //Do Nothing Because Microphone is not connected

        }

        else

        {

            micConnected = true;

            

            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            

            if(minFreq == 0 && maxFreq == 0)

            {

                maxFreq = 44100;

            }

            

            goAudioSource = this.GetComponent<AudioSource>();

        }

    }

    

    void OnGUI() 

    {

        if(micConnected)

        {

            if(!Microphone.IsRecording(null))

            {

                if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50), "Record"))

                {

                    goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);

                }

            }

            else

            {

                if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-75, 200, 50), "Stop && Play!"))

                {

                    Microphone.End(null); 

                    goAudioSource.Play(); 

                }

            }

        }

    }

}
