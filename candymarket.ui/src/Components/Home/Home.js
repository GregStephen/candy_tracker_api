import React, { Component } from 'react';
import CandyList from '../CandyList/CandyList';
import './Home.scss';

class Home extends Component {

  render () {
    const userObj = this.props.userObj;
    return (
      <div className="Home">
        <h1>Welcome {userObj.firstName}!</h1>
        <button className="btn btn-danger">Buy Some Candy</button>
        <button className="btn btn-danger">Donate Some Candy</button>
        <button className="btn btn-danger">Eat Some Candy</button>
          <CandyList/>
      </div>
    );
  }
}

export default Home;