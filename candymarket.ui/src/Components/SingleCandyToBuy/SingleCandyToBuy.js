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
            <div className='Candy col-4'>
                <div className='card'>
                    <div className='card-header'>
                        {candy.name}
                    </div>
                    <img src={candy.imgUrl} class="card-img-top" alt={candy.name}></img>
                    <div className='card-body'>
                        <ul className='list-group list-group-flush'>
            
                            <li className='list-group-item'>{candy.size}</li>
                        </ul>
                        <div className='buttons row justify-content-around'>
                            <button className='btn btn-danger col-3' onClick={this.buyThisCandy}>Buy</button>
                            <p>{msg}</p>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default SingleCandyToBuy;