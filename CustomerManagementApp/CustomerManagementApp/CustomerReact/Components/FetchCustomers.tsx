import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';



interface FetchCustomerDataState {
    customersList: CustomerData[];
    loading: boolean;
}  

//export class FetchCustomer extends React.Component
export class FetchCustomer extends React.Component<RouteComponentProps<{}>, FetchCustomerDataState>
{ 
    //render() {
    //    console.log("Inside Fetch Customer Render");
    //    return (
    //        <div> I am from Fetch Customer - New </div>
    //    );
    //}
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = { customersList: [], loading: true };
        fetch('/api/Customer/')
            .then(response => response.json() as Promise<CustomerData[]>)
            .then(data => {
                let convertedCustomersList: CustomerData[] = [];
                data.map((apiCustomer) => { convertedCustomersList.push(this.convertToCustomer(apiCustomer)) });
                this.setState({ customersList: convertedCustomersList, loading: false });
            }).catch(console.log);
        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }
    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCustomerTable(this.state.customersList);
        return <div>
            <h1>Customer Data</h1>
            <p>This component demonstrates fetching Employee data from the server.</p>
            <p>
                <Link to="/addCustomer">Create New</Link>
            </p>
            {contents}
        </div>;
    }
    // Handle Delete request for an employee  
    private handleDelete(id: number) {
        if (!confirm("Do you want to delete customer with Id: " + id))
            return;
        else {
           // let partitionKey: string = 
           // let customerData: CustomerData =
                //this.state.customersList.find((customer) =>
                //    customer.CustomerID == id
                //);
            fetch('api/Customer/Delete/' + id , {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        customersList: this.state.customersList.filter((rec) => {
                            return (rec.CustomerID != id);
                        })
                    });
            });
        }
    }
    private convertToCustomer(apiCustomer:any) {
        const customer = new CustomerData();
        customer.CustomerID = apiCustomer.RowKey;
        customer.FirstName = apiCustomer.FirstName;
        customer.SurName = apiCustomer.SurName;
        customer.City = apiCustomer.City;
        customer.Country = apiCustomer.Country;
        customer.PartitionKey = apiCustomer.PartitionKey; 
        return customer;      
    }
    private handleEdit(id: number) {
        this.props.history.push("/customer/edit/" + id);
    }
    // Returns the HTML table to the render() method.  
    private renderCustomerTable(customerList: CustomerData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>First Name</th>
                    <th>Surname</th>
                    <th>City</th>
                    <th>Country</th>
                </tr>
            </thead>
            <tbody>
                {customerList.map(customer =>
                    <tr key={customer.CustomerID}>
                        <td></td>
                        <td>{customer.FirstName}</td>
                        <td>{customer.SurName}</td>
                        <td>{customer.City}</td>
                        <td>{customer.Country}</td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(customer.CustomerID)}>Edit</a>  |
                            <a className="action" onClick={(id) => this.handleDelete(customer.CustomerID)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

export class CustomerData {
    CustomerID: number = 0;
    FirstName: string = "";
    City: string = "";
    Country: string = "";
    PartitionKey: string = ""; 
    SurName: string = "";
}

