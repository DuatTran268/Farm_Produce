import axios from "axios";
import {
  loginStart,
  loginSuccess,
  loginFail,
  registerStart,
  registerSuccess,
  registerFail,
} from "../redux/Account";
import { jwtDecode } from "jwt-decode";
const decodeAndSaveUserInfo = (token) => {
  const userInfo = jwtDecode(token);
  const user = {
    id: userInfo['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
    username: userInfo['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
    email: userInfo['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
    role: userInfo['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
  };
  localStorage.setItem("token", token);
  localStorage.setItem("user", JSON.stringify(user));
  return user
};
export const LoginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    
    const response = await axios.post("https://localhost:7047/api/account/login", user);
    alert("code da vao day",  response);
    
    const data = response.data;
    
    const userInfo = decodeAndSaveUserInfo(data.token);
    dispatch(loginSuccess(userInfo));
  

    
    if (data.flag === false){
      alert("Xảy ra lỗi không thể đăng nhập");
      return;
    }
   

    console.log("Chécdcdssdcsdcdsdsc", response.data)
    navigate("/");
    alert("Đăng nhập thành công")
  } catch (error) {
  

    dispatch(loginFail());
  }
}


export const RegisterUser = async (user, dispatch, navigate) => {
  dispatch(registerStart());
  try {
    const response = await axios.post("https://localhost:7047/api/account/register", user);

    const data = response.data;
    if (data.result.flag === false){
      alert("Xảy ra lỗi không thể đăng ký")
      return;
    }
    dispatch(registerSuccess());
    alert("Đăng ký tài khoản thành công")
    navigate("/login");
  } catch (error) {
    dispatch(registerFail());
  }
}