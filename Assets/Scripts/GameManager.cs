using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public UMP.UniversalMediaPlayer ump;
    int videoIndex = 0;
    List<string> videos;

    //public TextMesh debugText;

    // Start is called before the first frame update
    void Start()
    {
        videos = new List<string>();
        StartCoroutine(GetRequest("https://snapper.indie.hosting/Abby/list.php"));


        /*  DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
          FileInfo[] info = dir.GetFiles("*.*");

          foreach(FileInfo file in info)
          {
              Debug.Log(file.Name);
              if (file.Extension != "meta")
              {
                  videos.Add("file:///" + file.Name);
              }
          }
          for (int i = 1; i < 4; i++)
          {

          }*/

        //AssetBundle.LoadFromFile("sdcard/Android/obb/com.SmartSense.EyePatcher/EyePatcher.main.obb");
        // URL: https://snapper.indie.hosting/Abby/list.php


        //videos.Add("https://snapper.indie.hosting/Abby/Pui%20Pui%20Molcar1/stream.mp4");
        /*videos.Add("https://snapper.indie.hosting/Pui Pui Molcar1/stream.mp4");
        videos.Add("http://snapper.indie.hosting/Pui Pui Molcar1/stream.m3u8");

        videos.Add("file:///MI SCAPPA LA PIPI.mkv");
        videos.Add("file:///Piccolo Coro.mkv");
        videos.Add("file:///coccodrillo.webm");
        videos.Add("file:///IL BALLO.mp4");
        //videos.Add("file:///PP1.mkv");


        ump.Path = videos[videoIndex];*/
        //Debug.Log("UMP Path: " + ump.Path);

        //debugText.text = "Loading PP1";

        //Invoke("StartVideo", 2);

        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    LoadVideos(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    void LoadVideos(string videoArray)
    {
        string[] videoList = videoArray.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
        foreach(string video in videoList)
        {
            Debug.Log(video);

            videos.Add("https://snapper.indie.hosting/Abby/" + video.Replace(" ", "%20"));
        }
        ump.Path = videos[videoIndex];
        Debug.Log("UMP Path: " + ump.Path);

        //debugText.text = "Loading Video 1";

        Invoke("StartVideo", 0);
    }

    public void StartVideo()
    {
        //debugText.text = "Starting Video";
        Debug.Log("Starting video");
        ump.Play();
    }

    public void LoadNextVideo(int delay)
    {
        videoIndex++;
        videoIndex %= videos.Count;
        ump.Path = videos[videoIndex];
        //ump.Play();
        Invoke("StartVideo", delay);
    }
    public void LoadPreviousVideo(int delay)
    {
        videoIndex--;
        if (videoIndex < 0)
        videoIndex = videos.Count - 1; // Max
        ump.Path = videos[videoIndex];
        //ump.Play();
        Invoke("StartVideo", delay);
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.RightArrow)) // A
        {
            LoadNextVideo(0);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.LeftArrow)) // B
        {
            LoadPreviousVideo(0);
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // Trigger
        {
            float current = ump.Position * ump.Length;
            current += 60 * 2000; // 2 Minutes
            ump.Position = current / ump.Length;
            if (ump.Position >= 1.0)
                ump.Position = ump.Length - (60 * 1000);

        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)) // Middle Finger
        {
            float current = ump.Position * ump.Length;
            current -= 60 * 2000; // 2 Minutes
            ump.Position = current / ump.Length;
            if (ump.Position <= 0.0)
                ump.Position = 0;

        }

        Vector2 VideoNavigation = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //Debug.Log(VideoNavigation);
        // 0.5f - 5f
        float speedMultiplier = VideoNavigation.x * 4;
        if (VideoNavigation.x < 0)
            speedMultiplier = VideoNavigation.x * 0.5f;

        ump.PlayRate = 1.0f + speedMultiplier;
        //Debug.Log(ump.PlayRate);
    }

    public void LogErrors(String error)
    {
        Debug.Log(error);
        //debugText.text = error;
    }

    //float storedPos;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //storedPos = ump.Position;
            // pausing
            //debugText.text = "Pausing";
            ump.Pause();
        }
        else
        {
            // Resuming
            //if (ump.Path != "")
            //ump.Position = storedPos;
            //ump.Play();
            //debugText.text = "Delay";
            Invoke("StartVideo", 2);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        //if (focus)
        //{
        //    // Resuming
        //    ump.Play();
        //}
        //else
        //{
        //    ump.Stop();
        //}
    }
}
