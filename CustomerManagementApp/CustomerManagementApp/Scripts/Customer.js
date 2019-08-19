$(function () {
   customerViewModel.init();
});

var customerViewModel = (function(){
    var self = this;
    self.CustomerID = ko.observable("0");
    self.FirstName = ko.observable("");
    self.SurName = ko.observable("");
    self.EmailAddress = ko.observable("");
    self.AddressLine1 = ko.observable("");
    self.AddressLine2 = ko.observable("");
    self.AddressLine3 = ko.observable("");
    self.City = ko.observable("");
    self.Country = ko.observable("");
    self.MobileNumber = ko.observable("");
    self.LandlineNumber = ko.observable("");

    var customerData = {
        CustomerID: self.CustomerID,
        FirstName: self.FirstName,
        SurName: self.SurName,
        EmailAddress: self.EmailAddress,
        AddressLine1: self.AddressLine1,
        AddressLine2: self.AddressLine2,
        AddressLine3: self.AddressLine3,
        City: self.City,
        Country: self.Country,
        MobileNumber: self.MobileNumber,
        LandlineNumber: self.LandlineNumber
    };
   
    self.save = function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        if (token) {
            customerData.__RequestVerificationToken = token;
        }
            var saveData = {
            url: "/Customer/Create",
            data: customerData,
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            success: successCallback,
            error: errorCallback
        };
        $.ajax(saveData);
    };
    var init = function () {
        $("#btnClear").on('click', clearSearchFields);
    };
    var clearSearchFields = function () {
        $('#selectedSurname').val('');
        $('#selectedCity').val('');
        $('#selectedCountry').val('');
    };

    var deleteCustomer = function (partitionKey, rowKey) {
        var result = confirm("Are you sure you want to delete this customer?");
        if (result) {
            var parameters = {
                partitionKey: partitionKey,
                rowKey: rowKey
            };
            $.ajax({
                url: "/customer/delete",
                type: "DELETE",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(parameters),
                success: function (result) {
                    location.reload();
                },
                error: function (errormessage) {
                    location.reload();
                }
            });
        }
    };
    self.returnToCustomerList = function () { window.location.href = '/Customer'; };

    var successCallback = function successCallback(data) {
        returnToCustomerList();
    };

    var errorCallback = function errorCallback(err) {
        returnToCustomerList();
    };

    return {
        init : init,
        clearFields: clearSearchFields,
        delete: deleteCustomer
       };
})();
ko.applyBindings(customerViewModel);