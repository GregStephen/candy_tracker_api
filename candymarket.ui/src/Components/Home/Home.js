import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Candy from '../Candy/Candy';

import './Home.scss';

class Home extends Component {

eatTheCandy = (userCandyId) => {
  const { candyAte } = this.props;
  candyAte(userCandyId);
}

donateTheCandy = (userCandyId) => {
  const { candyDonated } = this.props;
  candyDonated(userCandyId);
}

putTheCandyUpForTrade = (userCandyId) => {
  const { candyUpForTrade } = this.props;
  candyUpForTrade(userCandyId);
}

unPutTheCandyUpForTrade = (userCandyId) => {
  const { candyNotUpForTrade } = this.props;
  candyNotUpForTrade(userCandyId);
}
componentDidMount() {

}

render () {
  const { userObj } = this.props;
  const showUsersCandy = userObj.candyOwned.map(candy => (
    <Candy
    key={ candy.userCandyId }
    candy={ candy }
    eatTheCandy={ this.eatTheCandy }
    donateTheCandy={ this.donateTheCandy }
    putTheCandyUpForTrade={ this.putTheCandyUpForTrade }
    unPutTheCandyUpForTrade={ this.unPutTheCandyUpForTrade }
    />
  ));

  return (
    <div className="Home">
      <h1>Welcome {userObj.firstName}!</h1>
      <Link to={'/candy-list'} className="btn btn-danger">Buy Some Candy</Link>
      <h5>Amount of candy donated: {userObj.amountOfCandyDonated}</h5>
      <h5>Amount of candy eaten: {userObj.amountOfCandyEaten}</h5>
      <p>{userObj.amountOfCandyEaten <= 5 ? 'Get to munchin' : 
           userObj.amountOfCandyEaten <= 25 ? 'There ya go chubby' :
           'You have eaten a lot of fucking candy.'}</p>
      <h2> Here's your list of candy that you own!</h2>
      <h3>Amount of Candy Owned : {userObj.candyOwned.length}</h3>
      <div className="container">
        <div className="row">
          { showUsersCandy }
        </div>
        
      </div>
        
    </div>
    );
  }
}

export default Home;