import React from 'react';
import {
  Form, FormGroup, Label, Input, ModalBody, ModalFooter, Button,
} from 'reactstrap';
import CandyTypeRequests from '../../Data/CandyTypeRequests';

class ConfirmOfferModalForm extends React.Component {

  state = {
    newCandyOffer: {
      Offered: '',
      Requested: this.props.candyWanted.userCandyId,
      message: '',
    },
    candyToTrade: [],
    candyWanted: this.props.candyWanted
  }

  componentDidMount() {
    const { userObj } = this.props;
    console.error(userObj)
    const candyToTrade = userObj.candyOwned.filter(candy => candy.isUpForTrade === true);
    this.setState({ candyToTrade })
    CandyTypeRequests.getAllCandyTypes()      
        .then(candyType => this.setState({ candyType }))
      .catch(err => console.error('trouble getting candy types', err));
  }

  toggleModal = () => {
    const { toggleConfirmOffer } = this.props;
    toggleConfirmOffer();
  }

  formFieldStringState = (e) => {
    const tempCandyOffer = { ...this.state.newCandyOffer };
    tempCandyOffer[e.target.id] = e.target.value;
    this.setState({ newCandyOffer : tempCandyOffer });
  }


  handleSubmit = (e) => {
    e.preventDefault();
    const { addCandyOffer } = this.props
    const { newCandyOffer } = this.state;
    addCandyOffer(newCandyOffer);
    this.toggleModal();
  }

  candyToTradeList = () => {
    const { candyToTrade } = this.state;
    const options = candyToTrade.map(candyTrade => (
      <option key={candyTrade.userCandyId} value={candyTrade.userCandyId}>{candyTrade.name}</option>
    ));
    options.unshift(<option key='pick not' value="">Candy To Trade</option>);
    return options;
  }


  render() {
    const { newCandyOffer, candyWanted } = this.state;
    return (
      <div>
        <Form onSubmit={this.handleSubmit}>
          <ModalBody>
          <div className='Trade col-4'>
                <div className='card-header'>
                    {candyWanted.candyName}
                </div>
                <img src={candyWanted.imgUrl} className="card-img-top" alt={candyWanted.candyName}></img>
                <ul className='list-group list-group-flush'>
                    <li className='list-group-item'>Offered by: {candyWanted.firstName} {candyWanted.lastName}</li>
                    <li className='list-group-item'>Size: { candyWanted.size }</li>
                </ul>
            </div>
            <FormGroup>
              <Label for="Offered">Candy You Want To Trade:</Label>
              <Input
              type="select"
              className="form-control"
              id="Offered"
              value={newCandyOffer.Offered}
              onChange={this.formFieldStringState}
              required
              >
              {this.candyToTradeList()}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="message">Message to user:</Label>
              <Input type="input" name="message" id="message" value={newCandyOffer.message} onChange={this.formFieldStringState}/>
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

export default ConfirmOfferModalForm;