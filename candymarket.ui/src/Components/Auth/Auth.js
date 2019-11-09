import React from 'react';
import { Link } from 'react-router-dom';
import './Auth.scss';
import UserRequests from '../../Data/UserRequests';

class Auth extends React.Component {
    state = {
        email: '',
        password: '',
        error: '',
    }

    handleChange = (e) => {
        this.setState({
            [e.target.id]: e.target.value,
        });
    };

    logIntoCandyMarket = (e) => {
        e.preventDefault();
        const { email, password } = this.state;
        UserRequests.logInUser(email,password)
        .then((request) => {
            this.props.userLoggedIn(request);
        })
        .catch(err => this.setState({ error: err.message }));
    }

  render() {
    const { email, password, error } = this.state;
    return ( 
        <div className="Auth container">
        <div className="row justify-content-center">
          <div className="col-12 col-lg-8 welcome-div row justify-content-center">
            <h1 className="welcome-header col-12">Welcome to Chubby's Candy Market!</h1>
            <h4 className="col-12 col-md-8">Need to show off your candy collection?</h4>
            <h4 className="bold col-12 col-md-8">WE GOT YA COVERED</h4>
            <h4 className="col-12 col-md-8">Want to donate or trade your candy?</h4>
            <h4 className="bold col-12 col-md-8">WE GOT YA COVERED</h4>
            <h4 className="col-12 col-md-8">Excited to get to it?</h4>
            <Link className="btn btn-info col-8" to={'/new-user'}>Create an Account!</Link>
          </div>
          <form className="col-10 col-lg-4 container sign-in-form" onSubmit={this.logIntoCandyMarket}>
            <h3 className="sign-in-header">Already Have An Account?</h3>
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input
              type="email"
              className="form-control"
              id="email"
              value={email}
              onChange={this.handleChange}
              placeholder="John@CandyMarket.com"
              required
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input
              type="password"
              className="form-control"
              id="password"
              value={password}
              onChange={this.handleChange}
              required
              />
            </div>
            <button type="submit" className="btn btn-success">Log In</button>
            <p className="error">{error}</p>
          </form>
        </div>
      </div>
    );
  }
}

export default Auth;