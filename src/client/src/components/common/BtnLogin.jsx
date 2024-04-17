import { faSignIn, faSignOut } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../redux/Account";

const BtnLogin = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
let user = useSelector((state) => state.auth.login.currentUser);
//let user = localStorage.getItem("users");
  const handleLogout = async () => {
    await dispatch(logout());
    navigate(`/`);
    window.location.reload();
  };

  return (
    <>
      {user != null ? (
        <>
          <span className="text-white px-3">
            Xin chào
            <Link
              to={`/profile/${user.id}`}
              className="px-1 text-white"
            >
              {user.username}
            </Link>
            
          </span>

          <Link
            className="btn btn-danger px-2 text-decoration-none"
            to="/"
            onClick={handleLogout}
          >
            Đăng xuất
            <FontAwesomeIcon icon={faSignOut} className="px-2" />
          </Link>
        </>
      ) : (
        <div className="px-2">
          <Link className="btn btn-primary" to={`/login`}>
            Đăng nhập
            <FontAwesomeIcon icon={faSignIn} className="px-2" />
          </Link>
        </div>
      )}
    </>
  );
};
export default BtnLogin;
