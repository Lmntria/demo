$(document).on("click", ".add-to-wishlist", function (e) {
    e.preventDefault()

    let url = $(this).attr("href");

    fetch(url)
        .then(response => {
            if (!response.ok) {
                alert("elave oluna bilmedi");
            }
            return response.text();
        }).then(html => {
            alert("elave olundu");
            $("#wishlist-block").html(html)
        })
})

$(document).on("click", ".delete-product", function (e) {
    e.preventDefault();

    let url = $(this).attr("href");

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url)
                .then(response => {
                    if (response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Your product has been deleted.',
                            'success'
                        ).then(() => window.location.reload())
                    }
                    else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Something went wrong!',
                            showConfirmButton: false,
                            timer: 1000
                        })
                    }
                })
        }
    })
})