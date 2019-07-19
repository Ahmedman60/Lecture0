$("submit").submit(function () {
        var txt = $("#Name").val();
        if (txt.includes("@") || txt.includes("*")) {
            $("#Error-Name").text("Name Not includes * Or @");
            return false;
        }
        else if (txt == null) {
            $("#Error-Name").text("Must Enter Name ");
            return false;
        }
        else {
            $("#Error-Name").text("");
            return true;
        }
    });

        $("#Name").blur(function () {
            var txt = $("#Name").val();
            if (txt.includes("@") || txt.includes("*")) {
                $("#Error-Name").text("Not includes * Or @");
                return false;
            }
            else if (txt == null || txt == 0 || txt == "") {
                $("#Error-Name").text("Must Enter Name ");
                return false;
            }
            else {
                $("#Error-Name").text("");
                return true;
            }
        });




