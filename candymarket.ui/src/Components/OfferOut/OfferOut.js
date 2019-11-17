import React from 'react'

class OfferOut extends React.Component{
    render() {
        const  { offer } = this.props;

        const cancelOffer = () => {
            const { offerCanceled } = this.props;
            offerCanceled(offer.id)
        };
        
        return (
            <div className="OfferOut col-3">
                <div className="card">
                    <p>Offer Pending</p>
                    <p>{ offer.message }</p>
                    <button className="btn btn-danger" onClick={cancelOffer}>Cancel Offer</button>
                </div>     
            </div>
        )
    }
}

export default OfferOut;