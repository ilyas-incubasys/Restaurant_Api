var NumberOfItems = 1;
var serverUrl = "http://localhost:51526";
$(document).ready(function () {
    $('[name="chkIsDeal"]').click(function () {
        if ($(this).is(':checked')) {
            $('#menuItemsPanel').show();
        }
        else {
            $('#menuItemsPanel').hide();
        }
    });

    $('.add').click(function () {
        NumberOfItems++;
        $('.appendItems').append('<div id="item-row' + NumberOfItems + '"><div class="row"> <input type="text" name="name" id="ItemName' + NumberOfItems + '" placeholder="Name" /></div><div class="row"><input type="text" name="imageUrl" id="ItemImageUrl' + NumberOfItems + '" placeholder="ImageUrl" /> </div><div class="row"><textarea name="description" id="ItemDescription' + NumberOfItems + '" data-val="true" class="text-box multi-line" placeholder="Description"></textarea></div> </div>');

    });
    saveMenu();
});
function validateInputs() {
    $("#ErrorMessage").html("");
    $("#SuccessMessage").css("color", "");
    $("#ErrorMessage").css("color", "");
    $("#Name,#Price,#ImageUrl,#Description,#CreatedDate,#CreatedBy,#SubCategoryId").css("border-color", "");
    if ($("#Name").val() === "" || $("#Price").val() === "" || $("#ImageUrl").val() === "" || $("#Description").val() === "" || $("#CreatedDate").val() === "" || $("#CreatedBy").val() === "" || $("#SubCategoryId").val() === "") {
        $("#ErrorMessage").css("color", "red");
        if ($("#Name").val() === "") {
            $("#Name").css("border-color", "red");
            $("#ErrorMessage").html("Name is required");
        }
        if ($("#Price").val() === "") {
            $("#Price").css("border-color", "red");
            $("#ErrorMessage").html("Price is required");
        }
        if ($("#ImageUrl").val() === "") {
            $("#ImageUrl").css("border-color", "red");
            $("#ErrorMessage").html("ImageUrl is required");
        }

        if ($("#Description").val() === "") {
            $("#Description").css("border-color", "red");
            $("#ErrorMessage").html("Description is required")
        }
        if ($("#CreatedDate").val() === "") {
            $("#CreatedDate").css("border-color", "red");
            $("#ErrorMessage").html("CreatedDate is required")
        }
        if ($("#CreatedBy").val() === "") {
            $("#CreatedBy").css("border-color", "red");
            $("#ErrorMessage").html("CreatedBy is required")
        }
        if ($("#SubCategoryId").val() === "") {
            $("#SubCategoryId").css("border-color", "red");
            $("#ErrorMessage").html("SubCategory is required")
        }
        $("#ErrorMessage").html("Red border fields are required")
        return false;
    }
    return true;
}

function saveMenu() {
    $(".btn").click(function (e) {
        if (!validateInputs())
        { return false; }
        $("#SuccessMessage").html("Saving, please wait ...");

        var menuItems = Array();
        if ($('[name="chkIsDeal"]').is(':checked')) {
            for (i = 1; i <= NumberOfItems ; i++) {

                var items = {
                    Name: $("#ItemName" + (i)).val(),
                    ImageUrl: $("#ItemImageUrl" + (i)).val(),
                    Description: $("#ItemDescription" + (i)).val(),
                    CreatedDate: $("#CreatedDate").val(),
                    CreatedBy: $("#CreatedBy").val()
                };

                menuItems.push(items);
            }
        }

        var userID = "";

        var username = "";
        var menu = {
            Name: $("#Name").val(),
            Price: $("#Price").val(),
            ImageUrl: $("#ImageUrl").val(),
            Description: $("#Description").val(),
            MenuItems: menuItems,
            CreatedDate: $("#CreatedDate").val(),
            CreatedBy: $("#CreatedBy").val(),
            SubCategoryId: $("#SubCategoryId").val()
        };
        $.ajax({
            headers: {
                Accept: "application/json; charset=utf-8",
                "Content-Type": "application/json; charset=utf-8"
            },
            url: serverUrl + '/menus/CreateMenu',
            type: 'POST',
            cache: true,
            data: JSON.stringify(menu),
            success: function (data) {;
                if (data) {
                    var url = serverUrl + '/menus/';
                    window.location.href = url;
                }
                else {
                    $("#SuccessMessage").html("");
                    $("#ErrorMessage").css("color", "red");
                    $("#ErrorMessage").html('Bad Request');
                }

            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#SuccessMessage").html("");
                $("#ErrorMessage").css("color", "red");
                $("#ErrorMessage").html(errorThrown);
                return false;
            },

        });
    });
}