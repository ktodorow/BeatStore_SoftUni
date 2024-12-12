document.addEventListener("DOMContentLoaded", function () {
    
    const beatIdElement = document.getElementById("beat-id");
    if (!beatIdElement) {
        console.error("beat-id element not found in the DOM.");
        return;
    }

    const beatId = beatIdElement.value;
    console.log("Beat ID:", beatId);

    loadComments();

    const addCommentForm = document.getElementById("add-comment-form");
    if (addCommentForm) {
        addCommentForm.addEventListener("submit", function (event) {
            event.preventDefault();

            console.log("Add Comment button clicked"); // Debug log

            const content = document.getElementById("comment-content").value.trim();
            if (!content) {
                Swal.fire("Error", "Comment content cannot be empty.", "error");
                return;
            }

            // Fetch the antiforgery token from the form
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            fetch(`/Comments/AddComment`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "RequestVerificationToken": token, // Include the antiforgery token
                },
                body: JSON.stringify({ BeatId: beatId, Content: content }),
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data.success) {
                        Swal.fire("Success", "Comment added successfully.", "success");
                        loadComments();
                        document.getElementById("comment-content").value = "";
                    } else {
                        Swal.fire("Error", data.message, "error");
                    }
                })
                .catch((error) => {
                    console.error("Error while adding comment:", error);
                    Swal.fire("Error", "An unexpected error occurred.", "error");
                });
        });
    }

    function loadComments() {
        fetch(`/Comments/GetComments?beatId=${beatId}`)
            .then((response) => response.json())
            .then((comments) => {
                const commentsList = document.getElementById("comments-list");
                commentsList.innerHTML = "";

                comments.forEach((comment) => {
                    const commentElement = document.createElement("div");
                    commentElement.classList.add("comment");

                    commentElement.innerHTML = `
                        <div class="comment-header">
                            <img src="${comment.profilePicture || "/images/avatar.jpg"}" alt="Profile Picture" class="rounded-circle" style="width: 50px; height: 50px; object-fit: cover;" />
                            <strong>${comment.username}</strong>
                            <small>${new Date(comment.datePosted).toLocaleString()}</small>
                            ${
                                comment.editedOn
                                    ? `<small class="text-muted">(Edited)</small>`
                                    : ""
                            }
                        </div>
                        <div class="comment-body">
                            <p>${comment.content}</p>
                            ${
                                comment.isOwner || comment.isBeatOwner
                                    ? `
                                <button class="btn btn-sm btn-warning edit-comment" data-id="${comment.id}">Edit</button>
                                <button class="btn btn-sm btn-danger delete-comment" data-id="${comment.id}">Delete</button>`
                                    : ""
                            }
                        </div>
                    `;

                    commentsList.appendChild(commentElement);
                });

                setupCommentActions();
            });
    }

    function setupCommentActions() {
        document.querySelectorAll(".edit-comment").forEach((button) =>
            button.addEventListener("click", handleEditComment)
        );

        document.querySelectorAll(".delete-comment").forEach((button) =>
            button.addEventListener("click", handleDeleteComment)
        );
    }

    function handleEditComment(event) {
        const commentId = event.target.dataset.id;

        Swal.fire({
            title: "Edit Comment",
            input: "textarea",
            inputLabel: "Update your comment",
            inputValue: event.target.closest(".comment").querySelector("p").innerText,
            showCancelButton: true,
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(`/Comments/EditComment`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value,
                    },
                    body: JSON.stringify({ Id: commentId, Content: result.value }),
                })
                    .then((response) => response.json())
                    .then((data) => {
                        if (data.success) {
                            Swal.fire("Success", "Comment updated successfully.", "success");
                            loadComments();
                        } else {
                            Swal.fire("Error", data.message, "error");
                        }
                    });
            }
        });
    }

    function handleDeleteComment(event) {
        const commentId = event.target.dataset.id;

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Yes, delete it!",
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(`/Comments/DeleteComment`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value,
                    },
                    body: JSON.stringify({ Id: commentId }),
                })
                    .then((response) => response.json())
                    .then((data) => {
                        if (data.success) {
                            Swal.fire("Deleted!", "Your comment has been deleted.", "success");
                            loadComments();
                        } else {
                            Swal.fire("Error", data.message, "error");
                        }
                    });
            }
        });
    }
});
