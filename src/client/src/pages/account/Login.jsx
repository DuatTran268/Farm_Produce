import React, { useState } from "react";
import { Image } from "react-bootstrap";
import IconImage from "../../assets/dalavi.png";
import "./Login.css";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";
import { LoginUser } from "../../api/Account";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash } from "@fortawesome/free-regular-svg-icons";
const Login = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();

  const [emailLogin, setEmailLogin] = useState("");
  const [passwordLogin, setPasswordLogin] = useState("");
  const [showPassword, setShowPassword] = useState(false); // State để lưu trạng thái hiển thị mật khẩu

  const dispatch = useDispatch();
  const naviagate = useNavigate();

  const hanldeLogin = (e) => {
    e.preventDefault();
    const newUser = {
      email: emailLogin,
      password: passwordLogin,
    };
    
    LoginUser(newUser, dispatch, naviagate)
      .then(() => {
        localStorage.setItem("isLoggedIn", true);
      })
      .catch((error) => {
        enqueueSnackbar("Đăng nhập không thành công: " + error.message, {
          variant: "error",
        });
      });
  };
  return (
    <>
      <section className="login">
        <div className="login_title">
          Chào mừng bạn đến với hệ thống Bán Hàng nông sản Đà Lạt
        </div>
        <div className="login_row">
          <div className="login_col col-6">
            <Link to={'/'} className="image_banner">
              <Image src={IconImage} className="rotating-image" />
            </Link>
          </div>
          <div className="login_col col-6">
            <div className="form-container">
              <form onSubmit={hanldeLogin}>
                <div className="form-group mb-3 mt-3">
                  <input
                    type="email"
                    className="form-control input_field_account"
                    placeholder="Nhập vào Email"
                    required
                    onChange={(e) => setEmailLogin(e.target.value)}
                  />
                </div>
                <div className="form-group mb-3 mt-3">
                  <input
                    type={showPassword ? "text" : "password"} // Thay đổi loại của trường input dựa trên trạng thái hiển thị mật khẩu
                    className="form-control form_inputpassword input_field_account"
                    placeholder="Nhập vào mật khẩu ..."
                    required
                    value={passwordLogin}
                    onChange={(e) => setPasswordLogin(e.target.value)}
                  />
                  <span
                    className="toggle-password"
                    onClick={() => setShowPassword(!showPassword)} // Xử lý sự kiện bấm vào biểu tượng con mắt
                  >
                    {showPassword ? (
                      <FontAwesomeIcon icon={faEye} />
                    ) : (
                      <FontAwesomeIcon icon={faEyeSlash} />
                    )}
                  </span>
                </div>
                <div className="form-group text-center">
                  <button type="submit" className="button_action">
                    Đăng nhập
                  </button>
                  <div className="text-end mt-3">
                    Bạn chưa có tài khoản?
                    <Link to={`/register`} className=" text-danger px-1">
                      Đăng ký
                    </Link>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};
export default Login;

