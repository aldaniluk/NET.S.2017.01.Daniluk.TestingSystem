$(document).ready(function () {
    $(".explanation").hide();

    $(".radiobtn").click(function () {
        var questionRadioBtn = $(this).attr("class").split(' ')[0];
        $("." + questionRadioBtn).hide();
        var answer = $(this).attr("id");

        $.ajax({
            method: "GET",
            url: "/Answer/IsAnswerTrue/" + answer,
            success: function (data) {
                console.log(data);
                if (data == "True") {
                    $("." + answer).addClass("correct");
                }
                else {
                    $("." + answer).addClass("wrong");
                }
                $(".explanation_" + answer).show();
            }
        });
    })
})