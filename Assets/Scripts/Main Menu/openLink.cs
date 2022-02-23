using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLink : MonoBehaviour
{
    public void OpenMotuLink() {

        Application.OpenURL("https://www.youtube.com/c/WowleraTV/");

    }
    public void OpenTargetLink(string link) {
        Debug.Log(link);
        Application.OpenURL(link);

    }

    public void OpenKlarLink() {

        Application.OpenURL("https://vk.com/loscops");

    }

    public void OpenGARMLink() {

        Application.OpenURL("https://patreon.com/loscops");

    }

    public void OpenWowleraLink() {

        Application.OpenURL("https://discord.gg/psmvujyA3m");

    }

    public void OpenSergeyLink() {

        Application.OpenURL("https://twitter.com/LosCopsTheGame");

    }

        public void OpenScyphLink() {

        Application.OpenURL("https://twitter.com/LosCopsTheGame");

    }
}