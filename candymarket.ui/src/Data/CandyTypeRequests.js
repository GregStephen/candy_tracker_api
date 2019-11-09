import axios from 'axios';

const baseUrl = 'https://localhost:44337'
const getAllCandyTypes = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/type`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

export default {getAllCandyTypes};