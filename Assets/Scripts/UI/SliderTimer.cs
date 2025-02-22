using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderTimer : MonoBehaviour
{
    GameManager manager;
    public Button reviveBtn;

    Slider slTimer;

    public float fSliderTime= 1000.0f;
    public AudioSource timerAudioSource;
    public AudioClip[] timerAudioList;
    private float volume = 0.5f;
    private bool isTimeOut = false;
    private bool activeSound = false;
    /* 
        0 = timer
        1= timeOver
    */
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slTimer = GetComponent<Slider>();
        slTimer.maxValue = fSliderTime;
        slTimer.value = fSliderTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if(manager.isClear || manager.isOver) //게임 종료 시 타이머 오디오 중단
        //{
        //    TimerSoundPlay
        //}
        //else
        //{
        //    slTimer.value -= Time.deltaTime;

        //    if (slTimer.value <= 0.0f)
        //    {
        //        if (isTimeOut == false)
        //        {
        //            isTimeOut = true;
        //            TimerSoundPlay("TimeOut");
        //        }

        //        manager.GameOver();
        //        reviveBtn.interactable = false;
        //    }
        //    else if (slTimer.value < 8.0f)
        //    {
        //        if (activeSound == false)
        //        {
        //            TimerSoundPlay("Timer");
        //            activeSound = true;
        //        }
        //    }
        //}
        if (slTimer.value > 0.0f)
        {
            if (manager.isClear || manager.isOver) //게임 오버, 클리어 시 타이머 멈춤
                timerAudioSource.Pause();
            else
            {
                slTimer.value -= Time.deltaTime;

                if (slTimer.value < 8.0f)
                {
                    if (activeSound == false)
                    {
                        TimerSoundPlay("Timer");
                        activeSound = true;
                    }
                }
            }

            //아래 코드 용도가 승리/ 패배 조건에서 사운드 안 나게 하는거면 지워주세요
            //if (Time.timeScale != 1.0f)
            //{

            //    timerAudioSource.Pause();
            //}
            //else
            //{
            //    if (!timerAudioSource.isPlaying)
            //        timerAudioSource.Play();
            //}

        }
        else
        {
            if (isTimeOut == false)
            {
                isTimeOut = true;
                TimerSoundPlay("TimeOut");
            }
            manager.GameOver();
            reviveBtn.interactable = false;
        }


    }

   public void TimerSoundPlay(string name)
   {
        if (!MuteManager.EffectIsMuted)
        {
            if(name == "Timer")
            {
                timerAudioSource.clip = timerAudioList[0];
  
            }
            else if(name == "TimeOut")
            {
                timerAudioSource.clip = timerAudioList[1];
             
            }

            timerAudioSource.loop = false;
            timerAudioSource.volume = volume;
            timerAudioSource.Play();
        }

   }
}
