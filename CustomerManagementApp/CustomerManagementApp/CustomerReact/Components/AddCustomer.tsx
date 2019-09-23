import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import { CustomerData } from './FetchCustomers';

interface AddCustomerDataState {
    title: string;
    loading: boolean;
    cityList: Array<any>;
    customerData: CustomerData;
}

export class AddCustomer extends React.Component<RouteComponentProps<{}>, AddCustomerDataState> {
    constructor(props : RouteComponentProps) {
        super(props);

        this.state = { title: "", loading: true, cityList: [], customerData: new CustomerData };

        //fetch('api/Employee/GetCityList')
        //    .then(response => response.json() as Promise<Array<any>>)
        //    .then(data => {
        //        this.setState({ cityList: data });
        //    });
        const { match: { params } } = this.props;
        var customerID = this.props.match.params["customerID"];
        var partitionKey = this.props.match.params["partitionkey"];

      // var customerID = params["customerID"];
        // This will set state for Edit customer
        if (customerID > 0) {
            fetch('api/Customer?partitionKey=' + partitionKey + "&rowKey=" + customerID)
                .then(response => response.json() as Promise<CustomerData>)
                .then(data => {
                    this.setState({ title: "Edit", loading: false, customerData: data });
                });
        }

        // This will set state for Add employee
        else {
            this.state = { title: "Create", loading: false, cityList: [], customerData: new CustomerData };
        }

        // This binding is necessary to make "this" work in the callback
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.cityList);

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Customer</h3>
            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event.
    private handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        // PUT request for Edit employee.
        if (this.state.customerData.CustomerID) {
            fetch('api/Customer/Edit', {
                method: 'PUT',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchCustomer");
                })
        }

        // POST request for Add employee.
        else {
            fetch('api/Customer/Create', {
                method: 'POST',
                body: data,

            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchCustomer");
                })
        }
    }

    // This will handle Cancel button click event.
    private handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchCustomer");
    }

    // Returns the HTML Form to the render() method.
    private renderCreateForm(cityList: Array<any>) {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="customerID" value={this.state.customerData.CustomerID} />
                    <input type="hidden" name="customerPartitionKey" value={this.state.customerData.PartitionKey} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">First Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="name" defaultValue={this.state.customerData.FirstName} required />
                    </div>
                </div >
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Sur Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="name" defaultValue={this.state.customerData.SurName} required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="City">City</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="City" defaultValue={this.state.customerData.City} required>
                            <option value="">-- Select City --</option>
                            {cityList.map(city =>
                                <option key={city.cityId} value={city.cityName}>{city.cityName}</option>
                            )}
                        </select>
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Gender">Country</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="gender" defaultValue={this.state.customerData.Country} required>
                            <option value="">-- Select Country --</option>
                            <option value="Male">India</option>
                            <option value="Female">United Kingdom</option>
                        </select>
                    </div>
                </div >
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }
}