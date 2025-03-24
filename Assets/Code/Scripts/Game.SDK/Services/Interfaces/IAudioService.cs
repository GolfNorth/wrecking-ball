namespace Game.SDK.Services.Interfaces
{
    /// <summary>
    /// Interface for managing audio in the game.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Gets or sets the master volume.
        /// </summary>
        float MasterVolume { get; set; }

        /// <summary>
        /// Gets or sets the sound volume.
        /// </summary>
        float SoundVolume { get; set; }

        /// <summary>
        /// Gets or sets the music volume.
        /// </summary>
        float MusicVolume { get; set; }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="sound">The sound effect to play.</param>
        void PlaySound(string sound);

        /// <summary>
        /// Stops a sound effect.
        /// </summary>
        /// <param name="sound">The sound effect to stop.</param>
        /// <param name="stopInstantly">Whether to stop the sound effect instantly.</param>
        void StopSound(string sound, bool stopInstantly = true);

        /// <summary>
        /// Plays a music track.
        /// </summary>
        /// <param name="music">The music track to play.</param>
        void PlayMusic(string music);

        /// <summary>
        /// Stops a music track.
        /// </summary>
        /// <param name="music">The music track to stop.</param>
        /// <param name="stopInstantly">Whether to stop the music track instantly.</param>
        void StopMusic(string music, bool stopInstantly = true);
    }
}