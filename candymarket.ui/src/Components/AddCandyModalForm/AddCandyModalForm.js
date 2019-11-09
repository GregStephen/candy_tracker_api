import React from 'react';
import {
  Form, FormGroup, Label, Input, ModalBody, ModalFooter, Button,
} from 'reactstrap';
import CandyTypeRequests from '../../Data/CandyTypeRequests';

class AddCandyModalForm extends React.Component {

  state = {
    newCandy: {
      name: '',
      typeId: 0,
      size: '',
      imgUrl: '',
    },
    candyType: [],
  }

  componentDidMount() {
    CandyTypeRequests.getAllCandyTypes()      
        .then(candyType => this.setState({ candyType }))
      .catch(err => console.error('trouble getting candy types', err));
  }

  toggleModal = () => {
    const { toggleAddCandy } = this.props;
    toggleAddCandy();
  }

  formFieldStringState = (e) => {
    const tempCandy = { ...this.state.newCandy };
    tempCandy[e.target.id] = e.target.value;
    this.setState({ newCandy: tempCandy });
  }

  toIntSelector = (e) => {
    const tempCandy = { ...this.state.newCandy };
    tempCandy[e.target.id] = parseInt(e.target.value, 10);
    this.setState({ newCandy : tempCandy });
  }

  handleSubmit = (e) => {
    e.preventDefault();
    const { addCandy} = this.props
    const { newCandy } = this.state;
    addCandy(newCandy);
    this.toggleModal();
  }

  candyTypeList = () => {
    const { candyType } = this.state;
    const options = candyType.map(candy => (
      <option key={candy.id} value={candy.id}>{candy.name}</option>
    ));
    options.unshift(<option key='pick not' value="">Type</option>);
    return options;
  }


  render() {
    const { newCandy } = this.state;
    return (
      <div>
        <Form onSubmit={this.handleSubmit}>
          <ModalBody>
            <FormGroup>
              <Label for="name">Candy Name:</Label>
              <Input type="input" name="name" id="name" value={newCandy.name} onChange={this.formFieldStringState} required/>
            </FormGroup>
            <FormGroup>
              <Label for="typeId">Candy Type:</Label>
              <Input
              type="select"
              className="form-control"
              id="typeId"
              value={newCandy.typeId}
              onChange={this.toIntSelector}
              required
              >
              {this.candyTypeList()}
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="imgUrl">Image URL:</Label>
              <Input type="url" name="imgUrl" id="imgUrl" value={newCandy.imgUrl} onChange={this.formFieldStringState} required/>
            </FormGroup>
            <FormGroup>
              <Label for="size">Size of Candy:</Label>
              <Input type="input" name="size" id="size" value={newCandy.size} onChange={this.formFieldStringState} required/>
            </FormGroup>
          </ModalBody>
          <ModalFooter>
         <Button type="submit" color="primary">Add Candy!</Button>{' '}
         <Button color="secondary" onClick={this.toggleModal}>Cancel</Button>
       </ModalFooter>
        </Form>
      </div>
    );
  }
}

export default AddCandyModalForm;