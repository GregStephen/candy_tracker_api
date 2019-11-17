import axios from 'axios';

const baseUrl = 'https://localhost:44337/offer/'

const addOffer = offer => axios.post(`${baseUrl}`, offer);

const removeOffer = offerId => axios.delete(`${baseUrl}${offerId}`);

export default { addOffer, removeOffer };
