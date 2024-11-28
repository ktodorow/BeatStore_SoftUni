document.addEventListener("DOMContentLoaded", () => {
    const mediaPlayer = document.getElementById("media-player");
    const audioPlayer = document.getElementById("audio-player");
    const audioSource = document.getElementById("audio-source");
    const currentTrackTitle = document.getElementById("current-track-title");

    // Attach click event to each library card
    document.querySelectorAll(".library-card").forEach(card => {
        const playButton = card.querySelector(".play-button");
        const audioUrl = card.dataset.audioUrl;
        const title = card.dataset.title;

        playButton.addEventListener("click", () => {
            console.log(`Playing track: ${title}`); // Debug log

            // Set audio source and track title
            audioSource.src = audioUrl;
            currentTrackTitle.textContent = title;

            // Load and play audio
            audioPlayer.load();
            audioPlayer.play()
                .then(() => {
                    console.log("Audio is playing.");
                })
                .catch(error => {
                    console.error("Error playing audio:", error);
                });

            // Show media player
            mediaPlayer.classList.remove("hidden");
        });
    });

    // Hide media player when audio ends
    audioPlayer.addEventListener("ended", () => {
        console.log("Audio ended."); // Debug log
        mediaPlayer.classList.add("hidden");
    });
});
