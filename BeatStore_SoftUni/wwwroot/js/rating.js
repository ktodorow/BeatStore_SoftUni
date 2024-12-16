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
        fetchUserRating(); 
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

                    Swal.fire({
                        title: "Thank You!",
                        text: data.message,
                        icon: "success",
                        timer: 2000,
                        showConfirmButton: false
                    });

                } else {
                    Swal.fire({
                        title: "Oops!",
                        text: data.message,
                        icon: "error",
                        timer: 1500,
                        showConfirmButton: false
                    });
                }
            })
            .catch(error => {
                console.error("Error submitting rating:", error);

                Swal.fire({
                    title: "Error!",
                    text: data.message,
                    icon: "error",
                    timer: 1500,
                    showConfirmButton: falsew
                });
            });
    }
});
