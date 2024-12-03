document.addEventListener("DOMContentLoaded", function () {
    const mediaPlayer = document.getElementById("media-player");
    const trackCover = document.getElementById("track-cover");
    const trackTitle = document.getElementById("track-title");
    const trackArtist = document.getElementById("track-artist");
    const playPauseButton = document.getElementById("play-pause-button");
    const audioPlayer = new Audio();
    const progressBar = document.getElementById("progress-bar");
    const currentTimeDisplay = document.getElementById("current-time");
    const trackDurationDisplay = document.getElementById("track-duration");
    const volumeSlider = document.getElementById("volume-slider");

    // Handle play/pause
    playPauseButton.addEventListener("click", function () {
        if (audioPlayer.paused) {
            audioPlayer.play();
            playPauseButton.innerHTML = '<i class="fa fa-pause"></i>';
        } else {
            audioPlayer.pause();
            playPauseButton.innerHTML = '<i class="fa fa-play"></i>';
        }
    });

    // Update progress bar and time
    audioPlayer.addEventListener("timeupdate", function () {
        progressBar.value = (audioPlayer.currentTime / audioPlayer.duration) * 100;
        currentTimeDisplay.textContent = formatTime(audioPlayer.currentTime);
        trackDurationDisplay.textContent = formatTime(audioPlayer.duration);
    });

    progressBar.addEventListener("input", function () {
        audioPlayer.currentTime = (progressBar.value / 100) * audioPlayer.duration;
    });

    // Volume control
    volumeSlider.addEventListener("input", function () {
        audioPlayer.volume = volumeSlider.value;
    });

    // Function to load a track
    function loadTrack(url, title, cover, artist) {
        audioPlayer.src = url;
        audioPlayer.load();
        audioPlayer.play();

        // Update UI
        trackCover.src = cover;
        trackTitle.textContent = title;
        trackArtist.textContent = artist; // Set artist dynamically
        mediaPlayer.classList.remove("hidden");
        playPauseButton.innerHTML = '<i class="fa fa-pause"></i>';
    }

    // Attach event to play button on each beat card
    document.querySelectorAll(".library-card").forEach(card => {
        const playButton = card.querySelector(".play-button");
        const audioUrl = card.dataset.audioUrl;
        const title = card.dataset.title;
        const cover = card.querySelector("img").src;
        const artist = card.dataset.artist; // Fetch artist from data attribute

        playButton.addEventListener("click", function () {
            loadTrack(audioUrl, title, cover, artist);
        });
    });

    // Utility function to format time in mm:ss
    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const secs = Math.floor(seconds % 60);
        return `${minutes}:${secs < 10 ? "0" + secs : secs}`;
    }
});
