using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    bool areOptionsVisible = false;

    Animator exitButtonAnimator;
    Animator optionsButtonAnimator;
    Animator startButtonAnimator;

    // Start is called before the first frame update
    void Start()
    {
        exitButtonAnimator = GameObject.Find("Exit").GetComponent<Animator>();
        optionsButtonAnimator = GameObject.Find("Options").GetComponent<Animator>();
        startButtonAnimator = GameObject.Find("Start").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTitleClicked()
    {
        if (areOptionsVisible)
        {
            exitButtonAnimator.Play("Exit_Button_Close");
            optionsButtonAnimator.Play("Options_Button_Close");
            startButtonAnimator.Play("Start_Button_Close");

            StartCoroutine(Utils.ExecuteAfterTime(1f, () =>
            {
                exitButtonAnimator.gameObject.SetActive(false);
                optionsButtonAnimator.gameObject.SetActive(false);
                startButtonAnimator.gameObject.SetActive(false);
            }));

            areOptionsVisible = false;
        }
        else
        {
            exitButtonAnimator.gameObject.SetActive(true);
            optionsButtonAnimator.gameObject.SetActive(true);
            startButtonAnimator.gameObject.SetActive(true);

            exitButtonAnimator.Play("Exit_Button_Open");
            optionsButtonAnimator.Play("Options_Button_Open");
            startButtonAnimator.Play("Start_Button_Open");

            areOptionsVisible = true;
        }
    }
}
