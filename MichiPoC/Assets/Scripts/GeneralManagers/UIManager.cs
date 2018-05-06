using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameOver;
    public Text GameOverReasonText;
    public GameObject GameWon;
    public Text GameWonScoreText;

    public RawImage FanBase;
    public RawImage RateToBeFired;

    private void Awake()
    {
        EventSystem.Instance.ShowGameWonScreenEvent += ShowGameWonScreen;
        EventSystem.Instance.ShowGameOverScreenEvent += ShowGameOverScreen;
        EventSystem.Instance.UpdateScareOfFanBaseUIEvent += UpdateFanBaseUI;
        EventSystem.Instance.UpdateGetCloserToBeUIFiredEvent += UpdateFireRateUI;
        EventSystem.Instance.SetScareOfFanBaseUIEvent += SetFanBaseUI;
        EventSystem.Instance.SetGetCloserToBeUIFiredEvent += SetFireRateUI;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.ShowGameWonScreenEvent -= ShowGameWonScreen;
        EventSystem.Instance.ShowGameOverScreenEvent -= ShowGameOverScreen;
        EventSystem.Instance.UpdateScareOfFanBaseUIEvent += UpdateFanBaseUI;
        EventSystem.Instance.UpdateGetCloserToBeUIFiredEvent += UpdateFireRateUI;
        EventSystem.Instance.SetScareOfFanBaseUIEvent += SetFanBaseUI;
        EventSystem.Instance.SetGetCloserToBeUIFiredEvent += SetFireRateUI;
    }

    private void ShowGameOverScreen()
    {
        GameOver.gameObject.SetActive(true);
    }

    private void ShowGameWonScreen()
    {
        GameWon.gameObject.SetActive(true);
    }

    private void UpdateFanBaseUI(float rate, bool wasPositive)
    {
        UpdateRate(FanBase.material, rate, wasPositive);
    }
    
    private void UpdateFireRateUI(float rate, bool wasPositive)
    {
        UpdateRate(RateToBeFired.material, rate, wasPositive);
    }

    private void UpdateRate(Material shaderMat, float rate, bool wasPositive)
    {
        if(wasPositive)
        {
            float currentTransitionState = shaderMat.GetFloat("_TransitionState");
            float nextTransitState = Mathf.Lerp(currentTransitionState, rate, Time.deltaTime);

            shaderMat.SetFloat("_FillState", rate);
            shaderMat.SetFloat("_TransitionState", nextTransitState);
            shaderMat.SetFloat("_TransitPositive", 1.0f);
        }
        else
        {
            float currentRate = shaderMat.GetFloat("_FillState");
            float nextFillState = Mathf.Lerp(currentRate, rate, Time.deltaTime);

            shaderMat.SetFloat("_FillState", nextFillState);
            shaderMat.SetFloat("_TransitionState", rate);
            shaderMat.SetFloat("_TransitPositive", 0.0f);
        }
    }

    private void SetFanBaseUI(float rate)
    {
        FanBase.material.SetFloat("_FillState", rate);
    }

    private void SetFireRateUI(float rate)
    {
        RateToBeFired.material.SetFloat("_FillState", rate);
    }
}
