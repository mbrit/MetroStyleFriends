var listsModel = {
    lists: ko.observableArray()
}

$(document).ready(function () {

    // load...
    refreshLists();

    // bind...
    ko.applyBindings(listsModel);

});

function refreshLists() {

    $.ajax({
        url: "api/lists",
        success: function (results) {

            // reset...
            listsModel.lists.removeAll();
            for (var index in results)
                listsModel.lists.push(results[index]);

            // update...
            refreshSubscriptions();
        }
    });

}

function refreshSubscriptions() {

    $.ajax({
        url: "api/subscriptions",
        success: function (results) {

            // get the buttons...
            var buttons = $("#subscriptionslist").find("input[type='button']");
            buttons.each(function () {

                var list = getList($(this).data("id"));

                $(this).addClass("btn-success");
                $(this).val("Subscribe to " + list.Name);
                list.IsSubscribed = false;

                for (var j in results) {
                    var sub = results[j];
                    if (list.ListId == sub.ListId) {

                        $(this).removeClass("btn-success");
                        $(this).addClass("btn-danger");
                        $(this).val("Unsubscribe from " + list.Name);
                        list.IsSubscribed = true;

                    }
                }

                $(this).unbind("click");
                if (list.IsMandatory)
                    $(this).click(handleMandatorySubscriptionClick);
                else
                    $(this).click(handleSubscriptionClick);

            });

        }
    });

}

function getList(id) {
    for (var index in listsModel.lists()) {
        var list = listsModel.lists()[index];
        if (list.ListId == id)
            return list;
    }
    return null;
}

function handleMandatorySubscriptionClick() {

    var buttons = $("#subscriptionslist").find("input[type='button']");

    var id = $(this).data("id");
    var subscribed = getList(id).IsSubscribed;
    if (subscribed) {
        if (confirm("This is a mandatory list. Unsubscribing from it will unsubscribe you from all lists. Proceed?")) {

            buttons.attr("disabled", true);
            buttons.removeClass("btn-success");
            buttons.removeClass("btn-danger");
            buttons.addClass("btn-warning");
            buttons.val("Unsubscribing...");

            // go...
            $.ajax({
                url: "api/listactions/0/unsubscribeall",
                success: function (result) {
                    refreshSubscriptions();
                },
                complete: function () {
                    buttons.removeAttr("disabled");
                }
            });

        }
    } else {

        buttons.attr("disabled", true);
        buttons.removeClass("btn-success");
        buttons.removeClass("btn-danger");
        buttons.addClass("btn-warning");
        buttons.val("Subscribing...");

        // setup...
        $.ajax({
            url: "api/listactions/0/setupuser",
            success: function (result) {
                refreshSubscriptions();
            },
            complete: function () {
                buttons.removeAttr("disabled");
            }
        });

    }
}

function handleSubscriptionClick() {

    // run...
    var button = $(this);
    button.attr("disable", true);

    // what are we doing?
    var id = $(this).data("id");
    var subscribed = getList(id).IsSubscribed;
    if (subscribed) {
        button.removeClass("btn-danger");
        button.addClass("btn-warning");
        $(this).val("Unsubscribing from " + $(this).data("name"));

        $.ajax({
            url: "api/listactions/" + $(this).data("id") + "/unsubscribe",
            success: function (result) {
                button.removeClass("btn-warning");
                button.addClass("btn-success");
                button.val("Subscribe to " + button.data("name"));
                getList(id).IsSubscribed = false;
            }
        });


    } else {
        button.removeClass("btn-success");
        button.addClass("btn-warning");
        $(this).val("Subscribing to " + $(this).data("name"));

        $.ajax({
            url: "api/listactions/" + $(this).data("id") + "/subscribe",
            success: function (result) {
                button.removeClass("btn-warning");
                button.addClass("btn-danger");
                button.val("Unsubscribe from " + button.data("name"));
                getList(id).IsSubscribed = true;
            }
        });

    }

}
