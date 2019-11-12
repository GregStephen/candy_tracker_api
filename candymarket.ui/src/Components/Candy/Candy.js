import React from 'react';

class Candy extends React.Component {
    eatCandy = () => {
        const { candy, eatTheCandy } = this.props;
        eatTheCandy(candy.userCandyId);
    };

    donateCandy = () => {
        const { candy, donateTheCandy } = this.props;
        donateTheCandy(candy.userCandyId);
    };

    tradeCandy = () => {

    }
    render() {
        const {candy} = this.props;
        return (
            <div className='Candy col-4'>
                <div className='card'>
                    <div className='card-header'>
                        {candy.name}, {candy.userCandyId}
                    </div>
                    <img src={candy.imgUrl} className="card-img-top" alt={candy.name}></img>
                    <div className='card-body'>
                        <ul className='list-group list-group-flush'>
                            <li className='list-group-item'>{candy.size}</li>
                        </ul>
                        <div className='buttons row justify-content-around'>
                            <button className='btn btn-danger col-3' onClick={this.eatCandy}>Eat</button>
                            <button className='btn btn-success col-3'onClick={this.donateCandy}>Donate</button>
                            <button className='btn btn-info col-3' onClick={this.tradeCandy}>Trade</button>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default Candy;
