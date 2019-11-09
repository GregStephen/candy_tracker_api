import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Candy from '../Candy/Candy';

import './Home.scss';

class Home extends Component {

eatTheCandy = (candyId) => {
  const { candyAte } = this.props;
  candyAte(candyId);
}

donateTheCandy = (candyId) => {
  const { candyDonated } = this.props;
  candyDonated(candyId);
}
componentDidMount() {

}
render () {
  const { userObj } = this.props;
  const showUsersCandy = userObj.candyOwned.map(candy => (
    <Candy
    key={ candy.id }
    candy={ candy }
    eatTheCandy={ this.eatTheCandy }
    donateTheCandy={ this.donateTheCandy }
    />
  ));
  return (
    <div className="Home">
      <h1>Welcome {userObj.firstName}!</h1>
      <Link to={'/candy-list'} className="btn btn-danger">Buy Some Candy</Link>
      <h3>Amount of candy donated: {userObj.amountOfCandyDonated}</h3>
      <h3>Amount of candy eaten: {userObj.amountOfCandyEaten}</h3>
      <h3>{userObj.amountOfCandyEaten <= 5 ? 'Get to munchin' : 
           userObj.amountOfCandyEaten <= 25 ? 'There ya go chubby' :
           'You have eaten a lot of fucking candy.'}</h3>
      <h2> Here's your list of candy that you own!</h2>
      <h3>Amount of Candy Owned : {userObj.candyOwned.length}</h3>
        {showUsersCandy}
    </div>
    );
  }
}

export default Home;