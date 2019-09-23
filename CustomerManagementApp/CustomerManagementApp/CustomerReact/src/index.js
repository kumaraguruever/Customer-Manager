"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var ReactDOM = require("react-dom");
var react_router_dom_1 = require("react-router-dom");
var react_hot_loader_1 = require("react-hot-loader");
var FetchCustomers_1 = require("../Components/FetchCustomers");
var RoutesModule = require("./routes");
var routes = RoutesModule.routes;
//const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
var App = function () { return (React.createElement(React.Fragment, null,
    React.createElement("div", null, "Hello world - Kumar Test - tsx 1234!"),
    React.createElement(react_hot_loader_1.AppContainer, null,
        React.createElement(react_router_dom_1.BrowserRouter, { children: routes })),
    React.createElement(react_router_dom_1.BrowserRouter, null,
        React.createElement("div", null,
            React.createElement("ul", null,
                React.createElement("li", null,
                    React.createElement(react_router_dom_1.Link, { to: "/fetchCustomers" }, "Fetch Customer"))),
            React.createElement("hr", null),
            React.createElement(react_router_dom_1.Route, { path: '/fetchCustomers', component: FetchCustomers_1.FetchCustomer }))))); };
ReactDOM.render(React.createElement(App, null), document.getElementById("root"));
//# sourceMappingURL=index.js.map