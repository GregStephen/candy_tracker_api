import axios from 'axios';

const baseUrl = 'https://localhost:44337'
const getAllCandy = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/candy`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

const addCandy = candyObj => axios.post(`${baseUrl}/candy`, candyObj);


export default {getAllCandy, addCandy};
