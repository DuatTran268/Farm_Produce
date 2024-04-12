// userDataProvider.js

import axios from 'axios';

const API_URL = 'https://localhost:7047/api/oderstatus';

const getUserList = async () => {
  const response = await axios.get(`https://localhost:7047/api/oderstatus/getall`);
  return response.data;
};

const createUser = async (userData) => {
  const response = await axios.post(`${API_URL}/users`, userData);
  return response.data;
};

const updateUser = async (userId, userData) => {
  const response = await axios.put(`${API_URL}/users/${userId}`, userData);
  return response.data;
};

const deleteUser = async (userId) => {
  const response = await axios.delete(`${API_URL}/users/${userId}`);
  return response.data;
};

export default {
  getList: getUserList,
  create: createUser,
  update: updateUser,
  delete: deleteUser,
};
