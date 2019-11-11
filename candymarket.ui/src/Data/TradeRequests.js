import axios from 'axios';

const baseUrl = 'https://localhost:44337'

const getAllTrades = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/trade`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

const addToTrade = (userCandyId) => axios.post(`${baseUrl}/trade`, userCandyId)

export default { getAllTrades, addToTrade };