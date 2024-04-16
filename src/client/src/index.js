import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
// import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import { loginSuccess } from './redux/Account';

import store from './redux/Store';
const user = JSON.parse(localStorage.getItem("user"));
if (user) {
  // Nếu token đã được lưu trữ trong Local Storage
  store.dispatch(loginSuccess(user)); // Sử dụng action loginSuccess để khôi phục trạng thái đăng nhập
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />

  </React.StrictMode>
);


