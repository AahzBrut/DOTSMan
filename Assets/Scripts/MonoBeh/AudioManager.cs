using UnityEngine;

namespace MonoBeh
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public static AudioSource MusicSource;
        private const string SoundResourceFolder = "SFX";
        private const string MusicResourceFolder = "Music";
        
        public void Awake()
        {
            Instance = this;
        }

        public void PlaySfxRequest(string clipName)
        {
            var audioClip = Resources.Load<AudioClip>($"{SoundResourceFolder}/{clipName}");
            if (audioClip == null || Camera.main == null) return;
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        public void PlayMusicRequest(string clipName)
        {
            var audioClip = Resources.Load<AudioClip>($"{MusicResourceFolder}/{name}");
            if (audioClip == null || MusicSource.clip == audioClip) return;
            MusicSource.clip = audioClip;
            MusicSource.Play();
        }
    }
}
