"use strict";
//import React from 'react';
//import { RouteComponentProps } from 'react-router';
//import { Link, NavLink } from 'react-router-dom';
//interface FetchCustomerDataState {
//    customersList: CustomerData[];
//    loading: boolean;
//}
//export class NewFetchCustomer extends React.Component<RouteComponentProps<{}>, FetchCustomerDataState> {
//    //render() {
//    //    return (
//    //        <div> I am from Fetch Customer </div>
//    //    );
//    //}
//    constructor() {
//        super();
//        this.state = { customersList: [], loading: true };
//        fetch('api/Customer/')
//            .then(response => response.json() as Promise<CustomerData[]>)
//            .then(data => {
//                this.setState({ customersList: data, loading: false });
//            });
//        // This binding is necessary to make "this" work in the callback  
//        this.handleDelete = this.handleDelete.bind(this);
//        this.handleEdit = this.handleEdit.bind(this);
//    }
//    public render() {
//        let contents = this.state.loading
//            ? <p><em>Loading...</em></p>
//            : this.renderCustomerTable(this.state.customersList);
//        return <div>
//            <h1>Customer Data</h1>
//            <p>This component demonstrates fetching Employee data from the server.</p>
//            <p>
//                <Link to="/addCustomer">Create New</Link>
//            </p>
//            {contents}
//        </div>;
//    }
//    // Handle Delete request for an employee  
//    private handleDelete(id: number) {
//        if (!confirm("Do you want to delete employee with Id: " + id))
//            return;
//        else {
//            fetch('api/Employee/Delete/' + id, {
//                method: 'delete'
//            }).then(data => {
//                this.setState(
//                    {
//                        customersList: this.state.customersList.filter((rec) => {
//                            return (rec.customerID != id);
//                        })
//                    });
//            });
//        }
//    }
//    private handleEdit(id: number) {
//        this.props.history.push("/employee/edit/" + id);
//    }
//    // Returns the HTML table to the render() method.  
//    private renderCustomerTable(customerList: CustomerData[]) {
//        return <table className='table'>
//            <thead>
//                <tr>
//                    <th></th>
//                    <th>First Name</th>
//                    <th>Surname</th>
//                    <th>City</th>
//                    <th>Country</th>
//                </tr>
//            </thead>
//            <tbody>
//                {customerList.map(customer =>
//                    <tr key={customer.customerID}>
//                        <td></td>
//                        <td>{customer.firstname}</td>
//                        <td>{customer.surname}</td>
//                        <td>{customer.city}</td>
//                        <td>{customer.country}</td>
//                        <td>
//                            <a className="action" onClick={(id) => this.handleEdit(customer.customerID)}>Edit</a>  |
//                            <a className="action" onClick={(id) => this.handleDelete(customer.customerID)}>Delete</a>
//                        </td>
//                    </tr>
//                )}
//            </tbody>
//        </table>;
//    }
//}
//export class CustomerData {
//    customerID: number = 0;
//    firstname: string = "";
//    gender: string = "";
//    city: string = "";
//    country: string = "";
//    surname: string = "";
//}
//# sourceMappingURL=NewFetchCustomers.js.map