import React from 'react';

class SingleCandyToBuy extends React.Component {
    state = { msg : ''}
    buyThisCandy = () => {
        const {candy, buyCandy} = this.props;
        this.setState({ msg: 'Hey thanks for buying one!'})
        buyCandy(candy.id);
    };

    render() {
        const {candy} = this.props;
        const {msg} = this.state;
        return (
            <div className='Candy col-xs-12 col-sm-6 col-md-4'>
                <div className='card'>
                    <div className='card-header'>
                        {candy.name}
                    </div>
                    <img src={candy.imgUrl} className="card-img-top" alt={candy.name}></img>
                    <div className='card-body'>
                        <ul className='list-group list-group-flush'>
                            <li className='list-group-item'>Type of Candy: {candy.type}</li>
                            <li className='list-group-item'>Size: {candy.size}</li>
                        </ul>
                        <div className='buttons row justify-content-center'>
                            <button className='btn btn-danger col-8' onClick={this.buyThisCandy}>Buy</button>
                        </div>
                        <p>{msg}</p>
                    </div>
                </div>
            </div>
        )
    }
}

export default SingleCandyToBuy;