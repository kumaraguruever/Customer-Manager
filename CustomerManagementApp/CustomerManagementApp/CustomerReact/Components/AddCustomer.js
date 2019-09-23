"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var FetchCustomers_1 = require("./FetchCustomers");
var AddCustomer = /** @class */ (function (_super) {
    __extends(AddCustomer, _super);
    function AddCustomer(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { title: "", loading: true, cityList: [], customerData: new FetchCustomers_1.CustomerData };
        //fetch('api/Employee/GetCityList')
        //    .then(response => response.json() as Promise<Array<any>>)
        //    .then(data => {
        //        this.setState({ cityList: data });
        //    });
        var params = _this.props.match.params;
        var customerID = _this.props.match.params["customerID"];
        var partitionKey = _this.props.match.params["partitionkey"];
        // var customerID = params["customerID"];
        // This will set state for Edit customer
        if (customerID > 0) {
            fetch('api/Customer?partitionKey=' + partitionKey + "&rowKey=" + customerID)
                .then(function (response) { return response.json(); })
                .then(function (data) {
                _this.setState({ title: "Edit", loading: false, customerData: data });
            });
        }
        // This will set state for Add employee
        else {
            _this.state = { title: "Create", loading: false, cityList: [], customerData: new FetchCustomers_1.CustomerData };
        }
        // This binding is necessary to make "this" work in the callback
        _this.handleSave = _this.handleSave.bind(_this);
        _this.handleCancel = _this.handleCancel.bind(_this);
        return _this;
    }
    AddCustomer.prototype.render = function () {
        var contents = this.state.loading
            ? React.createElement("p", null,
                React.createElement("em", null, "Loading..."))
            : this.renderCreateForm(this.state.cityList);
        return React.createElement("div", null,
            React.createElement("h1", null, this.state.title),
            React.createElement("h3", null, "Customer"),
            React.createElement("hr", null),
            contents);
    };
    // This will handle the submit form event.
    AddCustomer.prototype.handleSave = function (event) {
        var _this = this;
        event.preventDefault();
        var data = new FormData(event.target);
        // PUT request for Edit employee.
        if (this.state.customerData.CustomerID) {
            fetch('api/Customer/Edit', {
                method: 'PUT',
                body: data,
            }).then(function (response) { return response.json(); })
                .then(function (responseJson) {
                _this.props.history.push("/fetchCustomer");
            });
        }
        // POST request for Add employee.
        else {
            fetch('api/Customer/Create', {
                method: 'POST',
                body: data,
            }).then(function (response) { return response.json(); })
                .then(function (responseJson) {
                _this.props.history.push("/fetchCustomer");
            });
        }
    };
    // This will handle Cancel button click event.
    AddCustomer.prototype.handleCancel = function (e) {
        e.preventDefault();
        this.props.history.push("/fetchCustomer");
    };
    // Returns the HTML Form to the render() method.
    AddCustomer.prototype.renderCreateForm = function (cityList) {
        return (React.createElement("form", { onSubmit: this.handleSave },
            React.createElement("div", { className: "form-group row" },
                React.createElement("input", { type: "hidden", name: "customerID", value: this.state.customerData.CustomerID }),
                React.createElement("input", { type: "hidden", name: "customerPartitionKey", value: this.state.customerData.PartitionKey })),
            React.createElement("div", { className: "form-group row" },
                React.createElement("label", { className: " control-label col-md-12", htmlFor: "Name" }, "First Name"),
                React.createElement("div", { className: "col-md-4" },
                    React.createElement("input", { className: "form-control", type: "text", name: "name", defaultValue: this.state.customerData.FirstName, required: true }))),
            React.createElement("div", { className: "form-group row" },
                React.createElement("label", { className: " control-label col-md-12", htmlFor: "Name" }, "Sur Name"),
                React.createElement("div", { className: "col-md-4" },
                    React.createElement("input", { className: "form-control", type: "text", name: "name", defaultValue: this.state.customerData.SurName, required: true }))),
            React.createElement("div", { className: "form-group row" },
                React.createElement("label", { className: "control-label col-md-12", htmlFor: "City" }, "City"),
                React.createElement("div", { className: "col-md-4" },
                    React.createElement("select", { className: "form-control", "data-val": "true", name: "City", defaultValue: this.state.customerData.City, required: true },
                        React.createElement("option", { value: "" }, "-- Select City --"),
                        cityList.map(function (city) {
                            return React.createElement("option", { key: city.cityId, value: city.cityName }, city.cityName);
                        })))),
            React.createElement("div", { className: "form-group row" },
                React.createElement("label", { className: "control-label col-md-12", htmlFor: "Gender" }, "Country"),
                React.createElement("div", { className: "col-md-4" },
                    React.createElement("select", { className: "form-control", "data-val": "true", name: "gender", defaultValue: this.state.customerData.Country, required: true },
                        React.createElement("option", { value: "" }, "-- Select Country --"),
                        React.createElement("option", { value: "Male" }, "India"),
                        React.createElement("option", { value: "Female" }, "United Kingdom")))),
            React.createElement("div", { className: "form-group" },
                React.createElement("button", { type: "submit", className: "btn btn-default" }, "Save"),
                React.createElement("button", { className: "btn", onClick: this.handleCancel }, "Cancel"))));
    };
    return AddCustomer;
}(React.Component));
exports.AddCustomer = AddCustomer;
//# sourceMappingURL=AddCustomer.js.map