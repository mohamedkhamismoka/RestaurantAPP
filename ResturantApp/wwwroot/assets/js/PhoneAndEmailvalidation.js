$(document).ready(function () {
    $("#email").on("input", function () {
        if ($(this).val() != '') {


            $.ajax({
                url: '/Customer/CheckEmail',
                method: 'Post',
                data: { email: $(this).val() },
                success: function (result) {
                    console.log(result)
                    if (!result.result) {
                        $("#email").removeClass("invalidemail");
                        $("#email").addClass("validemail");
                        $("#myspanemail").text("");
                        $('#crt').prop("disabled", false);
                    } else {

                        $("#email").removeClass("validemail");
                        $("#email").addClass("invalidemail");
                        $("#myspanemail").text("Already Existed Email");
                        $('#crt').prop("disabled", true);

                    }
                }
            })
        } else {
            $("#email").removeClass("invalidemail");
            $("#email").addClass("validemail");
            $("#myspanemail").text("");

        }
    })

    $("#phone").on("input", function () {
        if ($(this).val() != "") {


            $.ajax({
                url: '/Customer/CheckPhone',
                method: 'Post',
                data: { Phone: $(this).val() },
                success: function (result) {
                    console.log(result)
                    if (!result.result) {
                        $("#phone").removeClass("invalidemail");
                        $("#phone").addClass("validemail");
                        $("#myspanphone").text("");
                        $('#crt').prop("disabled", false);
                    } else {
                        $("#phone").removeClass("validemail");
                        $("#phone").addClass("invalidemail");
                        $("#myspanphone").text("Already Existed phone");
                        $('#crt').prop("disabled", true);
                    }
                }
            })
        } else {
            $("#phone").removeClass("invalidemail");
            $("#phone").addClass("validemail");
            $("#myspanphone").text("");
        }
    })
})