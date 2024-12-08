document.addEventListener("DOMContentLoaded", function () {
    const mediaPlayer = document.getElementById("media-player");
    const trackCover = document.getElementById("track-cover");
    const trackCoverContainer = trackCover.parentElement;
    const trackTitle = document.getElementById("track-title");
    const trackArtist = document.getElementById("track-artist");
    const playPauseButton = document.getElementById("play-pause-button");
    const audioPlayer = new Audio();
    const progressBar = document.getElementById("progress-bar");
    const currentTimeDisplay = document.getElementById("current-time");
    const trackDurationDisplay = document.getElementById("track-duration");
    const volumeSlider = document.getElementById("volume-slider");

    let currentTrack = sessionStorage.getItem("currentTrack") || null;
    let currentTime = sessionStorage.getItem("currentTime") || 0;
    let savedVolume = localStorage.getItem("playerVolume") || 1;

    audioPlayer.volume = parseFloat(savedVolume);
    volumeSlider.value = savedVolume;

    if (currentTrack) {
        loadTrack(JSON.parse(currentTrack));
        audioPlayer.currentTime = parseFloat(currentTime);
    } else {
        hideTrackCover();
    }

    playPauseButton.addEventListener("click", function () {
        if (audioPlayer.paused) {
            audioPlayer.play();
            playPauseButton.innerHTML = '<i class="fa fa-pause"></i>';
        } else {
            audioPlayer.pause();
            playPauseButton.innerHTML = '<i class="fa fa-play"></i>';
        }
    });

    audioPlayer.addEventListener("timeupdate", function () {
        progressBar.value = (audioPlayer.currentTime / audioPlayer.duration) * 100;
        currentTimeDisplay.textContent = formatTime(audioPlayer.currentTime);
        trackDurationDisplay.textContent = formatTime(audioPlayer.duration);

        sessionStorage.setItem("currentTime", audioPlayer.currentTime);
    });

    progressBar.addEventListener("input", function () {
        audioPlayer.currentTime = (progressBar.value / 100) * audioPlayer.duration;
    });

    volumeSlider.addEventListener("input", function () {
        const volume = volumeSlider.value;
        audioPlayer.volume = volume;

        localStorage.setItem("playerVolume", volume);
    });

    function loadTrack(track) {
        audioPlayer.src = track.audioUrl;
        audioPlayer.load();
        audioPlayer.play();

        trackCover.src = track.cover;
        trackCoverContainer.href = track.detailsUrl;
        trackCoverContainer.style.display = "block";
        trackTitle.textContent = track.title;
        trackArtist.textContent = track.artist;
        mediaPlayer.classList.remove("hidden");
        playPauseButton.innerHTML = '<i class="fa fa-pause"></i>';

        sessionStorage.setItem("currentTrack", JSON.stringify(track));
    }

    function hideTrackCover() {
        trackCoverContainer.style.display = "none";
        trackTitle.textContent = "No track playing";
        trackArtist.textContent = "Unknown Artist";
    }

    document.body.addEventListener("click", function (event) {
        const button = event.target.closest(".play-button");
        if (button) {
            const card = button.closest(".library-card");
            const track = {
                audioUrl: card.dataset.audioUrl,
                title: card.dataset.title,
                cover: card.dataset.cover,
                artist: card.dataset.artist,
                detailsUrl: card.dataset.detailsUrl,
            };
            loadTrack(track);
        }
    });
    function formatTime(seconds) {
        const minutes = Math.floor(seconds / 60);
        const secs = Math.floor(seconds % 60);
        return `${minutes}:${secs < 10 ? "0" + secs : secs}`;
    }
    window.stopPlayer = function () {
        audioPlayer.pause();
        audioPlayer.currentTime = 0;
        sessionStorage.clear();
        hideTrackCover();
    };
});
