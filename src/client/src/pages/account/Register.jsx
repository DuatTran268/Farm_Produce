import React, { useState } from "react";
import { Image } from "react-bootstrap";
import IconImage from "../../assets/dalavi.png";
import "../../styles/admin/Login.css";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { RegisterUser } from "../../api/Account";

const Register = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setconfirmPassword] = useState("");

  const dispatch = useDispatch();
  const naviagate = useNavigate();

  const hanldeRegister = (e) => {
    e.preventDefault();
    const newRegister = {
      name: name,
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    };
    RegisterUser(newRegister, dispatch, naviagate);
    console.log("Đăng ký thành công");
  };

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
            <form onSubmit={hanldeRegister}>
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
                <input
                  type="password"
                  className="form-control"
                  placeholder="Nhập vào mật khẩu ..."
                  required
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <div className="form-group mb-3 mt-3">
                <input
                  type="password"
                  className="form-control"
                  placeholder="Xác nhận mật khẩu ..."
                  required
                  onChange={(e) => setconfirmPassword(e.target.value)}
                />
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
