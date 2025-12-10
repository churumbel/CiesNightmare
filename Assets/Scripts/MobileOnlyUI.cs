using UnityEngine;

public class MobileOnlyUI : MonoBehaviour
{
    void Start()
    {
        bool isMobile =
            Application.isMobilePlatform ||
            Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer;

        gameObject.SetActive(isMobile);
    }
}
