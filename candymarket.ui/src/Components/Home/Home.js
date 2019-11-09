import React, { Component } from 'react';
import Candy from '../Candy/Candy';

import './Home.scss';

class Home extends Component {

  componentDidMount() {

  }
  render () {
    const userObj = this.props.userObj;
    const showUsersCandy = userObj.candyOwned.map(candy => (
      <Candy
      key={ candy.id }
      candy={ candy }
      />
    ));
    return (
      <div className="Home">
        <h1>Welcome {userObj.firstName}!</h1>
        <button className="btn btn-danger">Buy Some Candy</button>
        <h2> Here's your list of candy that you own!</h2>
          {showUsersCandy}
      </div>
    );
  }
}

export default Home;