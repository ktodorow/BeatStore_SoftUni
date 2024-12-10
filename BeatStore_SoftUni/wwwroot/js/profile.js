document.addEventListener("DOMContentLoaded", function () {
    const updateProfileButton = document.getElementById("update-profile-button");
    const removeProfileButton = document.getElementById("action");
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

    removeProfileButton.addEventListener("click", function (event) {
        event.preventDefault();

        Swal.fire({
            title: "Are you sure?",
            text: "This will remove your current profile picture and reset it to the default avatar.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, remove it!",
        }).then((result) => {
            if (result.isConfirmed) {
                const actionInput = document.createElement("input");
                actionInput.type = "hidden";
                actionInput.name = "action";
                actionInput.value = "remove";
                document.getElementById("profile-form").appendChild(actionInput);

                document.getElementById("profile-form").submit();
            }
        });
    });
});
