import React from 'react'

class OfferIn extends React.Component{
    render() {
        const  { offer } = this.props;
        const approveOffer = () => {
            const { offerApproved } = this.props;
            const requestedId = offer.requestedUserCandyId;
            const offeredId = offer.offeredUserCandyId;
            offerApproved(requestedId, offeredId);
        };

        const denyOffer = () => {
            const {offerCanceled} = this.props;
            offerCanceled(offer.id);
        };

        return (
            <div className="OfferIn col-3">
                <div className="card">
                    <p>Offer for your candy</p>
                    <p>{ offer.message }</p>
                    <button className="btn btn-success" onClick={approveOffer}>Approve Offer</button>
                    <button className="btn btn-danger" onClick={denyOffer}>Deny Offer</button>
                </div>
                
            </div>
        )
    }
}

export default OfferIn;