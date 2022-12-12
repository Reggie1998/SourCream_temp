using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;


    private void Awake() 
    {
        GetComponentInParent<StatsComponent>().health.onHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float current, float max) 
    {   
        float precentage = current / max;
        Debug.Log("HANDLE HEALTH CHANGED");
        Debug.Log(current);
        Debug.Log(precentage);

        StartCoroutine(ChangeToPct(precentage));
    }
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChanePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds) 
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChanePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate() 
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
