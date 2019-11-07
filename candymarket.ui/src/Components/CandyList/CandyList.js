import React from 'react';
import candyRequests from '../../Data/CandyRequests';

class CandyList extends React.Component {
    state = {
        candyList: []
    }

    componentDidMount() {
        candyRequests.getAllCandy()
            .then(data => {
                this.setState({ candyList : data });
            })
            .catch(err => console.log(err));
    }
    buildCandies = () => this.state.candyList.map(c => (
        <h2>{c.name}</h2>
    ));

    render() {
        return (
            <div className='CandyList'>
                {this.buildCandies()}
            </div>
        )
    }
}

export default CandyList;
