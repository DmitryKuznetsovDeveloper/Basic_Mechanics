using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text timerLabel;
    [SerializeField] private TMP_Text lapsLabel;
    [SerializeField] private TMP_Text lapsTimeLabel;
    [SerializeField] private TMP_Text lastLapsTimeLabel;
    private float _currentLapsTime;
    private float _lastLapsTime;
    private float _currentTime;
    private int _lapsNumber;

    void Update()
    {
        _currentTime = Mathf.Round(Time.time);
        timerLabel.text = _currentTime.ToString();
    }

    public void LapsFinishButtonClick()
    {
        CalculateRaceData();
        DisplayRaceDate();
    }

    private void CalculateRaceData()
    {
        _lastLapsTime = _currentLapsTime;
        _currentLapsTime = _currentTime;
        _lapsNumber += 1;
    }

    private void DisplayRaceDate()
    {
        lapsTimeLabel.text = _currentLapsTime.ToString();
        lapsLabel.text = _lapsNumber.ToString();
        lastLapsTimeLabel.text = _lastLapsTime.ToString();
    }
}
