import React from 'react';
import {
    Form, Label, Input, Button,
} from 'reactstrap';
import candyTypeData from '../../Data/CandyTypeRequests';

const defaultUser = {
    firstName: '',
    lastName: '',
    favoriteTypeOfCandyId: '',
    password: '',
    email: '',
};

class NewUser extends React.Component {
    state = {
        newUser: defaultUser,
        error: '',
        candyList : [],
    }

    componentDidMount() {
        candyTypeData.getAllCandyTypes()
          .then(candyList => this.setState({ candyList }))
          .catch(err => console.error('trouble getting candy', err));
      }
    formSubmit = (e) => {
        e.preventDefault();
        const { newUser } = this.state;
        console.error(newUser);
    }

    formFieldStringState = (e) => {
        const tempUser = { ...this.state.newUser };
        tempUser[e.target.id] = e.target.value;
        this.setState({ newUser: tempUser });
    }
    candyList = () => {
        const { candyList } = this.state;
        const options = candyList.map(candy => (
          <option key={candy.id} value={candy.id}>{candy.name}</option>
        ));
        options.unshift(<option key='pick not' value="">Candy Type</option>);
        return options;
      }

    render() {
        const { newUser, error } = this.state;
        return (
            <div className="NewUser container">
                <h1 className="join-header">JOIN US!</h1>
        <Form className="row justify-content-center new-user-form" onSubmit={this.formSubmit}>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="firstName">First Name</Label>
            <Input
            type="text"
            className="form-control"
            id="firstName"
            value={newUser.firstName}
            onChange={this.formFieldStringState}
            placeholder="Greg"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="lastName">Last Name</Label>
            <Input
            type="text"
            className="form-control"
            id="lastName"
            value={newUser.lastName}
            onChange={this.formFieldStringState}
            placeholder="Stephen"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="email">Email</Label>
            <Input
            type="email"
            className="form-control"
            id="email"
            value={newUser.email}
            onChange={this.formFieldStringState}
            placeholder="Greg@CandyMarket.com"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="password">Password</Label>
            <Input
            type="password"
            className="form-control"
            id="password"
            value={newUser.password}
            onChange={this.formFieldStringState}
            required
            />
          </div>
          <div className="favCandy col-12 row justify-content-center">
            <div className="form-group col-3 col-md-2">
              <Label for="favortieTypeOfCandyId">Favorite Type of Candy</Label>
              <Input
              type="select"
              className="form-control"
              id="favortieTypeOfCandyId"
              value={newUser.favortieTypeOfCandyId}
              onChange={this.formFieldStringState}
              required
              >
              {this.candyList()}
              </Input>
            </div>
          </div>
          
          <h2 className="error col-12">{error}</h2>
          <Button type="submit" className="new-user-btn btn btn-success btn-lg">Join Candy Market!</Button>
        </Form>
            </div>
        )
    }
}

export default NewUser;
