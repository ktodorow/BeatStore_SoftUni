document.addEventListener("DOMContentLoaded", function () {
    const updateProfileButton = document.getElementById("update-profile-button");

    updateProfileButton.addEventListener("click", function (event) {
        event.preventDefault();

        Swal.fire({
            title: "Are you sure?",
            text: "You are about to update your profile details.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, update it!",
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById("profile-form").submit();
            }
        });
    });
});
