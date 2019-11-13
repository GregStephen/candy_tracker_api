import React from 'react';

class Trade extends React.Component {

    removeFromTrade = () => {
        const { takeOffTrade, trade } = this.props;
        takeOffTrade(trade.userCandyId);
    }

    offerTrade = () => {
        const { trade, tradeOffered, userObj } = this.props;
        tradeOffered(trade.userCandyId, userObj.id);
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
                    <button className='btn btn-success' onClick={this.offerTrade}>Offer a trade</button>}
                </div>
            </div>
        )
    }
}

export default Trade;
