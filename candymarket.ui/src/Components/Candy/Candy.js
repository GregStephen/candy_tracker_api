import React from 'react';
import './Candy.scss';

class Candy extends React.Component {
    eatCandy = () => {
        const { candy, eatTheCandy } = this.props;
        eatTheCandy(candy.userCandyId);
    };

    donateCandy = () => {
        const { candy, donateTheCandy } = this.props;
        donateTheCandy(candy.userCandyId);
    };

    tradeUpCandy = () => {
        const {candy, putTheCandyUpForTrade } = this.props;
        putTheCandyUpForTrade(candy.userCandyId);
    }
    
    unTradeUpCandy = () => {
        const {candy, unPutTheCandyUpForTrade} = this.props;
        unPutTheCandyUpForTrade(candy.userCandyId);
    }

    render() {
        const {candy} = this.props;
        return (
            <div className='Candy col-4'>
                <div className={ candy.isUpForTrade ? 'card up-for-trade' : 'card' }>
                    <div className='card-header'>
                        {candy.name}
                    </div>
                    <img src={candy.imgUrl} className="card-img-top" alt={candy.name}></img>
                    <div className='card-body'>
                        <ul className='list-group list-group-flush'>
                            <li className='list-group-item'>Type of candy: {candy.type}</li>
                            <li className='list-group-item'>Size: {candy.size}</li>
                        </ul>
                        <div className='buttons row justify-content-around'>
                            <button className='btn btn-danger col-3' onClick={this.eatCandy}>Eat</button>
                            <button className='btn btn-success col-3'onClick={this.donateCandy}>Donate</button>
                            { candy.isUpForTrade ? <button className='btn btn-danger col-3' onClick={this.unTradeUpCandy}>Remove From Trade</button> : 
                                <button className='btn btn-info col-3' onClick={this.tradeUpCandy}>Put Up For Trade</button> }
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default Candy;
