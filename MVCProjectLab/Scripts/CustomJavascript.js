$(function () {   
    $("#btnsubmit").mouseover(function () {

        $("#btnsubmit").css("background-color", "red");
    })
    $("#btnsubmit").mouseout(function () {

        $("#btnsubmit").css("background-color", "gray");
    })
});