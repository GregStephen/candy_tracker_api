import React, { Component } from 'react';
import CandyList from '../CandyList/CandyList';
import './Home.scss';

class Home extends Component {

  render () {
    const testText = this.props.testText;
    return (
      <div className="Home">
        <button className="btn btn-danger">Buy Some Candy</button>
        <button className="btn btn-danger">Donate Some Candy</button>
        <button className="btn btn-danger">Eat Some Candy</button>
          <CandyList/>
          <h1 className="testTarget">{testText}</h1>
      </div>
    );
  }
}

export default Home;