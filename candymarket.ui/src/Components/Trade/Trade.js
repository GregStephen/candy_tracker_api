import React from 'react';

class Trade extends React.Component {
    render() {
        const { trade } = this.props;
        return (
            <div className='Trade col-4'>
                <div className='card'>
                    <h1>{trade.candyName}</h1>
                    <h1>{trade.firstName} {trade.lastName}</h1>
                </div>
            </div>
        )
    }
}

export default Trade;
