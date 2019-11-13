import React from 'react';
import {
  Form, FormGroup, Label, Input, ModalBody, ModalFooter, Button,
} from 'reactstrap';
import CandyTypeRequests from '../../Data/CandyTypeRequests';

class ConfirmTradeModalForm extends React.Component {

  state = {
    newCandyTrade: {
      userCandyId1: this.props.candyWanted.userCandyId,
      userCandyId2: '',
      message: '',
    },
    candyToTrade: [],
    candyWanted: this.props.candyWanted
  }

  componentDidMount() {
    // need to get the candy to trade that the user has put up
    CandyTypeRequests.getAllCandyTypes()      
        .then(candyType => this.setState({ candyType }))
      .catch(err => console.error('trouble getting candy types', err));
  }

  toggleModal = () => {
    const { toggleAddCandyTrade } = this.props;
    toggleAddCandyTrade();
  }

  formFieldStringState = (e) => {
    const tempCandyTrade = { ...this.state.newCandyTrade };
    tempCandyTrade[e.target.id] = e.target.value;
    this.setState({ newCandyTrade: tempCandyTrade });
  }


  handleSubmit = (e) => {
    e.preventDefault();
    const { addCandyTrade } = this.props
    const { newCandyTrade } = this.state;
    addCandyTrade(newCandyTrade);
    this.toggleModal();
  }

  candyToTradeList = () => {
    const { candyToTrade } = this.state;
    const options = candyToTrade.map(candyTrade => (
      <option key={candyTrade.userCandyId} value={candyTrade.userCandyId}>{candyTrade.candyName}</option>
    ));
    options.unshift(<option key='pick not' value="">Candy To Trade</option>);
    return options;
  }


  render() {
    const { newCandyTrade } = this.state;
    return (
      <div>
        <Form onSubmit={this.handleSubmit}>
          <ModalBody>
          <div className='Trade col-4'>
                <div className='card-header'>
                    {newCandyTrade.candyName}
                </div>
                <img src={trade.imgUrl} className="card-img-top" alt={trade.candyName}></img>
                <ul className='list-group list-group-flush'>
                    <li className='list-group-item'>Offered by: {trade.firstName} {trade.lastName}</li>
                    <li className='list-group-item'>Size: { trade.size }</li>
                </ul>
                <div className='trade-btn'>
                    {userObj.id === trade.userId ? <button className='btn btn-danger' onClick={this.removeFromTrade}>Remove from Trade</button> :
                    <button className='btn btn-success' onClick={this.offerTrade}>Offer a trade</button>}
                </div>
            </div>
            <FormGroup>
              <Label for="userCandyId2">Candy You Want To Trade:</Label>
              <Input
              type="select"
              className="form-control"
              id="userCandyId2"
              value={newCandyTrade.userCandyId2}
              onChange={this.formFieldStringState}
              required
              >
              {this.candyToTradeList()}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="message">Message to user:</Label>
              <Input type="input" name="message" id="message" value={newCandyTrade.message} onChange={this.formFieldStringState}/>
            </FormGroup>
          </ModalBody>
          <ModalFooter>
         <Button type="submit" color="primary">Offer Trade!</Button>{' '}
         <Button color="secondary" onClick={this.toggleModal}>Cancel</Button>
       </ModalFooter>
        </Form>
      </div>
    );
  }
}

export default ConfirmTradeModalForm;