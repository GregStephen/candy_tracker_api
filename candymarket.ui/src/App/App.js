import React from 'react';
import {
  BrowserRouter as Router, Route, Redirect, Switch,
} from 'react-router-dom';
import MyNavbar from '../components/MyNavbar/MyNavbar';
import Auth from '../components/Auth/Auth';
import Home from '../Components/Home/Home';
import firebase from 'firebase/app';
import 'firebase/auth';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.scss';
import fbConnect from '../helpers/data/fbConnection';

fbConnect();

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

class App extends React.Component {
  state = {
    authed: false,
    userObj: {
      name: 'Greg',
    }
  };

  getUser = () => {
    if (this.state.authed) {
      const firebaseId = firebase.auth().currentUser.uid;
      userData.getUserByUID(firebaseId)
        .then(userObj => this.setState({ userObj }))
        .catch(err => console.error('trouble fetching user data', err));
    }
  }

  createUser = (saveMe) => {
    userData.postUser(saveMe)
      .then(() => {
        this.getUser();
      })
      .catch();
  }

  componentDidMount() {
    this.removeListener = firebase.auth().onAuthStateChanged((user) => {
      if (user) {
        userData.getUserByID(user.uid)
          .then((userObj) => {
            this.setState({ userObj });
            this.setState({ authed: true });
          })
          .catch(err => console.error('trouble fetching user data', err));
      } else {
        this.setState({ authed: false });
      }
    });
  }

  componentWillUnmount() {
    this.removeListener();
  }


  render() {
    const { authed, userObj } = this.state;
    return (
      <div className="App">
        <Router>
          <MyNavbar authed={ authed } userObj={ userObj } getUser={ this.getUser }/>
            <Switch>
              <PublicRoute path='/auth' component={Auth} authed={authed}/>
              <PrivateRoute path="/" exact component={ Home } authed={ authed } userObj={ userObj }/>
              <PrivateRoute path='/user/:id' component={ User } authed={ authed } userObj={ userObj }/>
              <Redirect from='*' to='/auth'/>
            </Switch>
        </Router>
      </div>
    );
  }
}

export default App;