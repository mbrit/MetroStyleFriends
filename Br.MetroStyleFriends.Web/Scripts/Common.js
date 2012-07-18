function handleAjaxError(err) {
    alert(err);
}

function logout() {

    $.ajax({
        url: "account/logoff",
        success: function () {
            location.reload();
        }
    });

}