using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OnlineVideo : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public string videoUrl = "https://www.youtube.com/watch?v=41Pa82bjHR0";

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = videoUrl;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {

    }
}