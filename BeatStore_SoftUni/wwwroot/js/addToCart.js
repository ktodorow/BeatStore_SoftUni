document.getElementById("addToCartButton").addEventListener("click", function () {
    const form = document.getElementById("addToCartForm");
    const formData = new FormData(form);

    fetch(form.action, {
        method: "POST",
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Update cart icon (optional)
                const cartIcon = document.getElementById("cartIcon");
                if (cartIcon) {
                    cartIcon.classList.add("has-items");
                }

                // Show success message
                Swal.fire({
                    icon: "success",
                    title: "Success",
                    text: data.message,
                    timer: 2000,
                    showConfirmButton: false
                });
            } else {
                // Show error message
                Swal.fire({
                    icon: "info",
                    title: "Info",
                    text: data.message,
                    timer: 2000,
                    showConfirmButton: false
                });
            }
        })
        .catch(error => console.error("Error:", error));
});
