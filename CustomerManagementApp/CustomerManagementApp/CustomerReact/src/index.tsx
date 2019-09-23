import * as React from "react";
import * as ReactDOM from "react-dom";
import { Route, BrowserRouter as Router, Link, BrowserRouter } from "react-router-dom";
import { AppContainer } from "react-hot-loader";
import { FetchCustomer } from "../Components/FetchCustomers";
import * as RoutesModule from "./routes";
let routes = RoutesModule.routes;
//const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;

const App = () => (
    <>
        <div>Hello world - Kumar Test - tsx 1234!</div>
           
        <AppContainer>
            <BrowserRouter children={routes}  />
        </AppContainer>
        <Router>
            <div>
                <ul>
                 
                    <li>
                        <Link to="/fetchCustomers">Fetch Customer</Link>
                    </li>
                </ul>

                <hr />

                <Route path='/fetchCustomers' component={FetchCustomer}/>
            </div>
        </Router>
    </>);

ReactDOM.render(<App />,
    document.getElementById("root"));