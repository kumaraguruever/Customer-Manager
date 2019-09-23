import * as React from "react";
import * as ReactDOM from "react-dom";
import { Route, Switch, Redirect } from 'react-router';
import { FetchCustomer } from "../Components/FetchCustomers";
import { AddCustomer } from "../Components/AddCustomer";

export const routes =
    <Switch>
        <Route path='/fetchCustomer' component={FetchCustomer} />
        <Route path='/addCustomer' component={AddCustomer} />
        <Route path='/customer/edit/:customerid' component={AddCustomer} />
    </Switch>;