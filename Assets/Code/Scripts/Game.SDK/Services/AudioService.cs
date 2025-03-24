using Game.SDK.Services.Interfaces;
using JSAM;
using UnityEngine;

namespace Game.SDK.Services
{
    /// <summary>
    /// Реализация интерфейса <see cref="IAudioService"/>
    /// </summary>
    public sealed class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField, Tooltip("Библиотека аудио")]
        private AudioLibrary library;

        public float MasterVolume
        {
            get => AudioManager.MasterVolume;
            set => AudioManager.MasterVolume = value;
        }

        public float SoundVolume
        {
            get => AudioManager.SoundVolume;
            set => AudioManager.SoundVolume = value;
        }

        public float MusicVolume
        {
            get => AudioManager.MusicVolume;
            set => AudioManager.MusicVolume = value;
        }

        public void Awake()
        {
            AudioManager.InternalInstance.LoadAudioLibrary(library);
        }

        public void PlaySound(string sound)
        {
            AudioManager.PlaySound(GetAudio<SoundFileObject>(sound));
        }

        public void StopSound(string sound, bool stopInstantly = true)
        {
            AudioManager.StopSound(GetAudio<SoundFileObject>(sound), stopInstantly: stopInstantly);
        }

        public void PlayMusic(string music)
        {
            AudioManager.PlayMusic(GetAudio<MusicFileObject>(music));
        }

        public void StopMusic(string music, bool stopInstantly = true)
        {
            AudioManager.StopMusic(GetAudio<MusicFileObject>(music), stopInstantly: stopInstantly);
        }

        private T GetAudio<T>(string audio) where T : BaseAudioFileObject
        {
            return AudioManager.InternalInstance.AudioFileFromString(audio) as T;
        }
    }
}