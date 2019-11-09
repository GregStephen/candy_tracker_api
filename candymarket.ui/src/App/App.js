import React from 'react';
import {
  BrowserRouter as Router, Route, Redirect, Switch,
} from 'react-router-dom';
import MyNavbar from '../Components/MyNavbar/MyNavbar';
import Auth from '../Components/Auth/Auth';
import Home from '../Components/Home/Home';
import User from '../Components/User/User';
import NewUser from '../Components/NewUser/NewUser';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.scss';
import UserRequests from '../Data/UserRequests';

const PublicRoute = ({ component: Component, authed, ...rest }) => {
  // props contains Location, Match, and History
  const routeChecker = props => (authed === false ? <Component {...props} {...rest}/> : <Redirect to={{ pathname: '/home', state: { from: props.location } }} />);
  return <Route render={props => routeChecker(props)} />;
};

const PrivateRoute = ({ component: Component, authed, ...rest }) => {
  // props contains Location, Match, and History
  const routeChecker = props => (authed === true ? <Component {...props} {...rest}/> : <Redirect to={{ pathname: '/auth', state: { from: props.location } }} />);
  return <Route render={props => routeChecker(props)} />;
};

const defaultUser = {
  FirstName: '',
  LastName: '',
  FavoriteTypeOfCandyId: 0,
  Password: '',
  Email: '',
};

class App extends React.Component {
  state = {
    authed: false,
    userObj: defaultUser,
  };

  userLoggedIn = (user) => {
    this.setState({
      authed : true,
      userObj : user})
  }

  userLoggedOut = () => {
    this.setState({
      authed : false,
      userObj : defaultUser})
  }
  componentDidMount() {
  }


  render() {
    const { authed, userObj } = this.state;
    return (
      <div className="App">
        <Router>
          <MyNavbar authed={ authed } userObj={ userObj } userLoggedOut={ this.userLoggedOut }/>
            <Switch>
              <PublicRoute path='/auth' component={ Auth } authed={ authed } userLoggedIn={ this.userLoggedIn }/>
              <PublicRoute path='/new-user' component={ NewUser } authed={ authed } userLoggedIn = { this.userLoggedIn }/>
              <PrivateRoute path="/home" exact component={ Home } authed={ authed } userObj={ userObj }/>
              <PrivateRoute path='/user/:id' component={ User } authed={ authed } userObj={ userObj }/>
              <Redirect from='*' to='/auth'/>
            </Switch>
        </Router>
      </div>
    );
  }
}

export default App;