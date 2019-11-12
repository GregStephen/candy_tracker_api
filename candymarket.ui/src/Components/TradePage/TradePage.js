import React from 'react';
import UserRequests from '../../Data/UserRequests';
import Trade from '../../Components/Trade/Trade';

class TradePage extends React.Component {
    state = {
        trades : []
    }

    componentDidMount() {
        UserRequests.getAllTrades()
            .then((results) => {
                this.setState({ trades : results })
                })
            .catch(err => console.error(err));
    }
    render() {
        const {trades} = this.state;
        const showTrades = trades.map(trade => (
            <Trade
            key={ trade.userCandyId }
            trade={ trade }
            />
          ));
        return (
            <div className='TradePage container'>
                <h1>It's the god damn trade page mother fucker</h1>
                <div className='row'>
                    {showTrades}
                </div>
            </div>
        )
    }
}

export default TradePage;
