import React from 'react';
import {
    Modal, ModalHeader,
  } from 'reactstrap';
import ConfirmOfferModalForm from '../ConfirmOfferModalForm/ConfirmOfferModalForm';

class Trade extends React.Component {
    state ={
       confirmOfferModal: false,
        collapse: false,
    }

    toggleConfirmOffer = () => {
        this.setState(prevState => ({
          confirmOfferModal: !prevState.confirmOfferModal,
        }));
    }
    removeFromTrade = () => {
        const { takeOffTrade, trade } = this.props;
        takeOffTrade(trade.userCandyId);
    }

    addCandyOffer = (offer) => {
        const {tradeOffered} = this.props;
        tradeOffered(offer);
    }

    render() {
        const { trade, userObj } = this.props;
        return (
            <div className='Trade col-4'>
                <div className='card-header'>
                    {trade.candyName}
                </div>
                <img src={trade.imgUrl} className="card-img-top" alt={trade.candyName}></img>
                <ul className='list-group list-group-flush'>
                    <li className='list-group-item'>Offered by: {trade.firstName} {trade.lastName}</li>
                    <li className='list-group-item'>Size: { trade.size }</li>
                </ul>
                <div className='trade-btn'>
                    {userObj.id === trade.userId ? <button className='btn btn-danger' onClick={this.removeFromTrade}>Remove from Trade</button> :
                    <button className='btn btn-success' onClick={this.toggleConfirmOffer}>Offer a trade</button>}
                </div>
                <div>
                    <Modal isOpen={this.state.confirmOfferModal} toggle={this.toggleModal}>
                        <ModalHeader toggle={this.toggleConfirmOffer}>Offer Up!</ModalHeader>
                        <ConfirmOfferModalForm
                        toggleConfirmOffer={this.toggleConfirmOffer}
                        addCandyOffer={this.addCandyOffer}
                        candyWanted={trade}
                        userObj={userObj}
                        />
                    </Modal>
                </div>
            </div>
        )
    }
}

export default Trade;
