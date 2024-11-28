
function showDeleteConfirmation(deleteUrl, beatId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to undo this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            const form = document.createElement('form');
            form.method = 'post';
            form.action = deleteUrl;
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'id';
            input.value = beatId;

            form.appendChild(input);
            document.body.appendChild(form);
            form.submit(); 
        }
    });
}
