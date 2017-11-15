
function friend(name, twt, twtName) {
    return {
        name: ko.observable(name),
        isOnTwitter: ko.observable(twt),
        twitterName: ko.observable(twtName),
        remove: function () {
            viewModel.friends.remove(this);
        }
    };
}

function getList() {
    var jsonResult = new Array();
    var someResult = new Array();

    var promise = testAjax();

    promise.success(function (data) {
        someResult = data;
    });

    for (var i = 0; i < someResult.length; i++) {
        var fr = new friend(someResult[i].Name, someResult[i].IsOnTwitter, someResult[i].TwitterName);
        jsonResult.push(fr);
    }

    return jsonResult; 
}

function testAjax() {
    return $.ajax({
        url: "/Home/GetFriendsList",
        async: false, 
        type: "post",
        contentType: "application/json"
    });
}

