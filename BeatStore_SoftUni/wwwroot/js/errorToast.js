document.addEventListener("DOMContentLoaded", function () {
    const toastElement = document.getElementById("error-toast");
    if (toastElement) {
        const toast = new bootstrap.Toast(toastElement);
        toast.show();
    }
});
