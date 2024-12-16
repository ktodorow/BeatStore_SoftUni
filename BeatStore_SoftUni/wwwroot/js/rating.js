document.addEventListener("DOMContentLoaded", function () {
    const beatId = document.getElementById("beat-id").value;

    fetchAverageRating();
    fetchUserRating();

    function fetchAverageRating() {
        fetch(`/Rating/AverageRating?beatId=${beatId}`)
            .then(response => response.text())
            .then(html => {
                document.getElementById("average-rating").innerHTML = html;
            });
    }

    function fetchUserRating() {
        fetch(`/Rating/UserRating?beatId=${beatId}`)
            .then(response => response.text())
            .then(html => {
                document.getElementById("user-rating").innerHTML = html;
                setupStarHandlers();
            });
    }

    function setupStarHandlers() {
        const stars = document.querySelectorAll(".fa-star");
        stars.forEach((star, index) => {
            star.addEventListener("mouseover", () => highlightStars(index));
            star.addEventListener("click", () => submitRating(index + 1));
            star.addEventListener("mouseout", () => resetStars());
        });
    }

    function highlightStars(index) {
        const stars = document.querySelectorAll(".fa-star");
        stars.forEach((star, i) => {
            star.classList.toggle("checked", i <= index);
        });
    }

    function resetStars() {
        const userStars = document.querySelectorAll(".fa-star");
        userStars.forEach(star => star.classList.remove("checked"));
        fetchUserRating(); // Reset to the saved user rating
    }

    function submitRating(value) {
        fetch("/Rating/AddRating", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ beatId, value })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    fetchAverageRating();
                    fetchUserRating();
                } else {
                    alert(data.message);
                }
            });
    }
});
