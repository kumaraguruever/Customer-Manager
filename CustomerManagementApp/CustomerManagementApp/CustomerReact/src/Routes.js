"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_1 = require("react-router");
var FetchCustomers_1 = require("../Components/FetchCustomers");
var AddCustomer_1 = require("../Components/AddCustomer");
exports.routes = React.createElement(react_router_1.Switch, null,
    React.createElement(react_router_1.Route, { path: '/fetchCustomer', component: FetchCustomers_1.FetchCustomer }),
    React.createElement(react_router_1.Route, { path: '/addCustomer', component: AddCustomer_1.AddCustomer }),
    React.createElement(react_router_1.Route, { path: '/customer/edit/:customerid', component: AddCustomer_1.AddCustomer }));
//# sourceMappingURL=Routes.js.map