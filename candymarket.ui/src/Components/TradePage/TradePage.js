import React from 'react';
import TradeRequests from '../../Data/TradeRequests';
import UserRequests from '../../Data/UserRequests';
import CandyRequests from '../../Data/CandyRequests';

class TradePage extends React.Component {
    state = {
        trades : []
    }

    componentDidMount() {
        TradeRequests.getAllTrades()
            .then((results) => {
                console.error(results);
                results.forEach(result  => {
                    UserRequests.getUserFromUserCandy(result.userCandyId)
                    .then((user) => {
                        result.user = user;
                    });
                })
                results.forEach(result => {
                   CandyRequests.getCandyFromUserCandy(result.userCandyId)
                    .then((candy) => {
                        result.candy = candy;
                    });
                })
                this.setState({ trades : results })
                })
            .catch(err => console.error(err));
    }
    render() {
        return (
            <div className='TradePage'>
                <h1>It's the god damn trade page mother fucker</h1>
            </div>
        )
    }
}

export default TradePage;
