function updateCartIcon(hasItems) {
    const cartIcon = document.getElementById("cartIcon");
    const badge = cartIcon.querySelector("span");

    if (hasItems) {
        badge.style.display = "inline";
    } else {
        badge.style.display = "none";
    }
}
