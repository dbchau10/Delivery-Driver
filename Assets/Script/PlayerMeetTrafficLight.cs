using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeetTrafficLight : MonoBehaviour
{
   
   public GameObject TrafficLight;
   public GameObject WarningTimer;

   public GameObject Snackbar;
   public bool checkOpenQuiz=false;

   AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    //    public bool checkSkip = false;
    void OnTriggerStay2D(Collider2D other)
   {
        if (TrafficLight.GetComponent<ChangeTrafficLightState>().currentLightState == "Red" && !checkOpenQuiz ){
            if (other.tag == "HeadCar"){
                FindObjectOfType<QuizManager>().QuizOpen();
                checkOpenQuiz = true;
            }
        }
       
   }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HeadCar")
        {
            WarningTimer.GetComponent<WarningTimer>().StopWarning();
            var snackbar = Snackbar.GetComponent<MoveModal>();
            snackbar.gameObject.SetActive(false);
        }
    }
  
   void OnTriggerExit2D(Collider2D other)
   {
        if (TrafficLight.GetComponent<ChangeTrafficLightState>().currentLightState == "Red" ){
            if (other.tag == "HeadCar"){
                audioManager.PlaySFX(audioManager.alert);
                WarningTimer.GetComponent<WarningTimer>().Warning();
                var snackbar = Snackbar.GetComponent<MoveModal>();
                snackbar.content = "Bạn đang vượt đèn đỏ!";
                snackbar.gameObject.SetActive(true);
            }
        }
    }
}
