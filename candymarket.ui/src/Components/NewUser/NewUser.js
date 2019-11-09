import React from 'react';
import {
    Form, Label, Input, Button,
} from 'reactstrap';
import candyTypeData from '../../Data/CandyTypeRequests';
import UserRequests from '../../Data/UserRequests';

const defaultUser = {
    FirstName: '',
    LastName: '',
    FavoriteTypeOfCandyId: 0,
    Password: '',
    Email: '',
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
        UserRequests.postUser(newUser)
          .then(() => {
            var Password = newUser.Password;
            var Email = newUser.Email;
            UserRequests.logInUser(Email, Password)
              .then((user) => {
                this.props.userLoggedIn(user)
              });
            })
          .catch();
    }

    formFieldStringState = (e) => {
        const tempUser = { ...this.state.newUser };
        tempUser[e.target.id] = e.target.value;
        this.setState({ newUser: tempUser });
    }
    candySelector = (e) => {
      const tempUser = { ...this.state.newUser };
      tempUser[e.target.id] = parseInt(e.target.value, 10);
      this.setState({ newUser : tempUser });
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
            <Label for="FirstName">First Name</Label>
            <Input
            type="text"
            className="form-control"
            id="FirstName"
            value={newUser.FirstName}
            onChange={this.formFieldStringState}
            placeholder="Greg"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="LastName">Last Name</Label>
            <Input
            type="text"
            className="form-control"
            id="LastName"
            value={newUser.LastName}
            onChange={this.formFieldStringState}
            placeholder="Stephen"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="Email">Email</Label>
            <Input
            type="Email"
            className="form-control"
            id="Email"
            value={newUser.Email}
            onChange={this.formFieldStringState}
            placeholder="Greg@CandyMarket.com"
            required
            />
          </div>
          <div className="form-group col-11 col-md-9 col-lg-7">
            <Label for="Password">Password</Label>
            <Input
            type="Password"
            className="form-control"
            id="Password"
            value={newUser.Password}
            onChange={this.formFieldStringState}
            required
            />
          </div>
          <div className="favCandy col-12 row justify-content-center">
            <div className="form-group col-3 col-md-2">
              <Label for="FavoriteTypeOfCandyId">Favorite Type of Candy</Label>
              <Input
              type="select"
              className="form-control"
              id="FavoriteTypeOfCandyId"
              value={newUser.FavoriteTypeOfCandyId}
              onChange={this.candySelector}
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
