import axios from "axios";
import {
  loginStart,
  loginSuccess,
  loginFail,
  registerStart,
  registerSuccess,
  registerFail,
} from "../redux/Account";


export const LoginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    
    const response = await axios.post("https://localhost:7047/api/account/login", user);
    
    const data = response.data;
    console.log(data);
    // localStorage.setItem("token", data.token)
    if (data.flag === false){
      alert("Xảy ra lỗi không thể đăng nhập");
      return;
    }
    dispatch(loginSuccess(response.data));

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