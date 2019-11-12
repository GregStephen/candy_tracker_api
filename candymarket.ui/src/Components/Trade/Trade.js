import React from 'react';

class Trade extends React.Component {

    removeFromTrade = () => {
        const { takeOffTrade, trade } = this.props;
        takeOffTrade(trade.userCandyId);
    }
    render() {
        const { trade, userObj } = this.props;
        return (
            <div className='Trade col-4'>
                <div className='card'>
                    <h1>{trade.candyName}</h1>
                    <h1>{trade.firstName} {trade.lastName}</h1>
                    <div className='trade-btn'>
                        {userObj.id === trade.userId ? <button className='btn btn-danger' onClick={this.removeFromTrade}>Remove from Trade</button> :
                        <button className='btn btn-success'>Offer a trade</button>}
                    </div>
                </div>
            </div>
        )
    }
}

export default Trade;
