import React from 'react';
import {
  BrowserRouter as Router, Route, Redirect, Switch,
} from 'react-router-dom';
import MyNavbar from '../Components/MyNavbar/MyNavbar';
import Auth from '../Components/Auth/Auth';
import Home from '../Components/Home/Home';
import User from '../Components/User/User';
import NewUser from '../Components/NewUser/NewUser';
import TradePage from '../Components/TradePage/TradePage';
import CandyList from '../Components/CandyList/CandyList';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.scss';
import UserRequests from '../Data/UserRequests';
import OfferRequests from '../Data/OfferRequests';


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

  refreshUserObj = () => {
    const {userObj} = this.state;
    UserRequests.getUserById(userObj.id)
      .then((refreshedUserObj) => {
        this.setState({ userObj : refreshedUserObj })
      })
      .catch()
  }

  candyAte = (candyId) => {
    const {userObj} = this.state;
    UserRequests.eatCandy(userObj.id, candyId)
      .then(() => {
        this.refreshUserObj();
      })
      .catch();
  }

  candyDonated = (candyId) => {
    const {userObj} = this.state;
    UserRequests.donateCandy(userObj.id, candyId)
      .then(() => {
        this.refreshUserObj();
      })
      .catch();
  };

  candyUpForTrade = (userCandyId) => {
    UserRequests.putCandyUpForTrade(userCandyId)
      .then(() => {
        this.refreshUserObj();
      })
      .catch();
  };

  candyNotUpForTrade = (userCandyId) => new Promise((resolve, reject) => {
    UserRequests.takeCandyOffTrade(userCandyId)
      .then(() => {
        this.refreshUserObj();
        resolve('');
    }).catch(err => reject(err))
  });

  candyTradeOffered = (offer) => {
    OfferRequests.addOffer(offer)
      .then(() => {
        this.refreshUserObj();
      })
      .catch(err => console.error(err));
  }


  candyBought = (candyId) => {
    const { userObj } = this.state;
    UserRequests.buyCandy(userObj.id, candyId)
      .then(() => {
        this.refreshUserObj();
      })
      .catch();
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
              <PrivateRoute path='/home' exact component={ Home } authed={ authed } userObj={ userObj } candyAte={this.candyAte} candyDonated={this.candyDonated} candyUpForTrade={this.candyUpForTrade} candyNotUpForTrade={this.candyNotUpForTrade}/>
              <PrivateRoute path='/candy-list' component={ CandyList } authed={ authed } userObj={ userObj} candyBought={this.candyBought}/>              
              <PrivateRoute path='/user/:id' component={ User } authed={ authed } userObj={ userObj }/>
              <PrivateRoute path='/trade' component={TradePage} authed={ authed } userObj= { userObj } candyNotUpForTrade={this.candyNotUpForTrade} candyTradeOffered={this.candyTradeOffered}/>
              <Redirect from='*' to='/auth'/>
            </Switch>
        </Router>
      </div>
    );
  }
}

export default App;