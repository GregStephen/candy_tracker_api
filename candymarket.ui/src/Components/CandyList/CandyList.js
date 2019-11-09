import React from 'react';
import {
    Modal, ModalHeader,
  } from 'reactstrap';
import candyRequests from '../../Data/CandyRequests';
import AddCandyModalForm from '../AddCandyModalForm/AddCandyModalForm';
import CandyRequests from '../../Data/CandyRequests';

class CandyList extends React.Component {
    state = {
        candyList: [],
        addCandyModal : false,
        collapse: false,
    }


    toggleAddCandy = () => {
        this.setState(prevState => ({
          addCandyModal: !prevState.addCandyModal,
        }));
    }
    loadPage = () => {
        candyRequests.getAllCandy()
            .then(data => {
                this.setState({ candyList : data });
            })
            .catch(err => console.log(err));
    }
    addCandy = (candyObj) => {
        console.error(candyObj);
        CandyRequests.addCandy(candyObj)
            .then(() => {
                this.loadPage();
            })
            .catch();
    }

    componentDidMount() {
        this.loadPage();
    }


    buildCandies = () => this.state.candyList.map(c => (
        <h2>{c.name}</h2>
    ));

    render() {
        return (
            <div className='CandyList'>
                <h1>Here's a list of all the Candy we carry!</h1>
                <button className='btn btn-success' onClick={this.toggleAddCandy}>Add More</button>
                {this.buildCandies()}
                <div>
                    <Modal isOpen={this.state.addCandyModal} toggle={this.toggleModal}>
                        <ModalHeader toggle={this.toggleAddCandy}>Add Candy!</ModalHeader>
                        <AddCandyModalForm
                        toggleAddCandy={this.toggleAddCandy}
                        addCandy={this.addCandy}
                        />
                    </Modal>
                </div>
            </div>
        )
    }
}

export default CandyList;
