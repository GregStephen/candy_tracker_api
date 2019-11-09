import React from 'react';

class Candy extends React.Component {
    render() {
        const {candy} = this.props;
        return (
            <div className='Candy col-4'>
                <div className='card'>
                <h1>{candy.name}</h1>
                <button className='btn btn-danger'>Eat</button>
                <button className='btn btn-success'>Donate</button>
                <button className='btn btn-info'>Trade</button>
                </div>
            </div>
        )
    }
}

export default Candy;
