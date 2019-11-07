import axios from 'axios';

const baseUrl = 'https://localhost:44337'
const getAllCandy = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/candy`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

export default {getAllCandy};