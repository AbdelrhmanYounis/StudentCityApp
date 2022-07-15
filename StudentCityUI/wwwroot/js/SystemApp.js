var url = "http://localhost:59194/api/";

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function Get(controller, callBack) {
 

    $.ajax({
        headers: {
            'Authorization': getCookie("AccessToken"),
            'Id': getCookie("UserId")
        },
        type: "GET",
        url: url + controller,

        success: function (response) {
            
            callBack(response);
        }

    });
}

function Post(controller, data, callBack) {
   
    $.ajax({
        headers: {
            'Authorization': getCookie("AccessToken"),
            'Id': getCookie("UserId")
        },
        type: "POST",
        url: url + controller,
        data: JSON.stringify(data),
        contentType:'application/json;charset=utf-8',
        dataType : 'json',
        success: function (response) {
            callBack(response);
        }

    });
}