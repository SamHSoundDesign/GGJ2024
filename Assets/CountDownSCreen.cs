using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownSCreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private float defaultFontSize;
    private int counter = 5;
    private void OnEnable()
    {
        
        counter = 5;
        tmp.text = counter.ToString();
        StartCoroutine(OneSec(1));
    }

    private void Start()
    {
        defaultFontSize = tmp.fontSize;
    }

    public IEnumerator OneSec(float delay)
    {
        yield return new WaitForSeconds(delay);
        counter--;

        if(counter > 3)
        {
            tmp.fontSize = defaultFontSize;
            tmp.text = counter.ToString();
            StartCoroutine(OneSec(delay));
        }
        else if(counter <= 3 && counter >= 0)
        {
            if (counter == 0)
            {
                tmp.fontSize = defaultFontSize /3;
                tmp.text = "OTTER";
                StartCoroutine(OneSec(0.5f));
            }
            else
            {
                tmp.fontSize = defaultFontSize;
                tmp.text = counter.ToString();
                StartCoroutine(OneSec(0.5f));
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
