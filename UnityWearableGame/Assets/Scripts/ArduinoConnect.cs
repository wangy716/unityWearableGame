using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoConnect : MonoBehaviour
{
    public string serialPort;

    SerialPort dataStream;
    private string receivedString;

    public float handPos;

    public float finger1, finger2, finger3, finger4, finger5;

    //public bool plasticGes;
    //public bool heavyGes;
    //public bool magGes;
    //public bool bananaGes;
    //public bool gumGes;
    //public bool trapGes;

    private float holdTimePlastic;
    private float holdTimeHeavy;
    private float holdTimeMag;
    private float lockGes = 1f;


    void Start()
    {
        dataStream = new SerialPort(serialPort, 9600);
        dataStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (dataStream != null)
        {
            receivedString = dataStream.ReadLine();

            string[] data = receivedString.Split(";");

            if (data.Length == 6)
            {
                handPos = float.Parse(data[0]);

                finger1 = float.Parse(data[1]);
                finger2 = float.Parse(data[2]);
                finger3 = float.Parse(data[3]);
                finger4 = float.Parse(data[4]);
                finger5 = float.Parse(data[5]);
            }
            else
            {
                Debug.Log("Connection Problem");
            }

        }
        else
        {
            Debug.Log("No Data");
        }


        //CheckGesture();
    }

    //private void CheckGesture()
    //{
    //    // Plastic Ball
    //    if (finger1 < 600 && finger2 > 900 && finger3 > 900 && finger4 > 900 && finger5 > 900)
    //    {
    //        holdTimePlastic += Time.deltaTime;
    //        if(holdTimePlastic >= lockGes)
    //        {
    //            plasticGes = true;
    //        }
    //    } else
    //    {
    //        holdTimePlastic = 0;
    //    }

    //    // Heavy Ball
    //    if (finger1 < 600 && finger2 < 600 && finger3 > 900 && finger4 > 900 && finger5 > 900)
    //    {
    //        holdTimeHeavy += Time.deltaTime;
    //        if (holdTimeHeavy >= lockGes)
    //        {
    //            heavyGes = true;
    //        }
    //    }
    //    else
    //    {
    //        holdTimeHeavy = 0;
    //    }

    //    // Mag Ball
    //    if (finger1 < 600 && finger2 < 600 && finger3 > 900 && finger4 > 900 && finger5 < 600)
    //    {
    //        holdTimeMag += Time.deltaTime;
    //        if (holdTimeMag >= lockGes)
    //        {
    //            magGes = true;
    //        }
    //    }
    //    else
    //    {
    //        holdTimeMag = 0;
    //    }
    //}
}
