function updateCartIcon(hasItems) {
    const cartIcon = document.getElementById("cartIcon");
    const badge = cartIcon.querySelector("span");

    if (hasItems) {
        badge.style.display = "inline";
    } else {
        badge.style.display = "none";
    }
}

// Fetch initial cart state (if needed)
fetch("@Url.Action("GetCartStatus", "Cart")")
    .then(response => response.json())
    .then(data => {
        updateCartIcon(data.hasItems);
    });