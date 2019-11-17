import React from 'react'

class OfferIn extends React.Component{
    render() {
        const  { offer } = this.props;
        return (
            <div className="OfferIn col-3">
                <div className="card">
                    <p>Offer for your candy</p>
                    <p>{ offer.message }</p>
                </div>
                
            </div>
        )
    }
}

export default OfferIn;