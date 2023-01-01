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

