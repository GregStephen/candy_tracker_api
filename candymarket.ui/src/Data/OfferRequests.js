import axios from 'axios';

const baseUrl = 'https://localhost:44337/offer/'

const addOffer = offer => axios.post(`${baseUrl}`, offer);

export default { addOffer };
