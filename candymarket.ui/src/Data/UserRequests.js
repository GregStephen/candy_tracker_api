import axios from 'axios';

const baseUrl = 'https://localhost:44337'
const getAllUsers = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/user`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

const getUserById = uid => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/user/${uid}`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

const postUser = userObj => axios.post(`${baseUrl}/user/`, userObj);



export default {getAllUsers, getUserById, postUser};