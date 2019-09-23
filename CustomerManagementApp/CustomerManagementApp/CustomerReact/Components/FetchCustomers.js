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
var react_router_dom_1 = require("react-router-dom");
//export class FetchCustomer extends React.Component
var FetchCustomer = /** @class */ (function (_super) {
    __extends(FetchCustomer, _super);
    //render() {
    //    console.log("Inside Fetch Customer Render");
    //    return (
    //        <div> I am from Fetch Customer - New </div>
    //    );
    //}
    function FetchCustomer(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { customersList: [], loading: true };
        fetch('/api/Customer/')
            .then(function (response) { return response.json(); })
            .then(function (data) {
            var convertedCustomersList = [];
            data.map(function (apiCustomer) { convertedCustomersList.push(_this.convertToCustomer(apiCustomer)); });
            _this.setState({ customersList: convertedCustomersList, loading: false });
        }).catch(console.log);
        // This binding is necessary to make "this" work in the callback  
        _this.handleDelete = _this.handleDelete.bind(_this);
        _this.handleEdit = _this.handleEdit.bind(_this);
        return _this;
    }
    FetchCustomer.prototype.render = function () {
        var contents = this.state.loading
            ? React.createElement("p", null,
                React.createElement("em", null, "Loading..."))
            : this.renderCustomerTable(this.state.customersList);
        return React.createElement("div", null,
            React.createElement("h1", null, "Customer Data"),
            React.createElement("p", null, "This component demonstrates fetching Employee data from the server."),
            React.createElement("p", null,
                React.createElement(react_router_dom_1.Link, { to: "/addCustomer" }, "Create New")),
            contents);
    };
    // Handle Delete request for an employee  
    FetchCustomer.prototype.handleDelete = function (id) {
        var _this = this;
        if (!confirm("Do you want to delete customer with Id: " + id))
            return;
        else {
            // let partitionKey: string = 
            // let customerData: CustomerData =
            //this.state.customersList.find((customer) =>
            //    customer.CustomerID == id
            //);
            fetch('api/Customer/Delete/' + id, {
                method: 'delete'
            }).then(function (data) {
                _this.setState({
                    customersList: _this.state.customersList.filter(function (rec) {
                        return (rec.CustomerID != id);
                    })
                });
            });
        }
    };
    FetchCustomer.prototype.convertToCustomer = function (apiCustomer) {
        var customer = new CustomerData();
        customer.CustomerID = apiCustomer.RowKey;
        customer.FirstName = apiCustomer.FirstName;
        customer.SurName = apiCustomer.SurName;
        customer.City = apiCustomer.City;
        customer.Country = apiCustomer.Country;
        customer.PartitionKey = apiCustomer.PartitionKey;
        return customer;
    };
    FetchCustomer.prototype.handleEdit = function (id) {
        this.props.history.push("/customer/edit/" + id);
    };
    // Returns the HTML table to the render() method.  
    FetchCustomer.prototype.renderCustomerTable = function (customerList) {
        var _this = this;
        return React.createElement("table", { className: 'table' },
            React.createElement("thead", null,
                React.createElement("tr", null,
                    React.createElement("th", null),
                    React.createElement("th", null, "First Name"),
                    React.createElement("th", null, "Surname"),
                    React.createElement("th", null, "City"),
                    React.createElement("th", null, "Country"))),
            React.createElement("tbody", null, customerList.map(function (customer) {
                return React.createElement("tr", { key: customer.CustomerID },
                    React.createElement("td", null),
                    React.createElement("td", null, customer.FirstName),
                    React.createElement("td", null, customer.SurName),
                    React.createElement("td", null, customer.City),
                    React.createElement("td", null, customer.Country),
                    React.createElement("td", null,
                        React.createElement("a", { className: "action", onClick: function (id) { return _this.handleEdit(customer.CustomerID); } }, "Edit"),
                        "  |",
                        React.createElement("a", { className: "action", onClick: function (id) { return _this.handleDelete(customer.CustomerID); } }, "Delete")));
            })));
    };
    return FetchCustomer;
}(React.Component));
exports.FetchCustomer = FetchCustomer;
var CustomerData = /** @class */ (function () {
    function CustomerData() {
        this.CustomerID = 0;
        this.FirstName = "";
        this.City = "";
        this.Country = "";
        this.PartitionKey = "";
        this.SurName = "";
    }
    return CustomerData;
}());
exports.CustomerData = CustomerData;
//# sourceMappingURL=FetchCustomers.js.map