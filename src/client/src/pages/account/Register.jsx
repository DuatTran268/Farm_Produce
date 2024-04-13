import React, { useState } from "react";
import { Image } from "react-bootstrap";
import IconImage from "../../assets/dalavi.png";
import "../../styles/admin/Login.css";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { RegisterUser } from "../../api/Account";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash } from "@fortawesome/free-regular-svg-icons";

const Register = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setconfirmPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false); // State để lưu trạng thái hiển thị mật khẩu

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleRegister = (e) => {
    e.preventDefault();
    const newRegister = {
      name: name,
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    };
    RegisterUser(newRegister, dispatch, navigate);
    console.log("Đăng ký thành công");
  };
  // test login push code success

  return (
    <section className="login">
      <div className="login_title">
        Chào mừng bạn đến với hệ thống Bán Hàng nông sản Đà Lạt
      </div>
      <div className="login_row">
        <div className="login_col col-6">
          <div className="image_banner">
            <Image src={IconImage} />
          </div>
        </div>
        <div className="login_col col-6">
          <div className="form-container">
            <form onSubmit={handleRegister}>
              <div className="form-group">
                <input
                  type="text"
                  className="form-control"
                  placeholder="Nhập vào tên ..."
                  required
                  onChange={(e) => setName(e.target.value)}
                />
              </div>
              <div className="form-group mb-3 mt-3">
                <input
                  type="email"
                  className="form-control"
                  placeholder="Nhập vào Email"
                  required
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
              <div className="form-group mb-3 mt-3">
                <div className="password-input">
                  <input
                    type={showPassword ? "text" : "password"} // Thay đổi loại của trường input dựa trên trạng thái hiển thị mật khẩu
                    className="form-control form_inputpassword"
                    placeholder="Nhập vào mật khẩu ..."
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
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
              </div>
              <div className="form-group mb-3 mt-3">
                <input
                  type={showPassword ? "text" : "password"} // Thay đổi loại của trường input dựa trên trạng thái hiển thị mật khẩu
                  className="form-control form_inputpassword"
                  placeholder="Xác nhận mật khẩu ..."
                  required
                  value={confirmPassword}
                  onChange={(e) => setconfirmPassword(e.target.value)}
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
                <button type="submit" className="btn btn-success">
                  Đăng ký
                </button>
                <div className="text-end mt-3">
                  Bạn đã có tài khoản?
                  <Link to={`/login`} className=" text-danger px-1">
                    Đăng nhập
                  </Link>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Register;
