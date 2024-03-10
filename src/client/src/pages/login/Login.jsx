import React from "react";
import { Image } from "react-bootstrap";
import IconImage from "../../assets/dalavi.png";
import "../../styles/admin/Login.css";
import { Link } from "react-router-dom";
const Login = () => {
  const hanldeLogin = (e) => {
    // e.preventDefault();
    // const newUser = {
    //   userName: userNameLogin,
    //   password: passwordLogin,
    // };
    // LoginUser(newUser, dispatch, naviagate).then(
    //   ()=>{
    //     localStorage.setItem("isLoggedIn", true)
    //   }
    // ).catch((error)=>{
    //   enqueueSnackbar("Đăng nhập không thành công: " + error.message, {
    //     variant: "error",
    //   });
    // });
    // enqueueSnackbar("Đăng nhập thành công", {
    //   variant: "success",
    // });
  };
  return (
    <>
      <section className="login">
        <div className="login_title">Chào mừng bạn đến với hệ thống quản trị Bán Hàng nông sản Đà Lạt</div>
        <div className="login_row">
          <div className="login_col col-6">
            <div className="image_banner">
              <Image src={IconImage} />
            </div>
          </div>
          <div className="login_col col-6">
            <div className="form-container">
              <form onSubmit={hanldeLogin}>
                <div className="form-group">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nhập vào tên ..."
                    required
                  />
                  {/* onChange={(e) => setUserNameLogin(e.target.value)} */}
                </div>
                <div className="form-group mb-3 mt-3">
                  <input
                    type="password"
                    className="form-control"
                    placeholder="Nhập vào mật khẩu ..."
                    required
                  />
                  {/* onChange={(e) => setPasswordLogin(e.target.value)} */}
                </div>
                <div className="form-group text-center">
                  <button type="submit" className="btn btn-success">
                    Đăng nhập
                  </button>
                  <div className="text-end mt-3">
                    Bạn chưa có tài khoản?
                    <Link to={`/admin/dashboard`} className=" text-danger px-1">
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
