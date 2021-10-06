using MyGame;
using System.Collections;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private IEnumerator Start()
    {
        // if (!AdsManager.HaveSetupConsent)
        // {
        // 	SharedUIManager.ConsentPanel.Show();
        yield return new WaitForSeconds(1f);
        // }
        GameManager.LoadScene("MainMenu");
    }
}