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

const logInUser = (email, password) => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/user/${email}/p/${password}`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

const getUserFromUserCandy = (userCandyId) => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/user/user-candy/${userCandyId}`)
    .then(result => resolve(result.data))
    .catch(err => reject(err));
});

const postUser = userObj => axios.post(`${baseUrl}/user/`, userObj);

const buyCandy = (userId, candyId) => axios.post(`${baseUrl}/user/${userId}/buy/${candyId}`);

const eatCandy = (userId, userCandyId) => axios.delete(`${baseUrl}/user/${userId}/eat/${userCandyId}`);

const donateCandy = (userId, userCandyId) => axios.delete(`${baseUrl}/user/${userId}/donate/${userCandyId}`);

const putCandyUpForTrade = (userCandyId) => axios.put(`${baseUrl}/user/up-for-trade/${userCandyId}`);

const takeCandyOffTrade = (userCandyId) => axios.put(`${baseUrl}/user/off-trade/${userCandyId}`);

const tradeCandies = (userCandyId1, userCandyId2) => axios.post(`${baseUrl}/user/${userCandyId1}/trades/${userCandyId2}`)

export default {
    getAllUsers,
    getUserById,
    postUser,
    logInUser,
    buyCandy,
    eatCandy,
    donateCandy,
    getUserFromUserCandy,
    putCandyUpForTrade,
    takeCandyOffTrade,
    tradeCandies
};