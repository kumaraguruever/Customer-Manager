$(function () {
   customerViewModel.init();
});

var customerViewModel = (function(){
    var self = this;
    self.CustomerID = ko.observable("0");
    self.FirstName = ko.observable('Test');
    self.SurName = ko.observable('Test Surname');
    self.EmailAddress = ko.observable("");
    self.AddressLine1 = ko.observable("");
    self.AddressLine2 = ko.observable("");
    self.AddressLine3 = ko.observable("");
    self.City = ko.observable("");
    self.Country = ko.observable("India");
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