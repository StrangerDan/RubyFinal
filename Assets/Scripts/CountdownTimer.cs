using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
 public int countdownTime;
 public Text countdownDisplay;

    IEnumerator CountdownToLose()
    {
        while(countdownTime > 0){
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "Game Over! You're Score is:" + 5;
        yield return new WaitForSeconds(1f);
        

    }
    
}
