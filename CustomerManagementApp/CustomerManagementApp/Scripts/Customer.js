$(function () {
    customer.init();
});

var customer = (function () {

    var init = function () {
        $("#btnClear").on('click', clearSearchFields);
    };
    var clearSearchFields = function () {
        $('#selectedSurname').val('');
        $('#selectedCity').val('');
        $('#selectedCountry').val('');
    };
    var deleteCustomer = function (ID) {
        var result = confirm("Are you sure you want to delete this customer?");
        if (result) {
            $.ajax({
                url: "/customer/delete/" + ID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    location.reload();
                },
                error: function (errormessage) {
                    location.reload();
                }
            });
        }
    };

    return {
        init : init,
        clearFields: clearSearchFields,
        delete: deleteCustomer
       };
})();


//function deleteCustomer(ID) {
//        var ans = confirm("Are you sure you want to delete this Record?");
//        if (ans) {
//            $.ajax({
//                url: "/customer/delete/" + ID,
//                type: "POST",
//                contentType: "application/json;charset=UTF-8",
//                dataType: "json",
//                error: function (errormessage) {
//                    alert(errormessage.responseText);
//                }
//            });
//        }
//}