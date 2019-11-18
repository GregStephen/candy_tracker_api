import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import Candy from '../Candy/Candy';
import OfferIn from '../OfferIn/OfferIn';
import OfferOut from '../OfferOut/OfferOut';

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

offerCanceled = (offerId) => {
  const { cancelTheOffer } = this.props;
  cancelTheOffer(offerId);
};

offerApproved = (userCandyId1, userCandyId2) => {
  const { approveTheOffer } = this.props;
  approveTheOffer(userCandyId1, userCandyId2);
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

  const showUsersOffersOut = userObj.offersOut.map(offer => (
    <OfferOut
    key={ offer.id }
    offer={ offer }
    offerCanceled= { this.offerCanceled }
    />
  ));

  const showUsersOffersIn = userObj.offersIn.map(offer => (
    <OfferIn 
    key={ offer.id }
    offer= { offer }
    offerCanceled= { this.offerCanceled }
    offerApproved= { this.offerApproved }
    />
  ))

  return (
    <div className='Home container'>
      <div className='row'>
        <div className='col-12 col-sm-10 col-md-6 col-lg-4'>
          <h1>Welcome {userObj.firstName}!</h1>
          <Link to={'/candy-list'} className="btn btn-danger">Buy Some Candy</Link>
        </div>
        <div className='col-6 col-lg-4'>
          <h5>Amount of candy donated: {userObj.amountOfCandyDonated}</h5>
        </div>
        <div className='col-6 col-lg-4'>
          <h5>Amount of candy eaten: {userObj.amountOfCandyEaten}</h5>
          <p>{userObj.amountOfCandyEaten <= 5 ? 'Get to munchin' : 
            userObj.amountOfCandyEaten <= 25 ? 'There ya go chubby' :
            'You have eaten a lot of fucking candy.'}</p>
        </div>
      </div>
      <div className='row'>
        <div className='col-12 justify-content-around'>
          <h3 className='col-12 col-md-8'> Here's your list of candy that you own!</h3>
          <p className='col-12 col-md-8'>Amount of Candy Owned : {userObj.candyOwned.length}</p>
        </div>
      </div>
      <div className="row userOffersOut">
        { showUsersOffersOut }
      </div>
      <div className="row userOffersIn">
        { showUsersOffersIn }
      </div>
      <div className="row userCandies">
        { showUsersCandy }
      </div>  
    </div>
    );
  }
}

export default Home;